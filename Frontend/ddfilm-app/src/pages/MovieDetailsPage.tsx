import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { ITmdbMovieDto } from "../models/movieDto";
import Spinner from "../components/common/Spinner";
import { toast } from "react-toastify";
import tmdbApiConnector from "../api/services/tmdbApiConnector";
import apiConnector from "../api/services/apiConnector";
import AddMovieToSessionModal from "../components/modals/AddMovieToSessionModal";

const MovieDetailsPage: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const [movie, setMovie] = useState<ITmdbMovieDto | null>(null);
  const [loading, setLoading] = useState(true);
  const [notFound, setNotFound] = useState(false);
  const [isModalOpen, setModalOpen] = useState(false);
  const [sessions, setSessions] = useState<string[]>([]);

  useEffect(() => {
    const fetchMovieDetails = async () => {
      setLoading(true);
      setNotFound(false);
      try {
        const movieDetails = await tmdbApiConnector.getMovieDetails(Number(id));
        setMovie(movieDetails);
      } catch (err) {
        setNotFound(true);
        console.error("Error fetching movie details:", err);
      } finally {
        setLoading(false);
      }
    };

    fetchMovieDetails();
  }, [id]);

  const openModal = async () => {
    try {
      setModalOpen(true);
    } catch (error) {
      console.error("Failed to fetch sessions", error);
    }
  };

  const closeModal = () => {
    setModalOpen(false);
  };

  const handleAddToSession = async (sessionId: string) => {
    if (!movie) return;

    try {
      const response = await apiConnector.addSessionMovie(
        sessionId,
        movie.id.toString(),
        movie.title
      );
      closeModal();
      toast.success("Movie was successfully added to session!");
    } catch (error) {
      closeModal();
    }
  };

  if (loading) {
    return (
      <div className="flex items-center justify-center h-screen">
        <Spinner></Spinner>
      </div>
    );
  }

  if (notFound || !movie) {
    return (
      <div className="text-center text-2xl text-[#F244D5]">
        Movie not found.
      </div>
    );
  }

  return (
    <div className="container mx-auto mt-8 p-6 bg-gradient-to-b from-[#2F0740] to-[#140626] rounded-3xl shadow-lg text-white max-w-4xl">
      <div className="flex flex-col md:flex-row items-center md:items-start">
        {/* Poster */}
        <div className="flex-shrink-0 mb-6 md:mb-0 md:mr-6">
          <img
            src={`https://image.tmdb.org/t/p/w500${movie.poster_path}`}
            alt={movie.title}
            className="rounded-lg shadow-xl max-w-xs"
          />
        </div>

        {/* Movie Info */}
        <div className="flex-grow">
          <h1 className="text-3xl font-bold mb-4 text-center md:text-left">
            {movie.title}
          </h1>
          <p className="text-gray-300 mb-4">{movie.overview}</p>
          <div className="flex flex-col md:flex-row justify-between text-sm text-gray-400 mb-6">
            <p>
              <strong className="text-[#F244D5]">Release Date:</strong>{" "}
              {movie.release_date}
            </p>
            <p>
              <strong className="text-[#F244D5]">Rating:</strong>{" "}
              {movie.vote_average}/10
            </p>
          </div>
          <button
            className="w-full md:w-auto py-3 px-5 bg-gradient-to-r from-[#F244D5] to-[#710973] text-white font-medium rounded-full hover:shadow-lg transition-all"
            onClick={openModal}
          >
            Add to Session
          </button>
        </div>
      </div>

      <AddMovieToSessionModal
        movieTitle={movie.title}
        isOpen={isModalOpen}
        onClose={closeModal}
        onAddToSession={handleAddToSession}
      />
    </div>
  );
};

export default MovieDetailsPage;
