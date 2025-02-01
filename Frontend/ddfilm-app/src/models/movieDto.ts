export interface ITmdbMovieDto {
  backdrop_path: string;
  id: number;
  original_language: string;
  original_title: string;
  overview: string;
  poster_path: string;
  release_date: string;
  title: string;
  name: string;
  vote_average: number;
}

export interface IMovie {
  id: number;
  title: string;
  poster_path: string;
  vote_average: number;
}
