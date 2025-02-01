import { Navigate, Route, Routes } from "react-router-dom";
import { privateRoutes, publicRoutes } from "../routes";
import ProtectedRoute from "../routes/ProtectedRoute";
import { PATH_PUBLIC } from "../routes/paths";
import MoviePage from "./MoviePage";
import NotFoundPage from "./NotFoundPage";

const AppRouter = () => {
  return (
    <Routes>
      {publicRoutes.map((route) => (
        <Route
          key={route.path}
          path={route.path}
          element={<route.component />}
        />
      ))}
      {privateRoutes.map((route) => (
        <Route
          key={route.path}
          path={route.path}
          element={
            <ProtectedRoute>
              <route.component />
            </ProtectedRoute>
          }
        />
      ))}

      <Route path="/" element={<Navigate to={PATH_PUBLIC.movie} />} />
      <Route path="*" element={<NotFoundPage />} />
    </Routes>
  );
};

export default AppRouter;
