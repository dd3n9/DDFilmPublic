import { ITmdbMovieDto } from "./movieDto";

export interface ITmdbApiResponse {
  results: ITmdbMovieDto[];
  total_pages: number;
  page: number;
  total_results: number;
}
