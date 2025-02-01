import { useCallback, useEffect, useRef, useState } from "react";
import { useAuth } from "./useAuth";
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { SESSION_HUB_URL } from "../utils/globalConfig";
import { toast } from "react-toastify";
import { handleError } from "../helpers/ErrorHandler";

interface ChatMessage {
  userName: string;
  message: string;
}

export const useChat = () => {
  const [connection, setConnection] = useState<HubConnection | null>(null);
  const [messages, setMessages] = useState<ChatMessage[]>([]);
  const [isStreaming, setIsStreaming] = useState(false);
  const localMediaStream = useRef<MediaStream | null>(null);
  const peerConnections = useRef<Record<string, RTCPeerConnection>>({});
  const videoContainerRef = useRef<HTMLDivElement | null>(null);

  const joinChat = useCallback(async (userName: string, sessionId: string) => {
    const newConnection = new HubConnectionBuilder()
      .withUrl(`https://localhost:8085${SESSION_HUB_URL}`)
      .withAutomaticReconnect()
      .build();

    newConnection.on("ReceiveMessage", (sender, message) => {
      setMessages((prevMessages) => [
        ...prevMessages,
        { userName: sender, message },
      ]);
    });

    newConnection.on("ReceiveSDP", async (peerId, sdp) => {
      const pc = peerConnections.current[peerId];
      if (!pc) return;

      await pc.setRemoteDescription(new RTCSessionDescription(sdp));
      if (sdp.type === "offer") {
        const answer = await pc.createAnswer();
        await pc.setLocalDescription(answer);
        await newConnection.invoke("SendSDP", peerId, answer);
      }
    });

    newConnection.on("ReceiveIceCandidate", (peerId, candidate) => {
      const pc = peerConnections.current[peerId];
      if (!pc) return;
      pc.addIceCandidate(new RTCIceCandidate(candidate));
    });

    newConnection.on("PeerLeft", (peerId) => {
      if (peerConnections.current[peerId]) {
        peerConnections.current[peerId].close();
        delete peerConnections.current[peerId];
        const videoElement = document.getElementById(`video-${peerId}`);
        videoElement?.remove();
      }
    });

    try {
      await newConnection.start();
      await newConnection.invoke("JoinChat", { userName, sessionId });
      toast.success("Connected to the chat!");
      setConnection(newConnection);
    } catch (error) {
      toast.error("Failed to connect to chat.");
    }

    setConnection(newConnection);
  }, []);

  const initializeScreenSharing = useCallback(async () => {
    try {
      const screenStream = await navigator.mediaDevices.getDisplayMedia({
        video: { cursor: "always" } as any,
        audio: false,
      });

      localMediaStream.current = screenStream;
      setIsStreaming(true);

      Object.values(peerConnections.current).forEach((pc) => {
        screenStream
          .getTracks()
          .forEach((track) => pc.addTrack(track, screenStream));
      });
    } catch (error) {
      toast.error("Failed to start screen sharing.");
    }
  }, []);

  const createPeerConnection = useCallback(
    (peerId: string) => {
      const pc = new RTCPeerConnection({
        iceServers: [{ urls: "stun:stun.l.google.com:19302" }],
      });

      peerConnections.current[peerId] = pc;

      pc.onicecandidate = (event) => {
        if (event.candidate) {
          connection?.invoke("SendIceCandidate", peerId, event.candidate);
        }
      };

      pc.ontrack = (event) => {
        const videoContainer = videoContainerRef.current;

        if (!document.getElementById(`video-${peerId}`)) {
          const videoElement = document.createElement("video");
          videoElement.id = `video-${peerId}`;
          videoElement.srcObject = event.streams[0];
          videoElement.autoplay = true;
          videoElement.playsInline = true;
          videoElement.className = "remote-video";
          videoContainer?.appendChild(videoElement);
        }
      };

      localMediaStream.current
        ?.getTracks()
        .forEach((track) => pc.addTrack(track, localMediaStream.current!));

      return pc;
    },
    [connection]
  );

  const sendSDP = useCallback(
    async (peerId: string, sdp: RTCSessionDescriptionInit) => {
      await connection?.invoke("SendSDP", peerId, sdp);
    },
    [connection]
  );

  const sendMessage = useCallback(
    async (message: string) => {
      if (connection) {
        try {
          await connection.invoke("SendMessage", message);
        } catch (error) {
          console.error("Failed to send message:", error);
        }
      }
    },
    [connection]
  );

  const onLeaveChat = useCallback(async () => {
    if (connection) {
      try {
        await connection.stop();
        setConnection(null);
        toast.success("Left the chat!");
      } catch (error) {
        console.error("Failed to leave chat:", error);
      }
    }

    Object.values(peerConnections.current).forEach((pc) => pc.close());
    peerConnections.current = {};
    setIsStreaming(false);
    localMediaStream.current?.getTracks().forEach((track) => track.stop());
  }, [connection]);

  useEffect(() => {
    return () => {
      onLeaveChat();
    };
  }, [onLeaveChat]);

  return {
    connection,
    messages,
    joinChat,
    initializeScreenSharing,
    createPeerConnection,
    sendSDP,
    sendMessage,
    onLeaveChat,
    isStreaming,
    videoContainerRef,
  };
};
