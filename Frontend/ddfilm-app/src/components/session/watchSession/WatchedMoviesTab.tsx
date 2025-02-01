import { useEffect, useState } from "react";
import { ISessionMovieDto } from "../../../models/sessionDto";
import { PaginationRequestParams } from "../../../models/pagination";
import apiConnector from "../../../api/services/apiConnector";
import Pagination from "../../common/Pagination";

interface WatchedMoviesTabProps {
  sessionId: string;
}

const WatchedMoviesTab: React.FC<WatchedMoviesTabProps> = ({ sessionId }) => {
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

  useEffect(() => {
    const fetchMovies = async () => {
      try {
        const response = await apiConnector.getWatchedSessionMovies(
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
  }, [sessionId, pagination]);

  const handlePageChange = (newPage: number) => {
    setPagination((prev) => ({ ...prev, pageNumber: newPage }));
  };

  if (isLoading) return <p>Loading watched movies...</p>;
  if (error) return <p className="text-red-500">{error}</p>;

  return (
    <div>
      <ul className="space-y-4">
        {movies.map((movie) => (
          <li
            key={movie.sessionMovieId}
            className="p-4 bg-[#2F0740] rounded-2xl shadow-md border border-[#710973] hover:bg-[#F244D5]/50 transition-all"
          >
            <p className="font-semibold text-lg text-[#F244D5]">
              {movie.movieTitle}
            </p>
            <p className="text-[#F2F2E9]">
              <span className="font-medium">Avg Rating:</span>{" "}
              {movie.averageRating}
            </p>
            <p className="text-[#F2F2E9]">
              <span className="font-medium">Added by:</span>{" "}
              {movie.addedByUserName}
            </p>
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

export default WatchedMoviesTab;
