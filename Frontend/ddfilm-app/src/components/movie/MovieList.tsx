import React, { useEffect, useState } from "react";
import MovieCard from "./MovieCard";
import { PaginationRequestParams } from "../../models/pagination";
import { CSSTransition, TransitionGroup } from "react-transition-group";
import "../../App.css";
import Pagination from "../common/Pagination";
import { IMovie, ITmdbMovieDto } from "../../models/movieDto";

interface MovieListProps {
  movies: ITmdbMovieDto[];
  onCardClick: (movieId: number) => void;
}

const MovieList: React.FC<MovieListProps> = ({ movies, onCardClick }) => {
  return (
    <div className="bg-[#710973]/30 p-6 rounded-2xl shadow-md max-w-4xl mx-auto">
      <TransitionGroup className="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-4 gap-6">
        {movies.map((movie) => (
          <CSSTransition key={movie.id} timeout={500} classNames="fade">
            <MovieCard
              movie={{ ...movie, title: movie.title || movie.name }}
              onClick={() => onCardClick(movie.id)}
            />
          </CSSTransition>
        ))}
      </TransitionGroup>
    </div>
  );
};

export default MovieList;
