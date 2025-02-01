import { AxiosResponse } from "axios";
import {
  PaginationRequestParams,
  PaginationResult,
} from "../../models/pagination";
import { ITmdbMovieDto } from "../../models/movieDto";
import tmdbAxiosInstance from "../instances/tmdbAxiosInstance";
import { handleError } from "../../helpers/ErrorHandler";

const tmdbApiConnector = {
  getPopularMovies: async (
    paginationRequestParams: PaginationRequestParams
  ): Promise<PaginationResult<ITmdbMovieDto[]>> => {
    try {
      const response: AxiosResponse<PaginationResult<ITmdbMovieDto[]>> =
        await tmdbAxiosInstance.get("/trending/all/week?language=en-US", {
          params: {
            page: paginationRequestParams.pageNumber,
            pageSize: paginationRequestParams.pageSize,
          },
        });

      return response.data;
    } catch (error) {
      console.error("Error fetching popular movies:", error);
      return {
        results: [],
        paginationParams: {
          totalItems: 0,
          totalPages: 0,
          currentPage: 0,
          itemsPerPage: 0,
        },
      };
    }
  },
  searchMovies: async (
    query: string,
    paginationRequestParams: PaginationRequestParams
  ): Promise<PaginationResult<ITmdbMovieDto[]>> => {
    try {
      const response = await tmdbAxiosInstance.get("/search/movie", {
        params: {
          query,
          page: paginationRequestParams.pageNumber,
        },
      });
      return response.data;
    } catch (error) {
      console.error("Error searching movies:", error);
      return {
        results: [],
        paginationParams: {
          totalItems: 0,
          totalPages: 0,
          currentPage: 0,
          itemsPerPage: 0,
        },
      };
    }
  },
  getMovieDetails: async (tmdbId: number): Promise<ITmdbMovieDto> => {
    try {
      const response = await tmdbAxiosInstance.get(`/movie/${tmdbId}`);
      return response.data;
    } catch (error) {
      handleError(error);
      throw error;
    }
  },
};
export default tmdbApiConnector;
