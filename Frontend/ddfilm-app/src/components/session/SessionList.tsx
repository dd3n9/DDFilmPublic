import { useState } from "react";
import { toast } from "react-toastify";
import { ISessionDto } from "../../models/sessionDto";
import { useAuth } from "../../context/useAuth";
import apiConnector from "../../api/services/apiConnector";
import SessionLoginModal from "../modals/SessionLoginModal";

interface SessionListProps {
  sessions: ISessionDto[];
}

const SessionList: React.FC<SessionListProps> = ({ sessions }) => {
  const { user } = useAuth();
  const [selectedSession, setSelectedSession] = useState<string | null>(null);

  const handleLogin = async (sessionId: string, password: string) => {
    try {
      const response = await apiConnector.loginSession(sessionId, password);
      if (response) {
        toast.success("Successfully joined the session!");
      }
    } catch (error) {
      toast.error("Error");
    } finally {
      setSelectedSession(null);
    }
  };

  return (
    <div className="container mx-auto p-6 bg-gradient-to-b from-[#140626] via-[#2F0740] to-[#710973] rounded-3xl">
      <h2 className="text-2xl font-semibold text-center text-[#F244D5] mb-6">
        Sessions
      </h2>
      <div className="space-y-4">
        {sessions.map((session) => {
          const isOwner = user?.userName === session.ownerName;
          return (
            <div
              key={session.id}
              className="flex justify-between items-center bg-[#0D0D0D] p-4 rounded-2xl shadow-lg border border-[#710973] hover:bg-[#2F0740] transition-all"
            >
              <div className="flex items-center space-x-4">
                <span className="text-lg font-medium text-[#F244D5]">
                  {session.sessionName}
                </span>
                <span className="text-sm text-gray-400">
                  ({session.participantsCount}/{session.participantLimit}{" "}
                  participants)
                </span>
              </div>
              <div className="flex items-center space-x-4">
                <span className="text-sm text-[#C79FB4]">
                  Owner: {session.ownerName}
                </span>
                {!isOwner && (
                  <button
                    onClick={() => setSelectedSession(session.id)}
                    className="px-6 py-2 bg-[#710973] text-white rounded-full hover:bg-[#A62EFF] hover:text-[#0D0D0D] focus:outline-none focus:ring-2 focus:ring-[#710973] focus:ring-offset-2 focus:ring-offset-[#140626]"
                  >
                    Join
                  </button>
                )}
              </div>
            </div>
          );
        })}
      </div>
      {selectedSession && (
        <SessionLoginModal
          sessionId={selectedSession}
          onClose={() => setSelectedSession(null)}
          onLogin={handleLogin}
        />
      )}
    </div>
  );
};

export default SessionList;
