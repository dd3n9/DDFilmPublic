import { useEffect, useState } from "react";
import { useSearch } from "../context/SearchContext";
import { PaginationRequestParams } from "../models/pagination";
import Pagination from "../components/common/Pagination";
import { useLocation } from "react-router-dom";
import { PATH_PRIVATE } from "../routes/paths";
import { handleError } from "../helpers/ErrorHandler";
import Spinner from "../components/common/Spinner";
import { ISessionDto } from "../models/sessionDto";
import apiConnector from "../api/services/apiConnector";
import MySessionList from "../components/session/MySessionList";
import SessionList from "../components/session/SessionList";
import CreateSessionModal from "../components/session/CreateSessionModal";

const SessionPage: React.FC<{ showMySessions?: boolean }> = () => {
  const location = useLocation();
  const showMySessions = location.pathname === PATH_PRIVATE.mySessions;

  const [sessions, setSessions] = useState<ISessionDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [notFound, setNotFound] = useState(false);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [pagination, setPagination] = useState<PaginationRequestParams>({
    pageNumber: 1,
    pageSize: 10,
  });
  const [paginationMeta, setPaginationMeta] = useState<{
    currentPage: number;
    totalPages: number;
  } | null>(null);

  useEffect(() => {
    setPagination((prev) => ({ ...prev, pageNumber: 1 }));
  }, [location.pathname]);

  useEffect(() => {
    fetchSessions();
  }, [location.pathname, pagination]);

  const fetchSessions = async () => {
    setLoading(true);
    setNotFound(false);
    try {
      const response = showMySessions
        ? await apiConnector.getMySessions({ ...pagination })
        : await apiConnector.getAllSessions({ ...pagination });

      if (response.results.length > 0) {
        setSessions(response.results);
        setPaginationMeta({
          currentPage: response.paginationParams?.currentPage ?? 1,
          totalPages: response.paginationParams?.totalPages ?? 1,
        });
      } else {
        setNotFound(true);
      }
    } catch (error) {
      console.error("Error fetching sessions:", error);
      setNotFound(true);
    } finally {
      setLoading(false);
    }
  };

  const handlePageChange = (newPage: number) => {
    setPagination((prev) => ({ ...prev, pageNumber: newPage }));
  };

  const handleCreateSession = async (name: string, password: string) => {
    try {
      await apiConnector.createSession(name, password);
      setIsModalOpen(false);
      fetchSessions();
    } catch (error) {
      handleError(error);
    }
  };

  if (loading) {
    return (
      <div className="flex items-center justify-center h-screen text-center text-2xl">
        <Spinner></Spinner>
      </div>
    );
  }

  if (notFound) {
    return (
      <div className="text-center text-2xl text-[#F244D5] py-6">
        No sessions found.
        <div className="mb-4 container mx-auto text-center py-6">
          <button
            onClick={() => setIsModalOpen(true)}
            className="px-4 py-1.5 bg-[#710973] text-lg text-white rounded-full hover:bg-[#F244D5] transition-all"
          >
            Create
          </button>
        </div>
      </div>
    );
  }

  return (
    <div className="p-6">
      <div className="mb-4 container mx-auto text-right">
        <button
          onClick={() => setIsModalOpen(true)}
          className="px-4 py-2 bg-[#710973] text-white rounded-full hover:bg-[#F244D5] transition-all"
        >
          Create
        </button>
      </div>

      {showMySessions ? (
        <MySessionList sessions={sessions} fetchSessions={fetchSessions} />
      ) : (
        <SessionList sessions={sessions} />
      )}
      {paginationMeta && (
        <Pagination
          currentPage={pagination.pageNumber}
          totalPages={paginationMeta.totalPages}
          onPageChange={handlePageChange}
        />
      )}
      {isModalOpen && (
        <CreateSessionModal
          onClose={() => setIsModalOpen(false)}
          onSubmit={handleCreateSession}
        />
      )}
    </div>
  );
};

export default SessionPage;
