import axios, { AxiosResponse } from "axios";
import { PaginationResult } from "../../models/pagination";

const tmdbAxiosInstance = axios.create({
  baseURL: "https://api.themoviedb.org/3",
  params: {
    api_key: process.env.REACT_APP_TMDB_API_KEY,
  },
});

let isInterceptorSetup = false;

export const setupResponseInterceptor = () => {
  if (!isInterceptorSetup) {
    tmdbAxiosInstance.interceptors.response.use(
      (response: AxiosResponse) => {
        if (response.data && response.data.results) {
          const paginationParams = {
            currentPage: response.data.page,
            totalPages: response.data.total_pages,
            totalItems: response.data.total_results,
            itemsPerPage: response.data.results.length,
          };

          response.data = new PaginationResult(
            response.data.results,
            paginationParams
          );
        }
        return response;
      },
      (error) => {
        if (error.response) {
          const statusCode = error.response.status;
          const data = error.response.data;

          switch (statusCode) {
            case 400:
              if (data.errors) {
                const modalStateErrors = [];

                for (const errorItem of data.errors) {
                  const property = errorItem.property;
                  const errorMessage = errorItem.errorMessage;

                  if (property && errorMessage) {
                    modalStateErrors.push({ property, errorMessage });
                  }
                }

                console.log(modalStateErrors);
              }
              break;
            case 401:
              console.log("Unauthorized access");
              break;
            case 403:
              console.log("Forbidden access");
              break;
            case 404:
              console.log("Not found");
              break;
            default:
              console.log("Generic error");
              break;
          }
        }

        return Promise.reject(error);
      }
    );
    isInterceptorSetup = true;
  }
};

setupResponseInterceptor();

export default tmdbAxiosInstance;
