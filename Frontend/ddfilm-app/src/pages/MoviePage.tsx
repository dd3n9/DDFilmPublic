import React, { useEffect, useState } from "react";
import { PaginationRequestParams } from "../models/pagination";
import "../App.css";
import MovieList from "../components/movie/MovieList";
import { ITmdbMovieDto } from "../models/movieDto";
import { useSearch } from "../context/SearchContext";
import { useNavigate } from "react-router-dom";
import { PATH_PUBLIC } from "../routes/paths";
import Pagination from "../components/common/Pagination";
import Spinner from "../components/common/Spinner";
import tmdbApiConnector from "../api/services/tmdbApiConnector";

const MoviePage: React.FC = () => {
  const navigate = useNavigate();
  const { searchQuery } = useSearch();
  const [movies, setMovies] = useState<ITmdbMovieDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [notFound, setNotFound] = useState(false);

  const [pagination, setPagination] = useState<PaginationRequestParams>({
    pageNumber: 1,
    pageSize: 20,
  });
  const [paginationMeta, setPaginationMeta] = useState<{
    currentPage: number;
    totalPages: number;
  } | null>(null);

  const handleCardClick = async (movieId: number) => {
    const movieDetailsPath = PATH_PUBLIC.movieDetails.replace(
      ":id",
      movieId.toString()
    );
    navigate(movieDetailsPath);
  };

  useEffect(() => {
    setPagination((prev) => ({ ...prev, pageNumber: 1 }));
  }, [searchQuery]);

  useEffect(() => {
    const fetchMovies = async () => {
      setLoading(true);
      setNotFound(false);
      try {
        const response = searchQuery
          ? await tmdbApiConnector.searchMovies(searchQuery, pagination)
          : await tmdbApiConnector.getPopularMovies(pagination);

        if (response.results.length > 0) {
          setMovies(response.results);
          setPaginationMeta({
            currentPage: response.paginationParams?.currentPage ?? 1,
            totalPages: response.paginationParams?.totalPages ?? 1,
          });
        } else {
          setNotFound(true);
        }
      } catch (error) {
        console.error("Error fetching movies:", error);
        setNotFound(true);
      } finally {
        setLoading(false);
      }
    };

    fetchMovies();
  }, [searchQuery, pagination]);

  const handlePageChange = (newPage: number) => {
    setPagination((prev) => ({ ...prev, pageNumber: newPage }));
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
      <div className="text-center text-2xl text-[#F244D5]">
        No movies found.
      </div>
    );
  }

  return (
    <div className="container mx-auto mt-6 max-w-5xl overflow-hidden">
      <MovieList
        movies={movies}
        onCardClick={(movieId) => handleCardClick(movieId)}
      />
      {paginationMeta && (
        <div className="my-4">
          <Pagination
            currentPage={pagination.pageNumber}
            totalPages={paginationMeta.totalPages}
            onPageChange={handlePageChange}
          />
        </div>
      )}
    </div>
  );
};

export default MoviePage;
