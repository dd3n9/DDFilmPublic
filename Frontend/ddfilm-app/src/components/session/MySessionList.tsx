import { toast } from "react-toastify";
import { useNavigate } from "react-router-dom";
import { ISessionDto } from "../../models/sessionDto";
import { useAuth } from "../../context/useAuth";
import { useState } from "react";
import apiConnector from "../../api/services/apiConnector";
import ConfirmLogoutModal from "../modals/ConfirmLogoutModal";

interface SessionListProps {
  sessions: ISessionDto[];
  fetchSessions: () => void;
}

const MySessionList: React.FC<SessionListProps> = ({
  sessions,
  fetchSessions,
}) => {
  const { user } = useAuth();
  const navigate = useNavigate();
  const [selectedSession, setSelectedSession] = useState<string | null>(null);
  const [confirmLogout, setConfirmLogout] = useState<string | null>(null);

  const handleLogout = async (sessionId: string) => {
    try {
      const response = await apiConnector.logoutSession(sessionId);
      if (response) {
        toast.success("Successfully logged out of the session!");
        setConfirmLogout(null);
        fetchSessions();
      }
    } catch (error) {
      toast.error("Successfully logged out of the session!");
    } finally {
      setSelectedSession(null);
    }
  };

  return (
    <div className="container mx-auto p-6 bg-gradient-to-b from-[#140626] via-[#2F0740] to-[#710973] rounded-3xl">
      <h2 className="text-2xl font-semibold text-center text-[#F244D5] mb-6">
        My Sessions
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
                <button
                  onClick={() => setConfirmLogout(session.id)}
                  className="px-6 py-2 bg-[#F244D5] text-white rounded-full hover:bg-[#FF66E5] hover:text-[#0D0D0D] focus:outline-none focus:ring-2 focus:ring-[#F244D5] focus:ring-offset-2 focus:ring-offset-[#140626]"
                >
                  Logout
                </button>
                <button
                  onClick={() => navigate(`/sessions/details/${session.id}`)}
                  className="px-6 py-2 bg-[#710973] text-white rounded-full hover:bg-[#A62EFF] hover:text-[#0D0D0D] focus:outline-none focus:ring-2 focus:ring-[#710973] focus:ring-offset-2 focus:ring-offset-[#140626]"
                >
                  Open
                </button>
              </div>
            </div>
          );
        })}
      </div>

      {confirmLogout && (
        <ConfirmLogoutModal
          sessionId={confirmLogout}
          onClose={() => setConfirmLogout(null)}
          onConfirm={() => handleLogout(confirmLogout)}
        />
      )}
    </div>
  );
};

export default MySessionList;
