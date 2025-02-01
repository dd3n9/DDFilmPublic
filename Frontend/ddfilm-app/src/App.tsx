import React, { useEffect, useState } from "react";
import "./App.css";
import MovieList from "./components/movie/MovieList";
import {
  BrowserRouter as Router,
  Routes,
  Route,
  BrowserRouter,
  useNavigate,
} from "react-router-dom";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { UserProvider } from "./context/useAuth";
import AppRouter from "./pages/AppRouter";
import { SearchProvider } from "./context/SearchContext";
import Navbar from "./components/layout/navbar/Navbar";
import ParticleBackground from "./components/common/ParticleBackground";

function App() {
  return (
    <div className="min-h-screen">
      <ParticleBackground />
      <BrowserRouter>
        <UserProvider>
          <SearchProvider>
            <Navbar />
            <AppRouter></AppRouter>
            <ToastContainer theme="dark" />
          </SearchProvider>
        </UserProvider>
      </BrowserRouter>
    </div>
  );
}

export default App;
