import { useCallback, useEffect, useState } from "react";
import { useAuth } from "../../context/useAuth";
import { ISessionMovieDto } from "../../models/sessionDto";
import { PaginationRequestParams } from "../../models/pagination";
import apiConnector from "../../api/services/apiConnector";
import Pagination from "../common/Pagination";
interface WatchedMoviesTabProps {
  sessionId: string;
}

const SessionMoviesTab: React.FC<WatchedMoviesTabProps> = ({ sessionId }) => {
  const { user } = useAuth();
  const [movies, setMovies] = useState<ISessionMovieDto[]>([]);
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  const [pagination, setPagination] = useState<PaginationRequestParams>({
    pageNumber: 1,
    pageSize: 10,
  });
  const [paginationMeta, setPaginationMeta] = useState<{
    currentPage: number;
    totalPages: number;
  } | null>(null);

  const handleDeleteMovie = useCallback(
    async (tmdbId: number) => {
      await apiConnector.deleteSessionMovie(sessionId, tmdbId);
    },
    [sessionId]
  );

  useEffect(() => {
    const fetchMovies = async () => {
      try {
        const response = await apiConnector.getUnwatchedSessionMovies(
          sessionId,
          pagination
        );

        if (response.results.length > 0) {
          setMovies(response.results);
          setPaginationMeta({
            currentPage: response.paginationParams?.currentPage ?? 1,
            totalPages: response.paginationParams?.totalPages ?? 1,
          });
        }
      } catch (err) {
        setError("Failed to load watched movies.");
      } finally {
        setIsLoading(false);
      }
    };
    fetchMovies();
  }, [sessionId, pagination, handleDeleteMovie]);

  const handlePageChange = (newPage: number) => {
    setPagination((prev) => ({ ...prev, pageNumber: newPage }));
  };

  if (isLoading) return <p>Loading movies...</p>;
  if (error) return <p className="text-red-500">{error}</p>;

  return (
    <div>
      <ul className="space-y-4">
        {movies &&
          movies.map((movie) => (
            <li
              key={movie.sessionMovieId}
              className="p-4 bg-[#2F0740] rounded-2xl shadow-md border border-[#710973] hover:bg-[#F244D5]/50 transition-all flex justify-between items-center"
            >
              <div className="flex items-center">
                <div>
                  <p className="font-semibold text-lg text-[#F244D5] mr-4">
                    {movie.movieTitle}
                  </p>
                  <p className="text-[#F2F2E9]">
                    <span className="font-medium">Added by:</span>{" "}
                    {movie.addedByUserName}
                  </p>
                </div>
              </div>
              {user?.userName === movie.addedByUserName && (
                <button
                  onClick={() => handleDeleteMovie(movie.tmdbId)}
                  className="px-6 py-2 bg-[#710973] text-white rounded-full hover:bg-[#A62EFF] hover:text-[#0D0D0D] focus:outline-none focus:ring-2 focus:ring-[#710973] focus:ring-offset-2 focus:ring-offset-[#140626]"
                >
                  Delete
                </button>
              )}
            </li>
          ))}
      </ul>
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
  );
};

export default SessionMoviesTab;
