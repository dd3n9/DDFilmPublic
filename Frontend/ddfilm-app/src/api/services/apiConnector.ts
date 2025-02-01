import { AxiosResponse } from "axios";
import { string } from "yup";
import {
  PaginationRequestParams,
  PaginationResult,
} from "../../models/pagination";
import {
  ISessionDto,
  ISessionMovieDto,
  ISessionParticipantDto,
} from "../../models/sessionDto";
import axiosInstance from "../instances/axiosInstance";
import { SESSION_URL } from "../../utils/globalConfig";
import { handleError } from "../../helpers/ErrorHandler";

const apiConnector = {
  getAllSessions: async (
    paginationRequestParams: PaginationRequestParams
  ): Promise<PaginationResult<ISessionDto[]>> => {
    try {
      const response: AxiosResponse<PaginationResult<ISessionDto[]>> =
        await axiosInstance.get(SESSION_URL, {
          params: {
            pageNumber: paginationRequestParams.pageNumber,
            pageSize: paginationRequestParams.pageSize,
          },
        });

      return response.data;
    } catch (error) {
      console.error("Error fetching sessions:", error);
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
  loginSession: async (sessionId: string, password: string) => {
    try {
      const url = `${SESSION_URL}/${sessionId}/login`;
      const response = await axiosInstance.post(url, { password });
      return response;
    } catch (error) {
      handleError(error);
    }
  },
  logoutSession: async (sessionId: string) => {
    try {
      const url = `${SESSION_URL}/${sessionId}/logout`;
      const response = await axiosInstance.delete(url);
      return response;
    } catch (error) {
      handleError(error);
    }
  },
  getMySessions: async (
    paginationRequestParams: PaginationRequestParams
  ): Promise<PaginationResult<ISessionDto[]>> => {
    try {
      const response: AxiosResponse<PaginationResult<ISessionDto[]>> =
        await axiosInstance.get(`${SESSION_URL}/my-sessions`, {
          params: {
            pageNumber: paginationRequestParams.pageNumber,
            pageSize: paginationRequestParams.pageSize,
          },
        });

      return response.data;
    } catch (error) {
      console.error("Error fetching sessions:", error);
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
  createSession: async (sessionName: string, password: string) => {
    try {
      const url = `${SESSION_URL}`;
      const response = await axiosInstance.post(url, {
        sessionName: sessionName,
        password: password,
      });
      return response;
    } catch (error) {
      handleError(error);
    }
  },
  getWatchedSessionMovies: async (
    sessionId: string,
    paginationRequestParams: PaginationRequestParams
  ): Promise<PaginationResult<ISessionMovieDto[]>> => {
    try {
      const url = `${SESSION_URL}/${sessionId}/movies/watched`;

      const response: AxiosResponse<PaginationResult<ISessionMovieDto[]>> =
        await axiosInstance.get(url, {
          params: {
            pageNumber: paginationRequestParams.pageNumber,
            pageSize: paginationRequestParams.pageSize,
          },
        });
      console.log(response.data);
      return response.data;
    } catch (error) {
      handleError(error);
      throw error;
    }
  },
  getWatchingSessionMovie: async (
    sessionId: string
  ): Promise<ISessionMovieDto> => {
    try {
      const url = `${SESSION_URL}/${sessionId}/movies/watching`;

      const response = await axiosInstance.get(url);
      console.log(response.data);
      return response.data;
    } catch (error) {
      handleError(error);
      throw error;
    }
  },
  getUnwatchedSessionMovies: async (
    sessionId: string,
    paginationRequestParams: PaginationRequestParams
  ): Promise<PaginationResult<ISessionMovieDto[]>> => {
    try {
      const url = `${SESSION_URL}/${sessionId}/movies/unwatched`;

      const response: AxiosResponse<PaginationResult<ISessionMovieDto[]>> =
        await axiosInstance.get(url, {
          params: {
            pageNumber: paginationRequestParams.pageNumber,
            pageSize: paginationRequestParams.pageSize,
          },
        });
      console.log(response.data);
      return response.data;
    } catch (error) {
      handleError(error);
      throw error;
    }
  },
  getAllUnwatchedSessionMovies: async (
    sessionId: string
  ): Promise<ISessionMovieDto[]> => {
    try {
      const url = `${SESSION_URL}/${sessionId}/movies/all-unwatched`;

      const response: AxiosResponse<ISessionMovieDto[]> =
        await axiosInstance.get(url);
      console.log(response.data);
      return response.data;
    } catch (error) {
      handleError(error);
      throw error;
    }
  },
  getSessionParticipants: async (
    sessionId: string
  ): Promise<ISessionParticipantDto[]> => {
    try {
      const url = `${SESSION_URL}/${sessionId}/participants`;

      const response: AxiosResponse<ISessionParticipantDto[]> =
        await axiosInstance.get(url);
      console.log(response.data);
      return response.data;
    } catch (error) {
      handleError(error);
      throw error;
    }
  },
  addSessionMovie: async (
    sessionId: string,
    tmdbId: string,
    movieTitle: string
  ) => {
    try {
      const url = `${SESSION_URL}/${sessionId}/movies`;

      const response = await axiosInstance.post(url, {
        tmdbId: tmdbId,
        movieTitle: movieTitle,
      });
      return response;
    } catch (error) {
      handleError(error);
      throw error;
    }
  },
  deleteSessionMovie: async (sessionId: string, tmdbId: number) => {
    try {
      const url = `${SESSION_URL}/${sessionId}/movies/${tmdbId}`;
      const response = await axiosInstance.delete(url);
      return response;
    } catch (error) {
      handleError(error);
    }
  },
  chooseMovie: async (sessionId: string): Promise<string> => {
    try {
      const url = `${SESSION_URL}/${sessionId}/movies/chooseMovie`;

      const response = await axiosInstance.post(url, sessionId);
      console.log(response.data);
      return response.data.sessionMovieId;
    } catch (error) {
      handleError(error);
      throw error;
    }
  },
};

export default apiConnector;
