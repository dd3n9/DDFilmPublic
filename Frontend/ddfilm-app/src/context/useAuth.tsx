import React, { createContext, useEffect, useState } from "react";
import { UserProfile } from "../models/User";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import { loginAPI, registerAPI } from "../services/AuthService";
import { toast } from "react-toastify";
import { ILoginDto, IRegisterDto, UserProfileToken } from "../models/auth";
import { PATH_PUBLIC } from "../routes/paths";

type UserContextType = {
  user: UserProfile | null;
  token: string | null;
  registerUser: (registerDto: IRegisterDto) => void;
  loginUser: (loginDto: ILoginDto) => void;
  logout: () => void;
  isLoggedIn: () => boolean;
};

type Props = { children: React.ReactNode };

const UserContext = createContext<UserContextType>({} as UserContextType);

export const UserProvider = ({ children }: Props) => {
  const navigate = useNavigate();
  const [token, setToken] = useState<string | null>(null);
  const [user, setUser] = useState<UserProfile | null>(null);
  const [isReady, setIsReady] = useState(false);

  useEffect(() => {
    const user = localStorage.getItem("user");
    const token = localStorage.getItem("token");
    if (user && token) {
      setUser(JSON.parse(user));
      setToken(token);
      axios.defaults.headers.common["Authorization"] = "Bearer " + token;
    }
    setIsReady(true);
  }, []);

  const registerUser = async (registerDto: IRegisterDto) => {
    await registerAPI(registerDto)
      .then((res) => {
        if (res) {
          toast.success("Register Success!");
          navigate(PATH_PUBLIC.movie);
        }
      })
      .catch((e) => toast.warning("Server error occured"));
  };

  const loginUser = async (loginDTO: ILoginDto) => {
    await loginAPI(loginDTO)
      .then((res) => {
        if (res) {
          localStorage.setItem("token", res?.data.token);

          const userObj: UserProfile = {
            userId: res?.data.authenticationDto.userId,
            firstName: res?.data.authenticationDto.firstName,
            lastName: res?.data.authenticationDto.lastName,
            userName: res?.data.authenticationDto.userName,
          };

          localStorage.setItem("user", JSON.stringify(userObj));
          setToken(res?.data.token!);
          setUser(userObj);
          toast.success("Login Success!");
          navigate(PATH_PUBLIC.movie);
        }
      })
      .catch((e) => toast.warning("Server error occured"));
  };

  const isLoggedIn = () => {
    return !!user;
  };

  const logout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("user");
    setUser(null);
    setToken("");
    navigate("/");
  };

  return (
    <UserContext.Provider
      value={{ loginUser, user, token, logout, isLoggedIn, registerUser }}
    >
      {isReady ? children : null}
    </UserContext.Provider>
  );
};

export const useAuth = () => React.useContext(UserContext);
