import { useEffect, useState } from "react";
import { ISessionParticipantDto } from "../../../models/sessionDto";
import apiConnector from "../../../api/services/apiConnector";
import Spinner from "../../common/Spinner";

interface ParticipantTabProps {
  sessionId: string;
}

const ParticipantTab: React.FC<ParticipantTabProps> = ({ sessionId }) => {
  const [participants, setParticipants] = useState<ISessionParticipantDto[]>(
    []
  );
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchParticipant = async () => {
      try {
        const response = await apiConnector.getSessionParticipants(sessionId);

        if (response.length > 0) {
          setParticipants(response);
        }
      } catch (err) {
        setError("Failed to load watched movies.");
      } finally {
        setIsLoading(false);
      }
    };
    fetchParticipant();
  }, [sessionId]);

  if (isLoading) {
    return (
      <div className="flex items-center justify-center">
        <Spinner></Spinner>
      </div>
    );
  }

  return (
    <div>
      <ul className="space-y-4">
        {participants.map((participant) => (
          <li
            key={participant.userId}
            className="p-4 bg-[#2F0740] rounded-2xl shadow-md border border-[#710973] hover:bg-[#F244D5]/50 transition-all"
          >
            <p className="font-semibold text-lg text-[#F244D5]">
              {participant.userName}
            </p>
            <p className="text-[#F2F2E9]">
              <span className="font-medium">Role:</span> {participant.role}
            </p>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default ParticipantTab;
