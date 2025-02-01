import { useCallback, useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import RandomWheel from "../components/session/watchSession/RandomWheel";
import TabNavigation from "../components/layout/TabNavigation";
import { toast } from "react-toastify";
import WatchSession from "../components/session/watchSession/WatchSession";
import { useAuth } from "../context/useAuth";
import { ISessionMovieDto } from "../models/sessionDto";
import { useSession } from "../context/useSession";
import { useWatching } from "../context/useWatching";
import apiConnector from "../api/services/apiConnector";
import apiMovieConnector from "../api/services/apiMovieConnector";
import SessionMoviesTab from "../components/session/SessionMoviesTab";
import WatchedMoviesTab from "../components/session/watchSession/WatchedMoviesTab";
import RatingModal from "../components/modals/RatingModal";
import ParticipantTab from "../components/session/watchSession/ParticipantTab";

const SessionDetailsPage: React.FC = () => {
  const { user } = useAuth();
  const { id } = useParams<{ id: string }>();

  const {
    isConnected: isSessionConnected,
    joinSession,
    disconnectSession,
  } = useSession();

  const {
    messages,
    sendMessage,
    joinWatchingSession,
    leaveWatchingSession,
    isStreamingActive,
    isLocalStreamReady,
    startDemo,
    endDemo,
    provideMediaRef,
    streamerConnectionId,
    streamerUserName,
    clients,
  } = useWatching(id ?? "", user?.userId ?? "defaultUserName");

  const [isRatingModalOpen, setIsRatingModalOpen] = useState(false);
  const [watchingMovie, setWatchingMovie] = useState<ISessionMovieDto | null>(
    null
  );
  const [activeTab, setActiveTab] = useState<
    "sessionMovies" | "participants" | "watchedMovies"
  >("sessionMovies");
  const [isWatching, setIsWatching] = useState(false);

  const handleTabChange = useCallback(
    (tab: "sessionMovies" | "participants" | "watchedMovies") => {
      setActiveTab(tab);
    },
    []
  );

  useEffect(() => {}, [isStreamingActive]);

  useEffect(() => {
    const fetchWatchingMovies = async () => {
      try {
        if (id) {
          const movie = await apiConnector.getWatchingSessionMovie(id);
          setWatchingMovie(movie || null);
        }
      } catch (error) {
        console.error("Failed to fetch current watching movie:", error);
        toast.error("Failed to load the current movie.");
      }
    };

    fetchWatchingMovies();
  }, [id]);

  const handleRateMovie = useCallback(
    async (rating: number) => {
      if (id && watchingMovie) {
        try {
          await apiMovieConnector.rateMovie(rating, watchingMovie.tmdbId);
          toast.success("Rating submitted!");
        } catch (error) {
          console.error("Failed to submit rating:", error);
          toast.error("Failed to submit rating.");
        }
      }
    },
    [id, watchingMovie]
  );

  const handleStartWatching = async () => {
    if (id && user?.firstName) {
      await joinWatchingSession(id, user.firstName);
      setIsWatching(true);
    }
  };

  const startRatingRoom = useCallback(async () => {
    setIsRatingModalOpen(true);
  }, []);

  const stopRatingRoom = useCallback(() => {
    setIsRatingModalOpen(false);
  }, []);

  const handleStopWatching = useCallback(async () => {
    if (id && user?.firstName) {
      await leaveWatchingSession(id, user.firstName);

      setIsWatching(false);
    }
  }, [id, user?.firstName, disconnectSession]);

  useEffect(() => {
    if (id && user?.firstName) {
      joinSession(id, user.firstName);
    }
    return () => {
      if (id) {
        disconnectSession();
      }
    };
  }, [id, user?.firstName, joinSession, disconnectSession]);

  return (
    <div>
      {isWatching ? (
        <WatchSession
          onLeaveSession={handleStopWatching}
          sessionId={id || ""}
          startDemo={startDemo}
          endDemo={endDemo}
          messages={messages}
          sendMessage={sendMessage}
          isStreamingActive={isStreamingActive}
          isLocalStreamReady={isLocalStreamReady}
          provideMediaRef={provideMediaRef}
          clients={clients}
          streamerConnectionId={streamerConnectionId || ""}
          streamerUserName={streamerUserName || ""}
        />
      ) : (
        <div className="max-w-5xl mx-auto p-6">
          <RandomWheel />
          <div className="text-center mb-6">
            {watchingMovie && (
              <div className="bg-[#0D0D0D]/90 rounded-3xl p-6 shadow-2xl mb-6">
                <h2 className="text-xl font-bold text-white mb-4">
                  Now Watching: {watchingMovie.movieTitle}
                </h2>
                <button
                  className="px-4 py-2 bg-[#710973] text-white rounded-full hover:bg-[#F244D5] transition-all"
                  onClick={startRatingRoom}
                >
                  Rate Movie
                </button>
              </div>
            )}
            <button
              className="px-6 py-3 bg-[#710973] text-white rounded-full hover:bg-[#F244D5] transition-all font-bold shadow-lg"
              onClick={handleStartWatching}
              disabled={isWatching}
            >
              {isSessionConnected ? "Watching Now" : "Start Watching"}
            </button>
          </div>
          <TabNavigation activeTab={activeTab} onTabChange={handleTabChange} />
          <div className="bg-[#0D0D0D]/90 rounded-3xl shadow-2xl p-6">
            {activeTab === "sessionMovies" && id && (
              <SessionMoviesTab sessionId={id} />
            )}
            {activeTab === "participants" && id && (
              <ParticipantTab sessionId={id} />
            )}
            {activeTab === "watchedMovies" && id && (
              <WatchedMoviesTab sessionId={id} />
            )}
          </div>
          <RatingModal
            isOpen={isRatingModalOpen}
            onClose={stopRatingRoom}
            sessionId={id || ""}
            onRateMovieParent={handleRateMovie}
          />
        </div>
      )}
    </div>
  );
};

export default SessionDetailsPage;
