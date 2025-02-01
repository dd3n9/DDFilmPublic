import { title } from "process";
import React, { useEffect, useState } from "react";

interface MovieProps {
  movie: {
    id: number;
    title: string;
    poster_path: string;
    vote_average: number;
  };
  onClick: () => void;
}

const MovieCard: React.FC<MovieProps> = ({ movie, onClick }) => {
  return (
    <div
      className="bg-[#710973] text-[#F2F2E9] border border-[#710973] rounded-3xl shadow-lg transform transition-transform duration-300 hover:scale-105 hover:shadow-xl"
      onClick={onClick}
    >
      <div className="relative w-full h-0 pb-[150%]">
        <img
          src={`https://image.tmdb.org/t/p/w500${movie.poster_path}`}
          alt={movie.title}
          className="absolute top-2 left-2 w-[calc(100%-16px)] h-[calc(100%-16px)] object-cover rounded-2xl"
        />
      </div>
      <div className="p-4">
        <h3 className="text-lg font-bold truncate">{movie.title}</h3>
        <p className="text-sm">Rating: {movie.vote_average.toFixed(1)}</p>
      </div>
    </div>
  );
};

export default MovieCard;
