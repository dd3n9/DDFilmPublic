import { useEffect, useState } from "react";
import { useAuth } from "../../context/useAuth";
import { useRating } from "../../context/useRating";
import { ISessionParticipantDto } from "../../models/sessionDto";
import apiConnector from "../../api/services/apiConnector";
import { toast } from "react-toastify";
import Modal from "../common/Modal";

interface RatingModalProps {
  isOpen: boolean;
  onClose: () => void;
  sessionId: string;
  onRateMovieParent: (rating: number) => void;
}

const RatingModal: React.FC<RatingModalProps> = ({
  isOpen,
  onClose,
  sessionId,
  onRateMovieParent,
}) => {
  const { ratingProgress, joinRatingRoom, disconnectRatingRoom } = useRating();
  const { user } = useAuth();
  const [selectedRating, setSelectedRating] = useState<number | null>(null);
  const [sessionParticipants, setSessionParticipants] = useState<
    ISessionParticipantDto[]
  >([]);

  useEffect(() => {
    const fetchCurrentUsers = async () => {
      try {
        const response = await apiConnector.getSessionParticipants(sessionId);
        setSessionParticipants(response);
      } catch (error) {
        console.error("Failed to fetch users:", error);
        toast.error("Failed to fetch users.");
      }
    };

    if (isOpen) {
      fetchCurrentUsers();
      if (user?.firstName) {
        joinRatingRoom(sessionId, user.firstName);
      }
    } else {
      disconnectRatingRoom();
    }
  }, [isOpen, sessionId]);

  const handleRateMovie = async () => {
    if (selectedRating) {
      await onRateMovieParent(selectedRating);
      setSelectedRating(null);
    }
  };

  const allUsersRated = ratingProgress.length === sessionParticipants.length;

  if (!isOpen) return null;

  return (
    <Modal onClose={onClose}>
      <h2 className="text-2xl font-bold text-white mb-4">Rate Movie</h2>
      <ul className="mb-4 text-white">
        {sessionParticipants.map((sessionParticipant) => {
          const userRating = ratingProgress.find(
            (rating) => rating.userId === sessionParticipant.userId
          );

          if (allUsersRated) {
            return (
              <li key={sessionParticipant.userId} className="mb-2">
                {sessionParticipant.userName}:{" "}
                {userRating?.rating
                  ? `Rated ${userRating.rating}`
                  : "Not Rated"}
              </li>
            );
          }

          if (userRating && user?.userId === sessionParticipant.userId) {
            return (
              <li key={sessionParticipant.userId} className="mb-2">
                {sessionParticipant.userName}: You rated {userRating.rating}
              </li>
            );
          }

          if (userRating) {
            return (
              <li key={sessionParticipant.userId} className="mb-2">
                {sessionParticipant.userName}: Rated
              </li>
            );
          }

          if (user?.userId === sessionParticipant.userId) {
            return null;
          }

          return (
            <li key={sessionParticipant.userId} className="mb-2">
              {sessionParticipant.userName}: Pending
            </li>
          );
        })}
      </ul>
      {!allUsersRated &&
        !ratingProgress.some((r) => r.userId === user?.userId) && (
          <div className="mb-4">
            <label className="block text-sm font-medium text-white mb-2">
              Your Rating:
            </label>
            <select
              className="border rounded w-full p-2 bg-[#2F0740] text-white"
              value={selectedRating || ""}
              onChange={(e) => setSelectedRating(Number(e.target.value))}
            >
              <option value="" disabled>
                Select a rating
              </option>
              {[1, 2, 3, 4, 5].map((value) => (
                <option key={value} value={value}>
                  {value}
                </option>
              ))}
            </select>
          </div>
        )}
      {!allUsersRated &&
        !ratingProgress.some((r) => r.userId === user?.userId) && (
          <button
            onClick={handleRateMovie}
            className="px-4 py-2 bg-[#710973] text-white rounded-full hover:bg-[#F244D5] transition-all"
            disabled={!selectedRating}
          >
            Submit Rating
          </button>
        )}
    </Modal>
  );
};

export default RatingModal;
