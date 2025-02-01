import { handleError } from "../../helpers/ErrorHandler";
import { MOVIES_URL } from "../../utils/globalConfig";
import axiosInstance from "../instances/axiosInstance";

const apiMovieConnector = {
  rateMovie: async (rating: number, tmdbId: number) => {
    try {
      const url = `${MOVIES_URL}/${tmdbId}`;

      const response = await axiosInstance.post(url, rating, {
        headers: {
          "Content-Type": "application/json",
        },
      });
      console.log(response);
      return response;
    } catch (error) {
      handleError(error);
      throw error;
    }
  },
};

export default apiMovieConnector;
