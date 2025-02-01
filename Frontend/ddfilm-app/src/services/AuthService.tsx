import axiosInstance from "../api/instances/axiosInstance";
import { handleError } from "../helpers/ErrorHandler";
import { ILoginDto, IRegisterDto, UserProfileToken } from "../models/auth";
import { LOGIN_URL, REGISTER_URL } from "../utils/globalConfig";

export const loginAPI = async (loginDto: ILoginDto) => {
  try {
    const data = await axiosInstance.post<UserProfileToken>(
      process.env.REACT_APP_HOST_API + LOGIN_URL,
      {
        email: loginDto.email,
        password: loginDto.password,
      }
    );
    return data;
  } catch (error) {
    handleError(error);
  }
};

export const registerAPI = async (registerDto: IRegisterDto) => {
  try {
    const data = await axiosInstance.post<UserProfileToken>(
      process.env.REACT_APP_HOST_API + REGISTER_URL,
      {
        username: registerDto.userName,
        password: registerDto.password,
        firstName: registerDto.firstName,
        lastName: registerDto.lastName,
        email: registerDto.email,
      }
    );
    return data;
  } catch (error) {
    handleError(error);
  }
};
