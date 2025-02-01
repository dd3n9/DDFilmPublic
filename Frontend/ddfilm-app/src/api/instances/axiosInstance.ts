import axios, { AxiosResponse } from "axios";
import { REFRESH_TOKEN_URL } from "../../utils/globalConfig";
import { PaginationResult } from "../../models/pagination";

const axiosInstance = axios.create({
  baseURL: process.env.REACT_APP_HOST_API,
  withCredentials: true,
});

let isInterceptorSetup = false;

export const setupResponseInterceptor = () => {
  if (!isInterceptorSetup) {
    axiosInstance.interceptors.request.use(
      (config) => {
        const token = localStorage.getItem("token");
        if (token) {
          config.headers["Authorization"] = "Bearer " + token;
        }
        return config;
      },
      (error) => Promise.reject(error)
    );
    axiosInstance.interceptors.response.use(
      (response: AxiosResponse) => {
        const paginationParams = response.headers["x-pagination"];

        if (paginationParams) {
          response.data = new PaginationResult(
            response.data,
            JSON.parse(paginationParams)
          );
          return response as AxiosResponse<PaginationResult<any>>;
        }

        return response;
      },
      async (error) => {
        const originalRequest = error.config;
        if (error.response.status == 401 && !originalRequest._retry) {
          originalRequest._retry = true;
          try {
            const response = await axios.post(
              process.env.REACT_APP_HOST_API + REFRESH_TOKEN_URL,
              {},
              { withCredentials: true }
            );
            localStorage.setItem("token", response.data);
          } catch {
            console.log(error);
          }
          originalRequest.headers[
            "Authorization"
          ] = `Bearer ${localStorage.getItem("token")}`;
          return axiosInstance.request(originalRequest);
        }
        return Promise.reject(error);
      }
    );

    isInterceptorSetup = true;
  }
};

setupResponseInterceptor();

export default axiosInstance;
