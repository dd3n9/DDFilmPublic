import { useCallback, useEffect, useRef, useState } from "react";
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { toast } from "react-toastify";
import { SESSION_HUB_URL } from "../utils/globalConfig";

export const useSession = () => {
  const [connection, setConnection] = useState<signalR.HubConnection | null>(
    null
  );
  const [isConnected, setIsConnected] = useState<boolean>(false);
  const [userList, setUserList] = useState<string[]>([]);

  const connectionRef = useRef<signalR.HubConnection | null>(null);

  const startConnection = useCallback(async () => {
    try {
      const hubConnection = new HubConnectionBuilder()
        .withUrl(`${process.env.REACT_APP_HOST_HUB}${SESSION_HUB_URL}`)
        .withAutomaticReconnect()
        .build();

      connectionRef.current = hubConnection;
      setConnection(hubConnection);

      await hubConnection.start();
      setIsConnected(true);
      console.log("SignalR SessionHub Connected.");

      hubConnection.on("UserJoined", (userName: string) => {
        console.log(`User Joined: ${userName}`);
        setUserList((prevUsers) => [...prevUsers, userName]);
        toast.success(`${userName} joined the session!`);
      });

      hubConnection.on("UserLeft", (userName: string) => {
        console.log(`User Left: ${userName}`);
        setUserList((prevUsers) =>
          prevUsers.filter((user) => user !== userName)
        );
        toast.warn(`${userName} left the session.`);
      });

      hubConnection.on("BroadcastStarted", () => {
        console.log("Broadcast Started!");
        toast.info("The broadcast has started!");
      });

      hubConnection.on("RatingGiven", (userName: string) => {
        toast.success(`User gave a rating!`);
      });

      hubConnection.onclose((error?: Error) => {
        console.error("SignalR SessionHub Disconnected:", error);
        setIsConnected(false);
      });
    } catch (error: any) {
      console.error("Error starting SignalR SessionHub connection:", error);
    }
  }, []);

  useEffect(() => {
    startConnection();

    return () => {
      if (connectionRef.current) {
        connectionRef.current.stop().then(() => {
          console.log("SignalR SessionHub connection stopped.");
          setIsConnected(false);
        });
      }
    };
  }, [startConnection]);

  const joinSession = useCallback(
    async (sessionId: string, userName: string) => {
      const hubConnection = new HubConnectionBuilder()
        .withUrl(`${process.env.REACT_APP_HOST_HUB}${SESSION_HUB_URL}`)
        .withAutomaticReconnect()
        .build();

      connectionRef.current = hubConnection;

      setIsConnected(true);
      console.log("SignalR SessionHub Connected.");

      hubConnection.on("UserJoined", (userName: string) => {
        console.log(`User Joined: ${userName}`);
        setUserList((prevUsers) => [...prevUsers, userName]);
        toast.success(`${userName} joined the session!`);
      });

      hubConnection.on("UserLeft", (userName: string) => {
        console.log(`User Left: ${userName}`);
        setUserList((prevUsers) =>
          prevUsers.filter((user) => user !== userName)
        );
        toast.warn(`${userName} left the session.`);
      });

      hubConnection.on("BroadcastStarted", () => {
        console.log("Broadcast Started!");
        toast.info("The broadcast has started!");
      });

      hubConnection.on("RatingGiven", (userName: string) => {
        console.log(`Rating Given by: ${userName}`);
        toast.success(`${userName} gave a rating!`);
      });

      hubConnection.onclose((error?: Error) => {
        console.error("SignalR SessionHub Disconnected:", error);
        setIsConnected(false);
      });

      try {
        await hubConnection.start();
        await connectionRef.current.invoke("JoinSession", {
          sessionId,
          userName,
        });
      } catch (error: any) {
        console.error("Error joining session:", error);
      }
      setConnection(hubConnection);
    },
    []
  );

  const disconnectSession = useCallback(async () => {
    if (connectionRef.current) {
      try {
        await connectionRef.current.stop();
        setIsConnected(false);
        setUserList([]);
        setConnection(null);
        console.log("SignalR SessionHub disconnected manually.");
      } catch (error) {
        console.error("Error disconnecting from SessionHub:", error);
      }
    }
  }, []);

  return {
    connection,
    isConnected,
    userList,
    joinSession,
    disconnectSession,
  };
};
