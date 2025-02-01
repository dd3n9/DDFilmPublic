import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { toast } from "react-toastify";
import apiConnector from "../../../api/services/apiConnector";
const RandomWheel: React.FC = () => {
  const [movies, setMovies] = useState<{ id: string; name: string }[]>([]);
  const [selectedMovie, setSelectedMovie] = useState<string | null>(null);
  const [isSpinning, setIsSpinning] = useState<boolean>(false);
  const [rotation, setRotation] = useState<number>(0);

  const { id } = useParams<{ id: string }>();

  useEffect(() => {
    const fetchUnwatchedMovies = async () => {
      try {
        if (!id) return;
        const unwatchedMovies = await apiConnector.getAllUnwatchedSessionMovies(
          id
        );
        setMovies(
          unwatchedMovies.map((movie) => ({
            id: movie.sessionMovieId,
            name: movie.movieTitle,
          }))
        );
      } catch (error) {
        console.error("Failed to fetch unwatched movies:", error);
      }
    };

    fetchUnwatchedMovies();
  }, [id, selectedMovie]);

  const startSpin = async () => {
    if (!id) return;
    setIsSpinning(true);
    setSelectedMovie(null);

    try {
      const response = await apiConnector.chooseMovie(id);
      const movieId = response;
      const movieIndex = movies.findIndex((movie) => movie.id === movieId);

      if (movieIndex !== -1) {
        const selectedMovieTitle = movies[movieIndex].name;
        setSelectedMovie(selectedMovieTitle);

        const segmentAngle = 360 / movies.length;
        const targetRotation =
          360 * 5 + 360 - movieIndex * segmentAngle + segmentAngle / 2;

        setRotation(targetRotation);

        setTimeout(() => {
          setSelectedMovie(movies[movieIndex].name);
        }, 3000);
      } else {
        toast.error("Error");
      }
    } catch (error) {
      console.error("Error spinning the wheel:", error);
    } finally {
      setTimeout(() => {
        setIsSpinning(false);
      }, 3000);
    }
  };

  return (
    <div className="flex flex-col items-center bg-gray-800 text-white p-6 rounded-lg shadow-lg">
      <h2 className="text-2xl font-bold mb-6">Random Movie Selector</h2>
      <div className="relative w-[300px] h-[300px]">
        <div
          className="absolute w-full h-full rounded-full border-4 border-white overflow-hidden"
          style={{
            transition: isSpinning ? "transform 3s ease-out" : undefined,
            transform: `rotate(${rotation}deg)`,
          }}
        >
          {movies.map((movie, index) => {
            const segmentAngle = 360 / movies.length;
            const rotateAngle = index * segmentAngle;

            return (
              <div
                key={movie.id}
                className="absolute top-0 left-0 w-full h-full"
                style={{
                  transform: `rotate(${rotateAngle}deg)`,
                  transformOrigin: "50% 50%",
                }}
              >
                <div
                  className="absolute w-full h-full"
                  style={{
                    backgroundColor: `hsl(${
                      (index * 360) / movies.length
                    }, 70%, 50%)`,
                    clipPath: "polygon(50% 50%, 100% 0, 100% 100%)",
                  }}
                >
                  <div
                    className="absolute w-full text-center text-white font-bold text-xs"
                    style={{
                      transform: `rotate(${
                        segmentAngle / 2
                      }deg) translate(90px) rotate(90deg)`,
                      transformOrigin: "0 0",
                      whiteSpace: "nowrap",
                      overflow: "hidden",
                      textOverflow: "ellipsis",
                    }}
                  >
                    {movie.name.length > 15
                      ? `${movie.name.slice(0, 15)}...`
                      : movie.name}
                  </div>
                </div>
              </div>
            );
          })}
        </div>

        <div
          className="absolute top-0 left-[50%] w-2 h-8 bg-red-500"
          style={{
            transform: "translateX(-50%)",
          }}
        ></div>

        <button
          className="absolute w-16 h-16 bg-white text-black font-bold rounded-full shadow-lg flex items-center justify-center hover:bg-gray-200 transition-all"
          style={{
            top: "50%",
            left: "50%",
            transform: "translate(-50%, -50%)",
          }}
          onClick={startSpin}
          disabled={isSpinning}
        >
          {isSpinning ? "..." : "Spin"}
        </button>
      </div>

      {!isSpinning && selectedMovie && (
        <div className="mt-6 text-center">
          <h3 className="text-lg font-semibold">
            Selected Movie:{" "}
            <span className="text-yellow-400">{selectedMovie}</span>
          </h3>
        </div>
      )}
    </div>
  );
};

export default RandomWheel;
