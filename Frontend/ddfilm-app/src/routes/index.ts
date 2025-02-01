import LoginPage from "../pages/LoginPage";
import MoviePage from "../pages/MoviePage";
import { PATH_PRIVATE, PATH_PUBLIC } from "./paths";
import RegisterPage from "../pages/RegisterPage";
import SessionsPage from "../pages/SessionPage";
import { Route } from "react-router-dom";
import SessionDetailsPage from "../pages/SessionDetailsPage";
import MovieDetailsPage from "../pages/MovieDetailsPage";

export const privateRoutes = [
  { path: PATH_PRIVATE.allSessions, component: SessionsPage },
  { path: PATH_PRIVATE.mySessions, component: SessionsPage },
  { path: PATH_PRIVATE.sessionDetails, component: SessionDetailsPage }
];

export const publicRoutes = [
  { path: PATH_PUBLIC.login, component: LoginPage },
  { path: PATH_PUBLIC.register, component: RegisterPage },
  { path: PATH_PUBLIC.movie, component: MoviePage },
  { path: PATH_PUBLIC.movieDetails, component: MovieDetailsPage },
];
