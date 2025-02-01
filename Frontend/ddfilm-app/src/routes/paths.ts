import SessionDetailsPage from "../pages/SessionDetailsPage";

export const PATH_PUBLIC = {
  register: "/account/register",
  login: "/account/login",
  notFound: "/404",
  movie: "/movies",
  movieDetails: "/movies/:id",
};

export const PATH_PRIVATE = {
  allSessions: "/sessions",
  mySessions: "/sessions/my",
  sessionDetails: "/sessions/details/:id",
};
