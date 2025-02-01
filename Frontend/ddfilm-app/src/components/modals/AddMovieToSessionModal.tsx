import React, { useEffect, useState } from "react";
import { ISessionDto } from "../../models/sessionDto";
import { PaginationRequestParams } from "../../models/pagination";
import apiConnector from "../../api/services/apiConnector";
import Pagination from "../common/Pagination";

interface AddToSessionModalProps {
  movieTitle: string;
  isOpen: boolean;
  onClose: () => void;
  onAddToSession: (sessionId: string) => void;
}

const AddMovieToSessionModal: React.FC<AddToSessionModalProps> = ({
  movieTitle,
  isOpen,
  onClose,
  onAddToSession,
}) => {
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(false);
  const [sessions, setSessions] = useState<ISessionDto[]>([]);
  const [pagination, setPagination] = useState<PaginationRequestParams>({
    pageNumber: 1,
    pageSize: 10,
  });

  const [paginationMeta, setPaginationMeta] = useState<{
    currentPage: number;
    totalPages: number;
  } | null>(null);

  const handlePageChange = (newPage: number) => {
    setPagination((prev) => ({ ...prev, pageNumber: newPage }));
  };

  useEffect(() => {
    const fetchMovieDetails = async () => {
      setLoading(true);
      setError(false);
      try {
        const userSessions = await apiConnector.getMySessions(pagination);
        if (userSessions.results.length > 0) {
          setSessions(userSessions.results);
          setPaginationMeta({
            currentPage: userSessions.paginationParams?.currentPage ?? 1,
            totalPages: userSessions.paginationParams?.totalPages ?? 1,
          });
        }
      } catch (err) {
        setError(true);
        console.error("Error fetching movie details:", err);
      } finally {
        setLoading(false);
      }
    };

    fetchMovieDetails();
  }, [pagination]);

  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 bg-black bg-opacity-60 flex items-center justify-center z-50">
      <div className="relative bg-gradient-to-b from-[#2F0740] to-[#140626] rounded-3xl shadow-2xl p-6 w-full max-w-md">
        <button
          className="absolute top-3 right-3 text-[#F244D5] hover:text-[#FF66E5] text-2xl transition-all"
          onClick={onClose}
        >
          ×
        </button>
        <h3 className="text-2xl font-bold text-center text-[#F244D5] mb-6">
          Add <span className="text-white">{movieTitle}</span> to a Session
        </h3>
        {sessions.length > 0 ? (
          <ul className="space-y-4">
            {sessions.map((session) => (
              <li key={session.id}>
                <button
                  className="w-full py-3 px-5 bg-gradient-to-r from-[#F244D5] to-[#710973] text-white rounded-full hover:shadow-lg transition-all"
                  onClick={() => onAddToSession(session.id)}
                >
                  {session.sessionName}
                </button>
              </li>
            ))}
          </ul>
        ) : (
          <p className="text-center text-gray-400">No sessions found.</p>
        )}
        {paginationMeta && (
          <div className="mt-6">
            <Pagination
              currentPage={paginationMeta.currentPage}
              totalPages={paginationMeta.totalPages}
              onPageChange={handlePageChange}
            />
          </div>
        )}
      </div>
    </div>
  );
};

export default AddMovieToSessionModal;
