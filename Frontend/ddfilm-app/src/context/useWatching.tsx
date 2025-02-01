import { useCallback, useEffect, useRef, useState } from "react";
import * as signalR from "@microsoft/signalr";
import { LOCAL_VIDEO, WATCHING_HUB_URL } from "../utils/globalConfig";

interface ChatMessage {
  userName: string;
  message: string;
}

export const useWatching = (sessionId: string, userName: string) => {
  const [clients, updateClients] = useState<string[]>([]);
  const [streamerConnectionId, setStreamerConnectionId] = useState<string>();
  const [streamerUserName, setStreamerUserName] = useState<string>();
  const [isStreamingActive, setIsStreamingActive] = useState<boolean>(false);
  const [isLocalStreamActive, setIsLocalStreamActive] =
    useState<boolean>(false);
  const [isLocalStreamReady, setIsLocalStreamReady] = useState<boolean>(false);
  const [messages, setMessages] = useState<ChatMessage[]>([]);
  const connectionRef = useRef<signalR.HubConnection | null>(null);
  const peerConnections = useRef<{ [key: string]: RTCPeerConnection }>({});
  const localMediaStream = useRef<MediaStream | null>(null);
  const peerMediaElements = useRef<{ [key: string]: HTMLVideoElement | null }>({
    [LOCAL_VIDEO]: null,
  });

  const addNewClient = useCallback((newClient: string) => {
    updateClients((list) =>
      list.includes(newClient) ? list : [...list, newClient]
    );
  }, []);

  const removeClient = useCallback((clientID: string) => {
    updateClients((list) => list.filter((id) => id !== clientID));
  }, []);

  useEffect(() => {
    async function startLocalStream() {
      try {
        const stream = await navigator.mediaDevices.getDisplayMedia({
          audio: true,
          video: true,
        });
        localMediaStream.current = stream;

        setIsLocalStreamReady(true);
        if (peerMediaElements.current[LOCAL_VIDEO]) {
          peerMediaElements.current[LOCAL_VIDEO]!.srcObject = stream;
        }
      } catch (error) {
        console.error("Could not get user media", error);
        setIsLocalStreamReady(false);
      }
    }

    if (isLocalStreamActive) {
      startLocalStream();
    } else if (peerMediaElements.current[LOCAL_VIDEO]) {
      peerMediaElements.current[LOCAL_VIDEO]!.srcObject = null;
      setIsLocalStreamReady(false);
    }

    return () => {
      localMediaStream.current?.getTracks().forEach((track) => track.stop());
      setIsLocalStreamReady(false);
    };
  }, [isLocalStreamActive]);

  const createOffer = useCallback(async (targetPeerId: string) => {
    if (!localMediaStream.current) {
      console.error("Local media stream not available for offer.");
      return;
    }

    const peerConnection = new RTCPeerConnection({
      iceServers: [{ urls: "stun:stun1.l.google.com:19302" }],
    });
    peerConnections.current[targetPeerId] = peerConnection;

    peerConnection.onicecandidate = (event) => {
      if (event.candidate) {
        console.log("SendIceCandidate in createOffer");
        connectionRef.current?.invoke(
          "SendIceCandidate",
          targetPeerId,
          JSON.stringify(event.candidate)
        );
      }
    };

    peerConnection.ontrack = (event) => {
      peerMediaElements.current[targetPeerId]!.srcObject = event.streams?.[0];
    };

    localMediaStream.current
      .getTracks()
      .forEach((track) =>
        peerConnection.addTrack(track, localMediaStream.current!)
      );

    const offer = await peerConnection.createOffer();
    await peerConnection.setLocalDescription(offer);
    connectionRef.current?.invoke(
      "SendOffer",
      targetPeerId,
      JSON.stringify(offer)
    );
  }, []);

  const createAnswer = useCallback(
    async (remotePeerId: string, offer: string) => {
      const peerConnection = new RTCPeerConnection({
        iceServers: [{ urls: "stun:stun1.l.google.com:19302" }],
      });
      peerConnections.current[remotePeerId] = peerConnection;

      peerConnection.onicecandidate = (event) => {
        if (event.candidate) {
          console.log("SendIceCandidate in CreateAnswer");
          connectionRef.current?.invoke(
            "SendIceCandidate",
            remotePeerId,
            JSON.stringify(event.candidate)
          );
        }
      };

      peerConnection.ontrack = (event) => {
        if (peerMediaElements.current[remotePeerId]) {
          peerMediaElements.current[remotePeerId]!.srcObject =
            event.streams?.[0];
        } else {
          console.warn(
            `Video element for remotePeerId not yet available in ontrack`
          );
        }
      };

      const remoteSdp = new RTCSessionDescription(JSON.parse(offer));
      await peerConnection.setRemoteDescription(remoteSdp);

      const answer = await peerConnection.createAnswer();
      await peerConnection.setLocalDescription(answer);
      connectionRef.current?.invoke(
        "SendAnswer",
        remotePeerId,
        JSON.stringify(answer)
      );
    },
    []
  );

  useEffect(() => {
    if (isLocalStreamActive && isLocalStreamReady) {
      console.log(
        "Local stream is ready (isLocalStreamReady is true). Creating offers in a separate useEffect."
      );
      clients.forEach(async (clientId) => {
        if (clientId !== LOCAL_VIDEO) {
          await createOffer(clientId);
        }
      });
    }
  }, [isLocalStreamReady, clients, createOffer]);

  useEffect(() => {
    if (connectionRef.current) {
      connectionRef.current?.on("ReceiveMessage", (sender, message) => {
        setMessages((prevMessages) => [
          ...prevMessages,
          { userName: sender, message },
        ]);
      });
    }
  }, [connectionRef.current]);

  useEffect(() => {
    if (connectionRef.current) {
      connectionRef.current?.on("NewViewerJoined", async (viewerId: string) => {
        if (isLocalStreamReady && localMediaStream.current) {
          await createOffer(viewerId);
        }
      });

      connectionRef.current?.on(
        "ReceiveOffer",
        async (remotePeerId: string, sessionDescription: string) => {
          if (!isStreamingActive) {
            console.log(
              "Lecturer received offer, shouldn't happen in lecture mode."
            );
            return;
          }
          console.log("Viewer received offer, creating answer.");
          await createAnswer(remotePeerId, sessionDescription);
        }
      );

      connectionRef.current?.on(
        "ReceiveAnswer",
        async (remotePeerId: string, sessionDescription: string) => {
          try {
            await peerConnections.current[remotePeerId]?.setRemoteDescription(
              new RTCSessionDescription(JSON.parse(sessionDescription))
            );
          } catch (error) {
            console.error("Error setting remote description:", error);
          }
        }
      );

      connectionRef.current?.on(
        "ReceiveIceCandidate",
        async (remotePeerId: string, iceCandidate: string) => {
          try {
            await peerConnections.current[remotePeerId]?.addIceCandidate(
              JSON.parse(iceCandidate)
            );
          } catch (e) {
            console.error("Error adding ICE candidate", e);
          }
        }
      );

      connectionRef.current?.on("UserJoined", (newUserId: string) => {
        addNewClient(newUserId);
      });

      connectionRef.current?.on("UserLeft", (leavingUserId: string) => {
        removeClient(leavingUserId);
        if (peerConnections.current[leavingUserId]) {
          peerConnections.current[leavingUserId].close();
          delete peerConnections.current[leavingUserId];
        }
      });

      connectionRef.current?.on(
        "InitialViewersList",
        (initialViewerIds: string[]) => {
          updateClients(initialViewerIds);
        }
      );

      connectionRef.current?.on(
        "BroadcastStarted",
        (streamerConnectionId: string, streamerUserName: string) => {
          setIsStreamingActive(true);
          setStreamerConnectionId(streamerConnectionId);
          setStreamerUserName(streamerUserName);
          console.log("Broadcast Started signal received.");
        }
      );

      connectionRef.current?.on("BroadcastEnded", () => {
        setIsStreamingActive(false);
      });
    }

    return () => {
      if (connectionRef.current) {
        connectionRef.current?.off("NewViewerJoined");
        connectionRef.current?.off("ReceiveOffer");
        connectionRef.current?.off("ReceiveAnswer");
        connectionRef.current?.off("ReceiveIceCandidate");
        connectionRef.current?.off("UserJoined");
        connectionRef.current?.off("UserLeft");
        connectionRef.current?.off("BroadcastStarted");
        connectionRef.current?.off("BroadcastEnded");
        connectionRef.current?.off("onreconnecting");
        connectionRef.current?.off("onreconnected");
      }
    };
  }, [connectionRef.current, isStreamingActive, clients]);

  const provideMediaRef = useCallback(
    (id: string, node: HTMLVideoElement | null) => {
      if (peerMediaElements.current[id] !== node) {
        peerMediaElements.current[id] = node;
        if (id === LOCAL_VIDEO && node && localMediaStream.current) {
          node.srcObject = localMediaStream.current;
        } else if (node && peerConnections.current[id]) {
          const remoteStream = new MediaStream();
          peerConnections.current[id].getReceivers().forEach((receiver) => {
            if (receiver.track) {
              remoteStream.addTrack(receiver.track);
            }
          });
          node.srcObject = remoteStream;
        }
      }
    },
    []
  );
  const startDemo = useCallback(async (sessionId: string, userName: string) => {
    if (connectionRef.current) {
      try {
        await connectionRef.current.invoke("StartDemo", {
          sessionId,
          userName,
        });
        setIsLocalStreamActive(true);
      } catch (error) {
        console.error("Error starting demo:", error);
      }
    }
  }, []);

  const endDemo = useCallback(async (sessionId: string, userName: string) => {
    if (connectionRef.current) {
      try {
        await connectionRef.current.invoke("EndDemo", {
          sessionId,
          userName,
        });
        setIsStreamingActive(false);
        setIsLocalStreamActive(false);
        setIsLocalStreamReady(false);
      } catch (error) {
        console.error("Error ending demo:", error);
      }
    }
  }, []);

  const joinWatchingSession = useCallback(
    async (sessionId: string, userName: string) => {
      const newConnection = new signalR.HubConnectionBuilder()
        .withUrl(`${process.env.REACT_APP_HOST_HUB}${WATCHING_HUB_URL}`)
        .withAutomaticReconnect()
        .build();
      try {
        await newConnection
          .start()
          .then(async () => {
            console.log("Connected to SignalR WatchingHub");

            await newConnection.invoke("JoinWatchingSession", {
              sessionId,
              userName,
            });

            connectionRef.current = newConnection;
          })
          .catch((err) => console.error("Connection failed: ", err));
      } catch (error) {
        console.error("Error joining watching session:", error);
      }
    },
    []
  );
  const leaveWatchingSession = useCallback(
    async (sessionId: string, userName: string) => {
      if (connectionRef) {
        try {
          await connectionRef.current?.invoke("LeaveWatchingSession", {
            sessionId,
            userName,
          });
          setIsStreamingActive(false);
        } catch (error) {
          console.error("Error leaving watching session:", error);
        }
      }
    },
    []
  );

  const sendMessage = useCallback(
    async (message: string, sessionId: string, userName: string) => {
      if (connectionRef) {
        try {
          await connectionRef.current?.invoke("SendMessage", message, {
            sessionId,
            userName,
          });
        } catch (error) {
          console.error("Failed to send message:", error);
        }
      }
    },
    []
  );

  const remoteClients = clients.filter((clientID) => clientID !== LOCAL_VIDEO);

  return {
    clients: remoteClients,
    streamerConnectionId,
    streamerUserName,
    messages,
    provideMediaRef,
    isStreamingActive,
    isLocalStreamReady,
    startDemo,
    endDemo,
    sendMessage,
    joinWatchingSession,
    leaveWatchingSession,
  };
};
