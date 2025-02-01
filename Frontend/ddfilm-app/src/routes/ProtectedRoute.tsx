import { Navigate, useLocation } from "react-router-dom";
import { useAuth } from "../context/useAuth";
import { PATH_PUBLIC } from "./paths";

type Props = { children: React.ReactNode };

const ProtectedRoute = ({ children }: Props) => {
  const location = useLocation();
  const { isLoggedIn } = useAuth();

  return isLoggedIn() ? (
    <>{children}</>
  ) : (
    <Navigate
      to={PATH_PUBLIC.login}
      state={{ from: location }}
      replace
    ></Navigate>
  );
};

export default ProtectedRoute;
