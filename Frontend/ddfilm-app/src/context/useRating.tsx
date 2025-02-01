import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { useCallback, useEffect, useState } from "react";
import { RATING_HUB_URL } from "../utils/globalConfig";
import { toast } from "react-toastify";
import { RatingProgress } from "../models/sessionDto";

export const useRating = () => {
  const [connection, setConnection] = useState<HubConnection | null>(null);
  const [ratingProgress, setRatingProgress] = useState<RatingProgress[]>([]);

  const joinRatingRoom = useCallback(
    async (sessionId: string, userName: string) => {
      const newConnection = new HubConnectionBuilder()
        .withUrl(`${process.env.REACT_APP_HOST_HUB}${RATING_HUB_URL}`)
        .withAutomaticReconnect()
        .build();

      newConnection.on(
        "ReceiveAllRatings",
        (ratingProgress: RatingProgress[]) => {
          setRatingProgress(ratingProgress);
        }
      );

      newConnection.on("ReceiveNewRating", (ratingProgress: RatingProgress) => {
        setRatingProgress((prev) => [...prev, ratingProgress]);
      });

      try {
        await newConnection.start();
        await newConnection.invoke("JoinRatingRoom", {
          userName,
          sessionId,
        });
        toast.success("Connected to rating room.");
        setConnection(newConnection);
      } catch (error) {
        toast.error("Failed to connect to rating room.");
      }
      setConnection(newConnection);
    },
    []
  );

  const disconnectRatingRoom = useCallback(async () => {
    if (connection) {
      try {
        await connection.stop();
        setConnection(null);
        setRatingProgress([]);
      } catch (error) {
        console.error("Failed to leave chat:", error);
      }
    }
  }, [connection]);

  useEffect(() => {
    return () => {
      disconnectRatingRoom();
    };
  }, [disconnectRatingRoom]);

  return {
    connection,
    ratingProgress,
    joinRatingRoom,
    disconnectRatingRoom,
  };
};
