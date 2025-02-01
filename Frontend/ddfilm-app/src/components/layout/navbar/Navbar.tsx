import { Link } from "react-router-dom";
import { useSearch } from "../../../context/SearchContext";
import { useAuth } from "../../../context/useAuth";
import SearchBar from "./SearchBar";
import { useState } from "react";
import { PATH_PRIVATE, PATH_PUBLIC } from "../../../routes/paths";

const Navbar: React.FC = () => {
  const { setSearchQuery } = useSearch();
  const { isLoggedIn, user, logout } = useAuth();
  const [isSessionMenuOpen, setSessionMenuOpen] = useState(false);

  const handleSearch = (query: string) => {
    setSearchQuery(query);
  };

  const [isMobileMenuOpen, setMobileMenuOpen] = useState(false);
  const [isDropdownOpen, setDropdownOpen] = useState(false);

  const toggleDropdown = () => setDropdownOpen(!isDropdownOpen);
  const closeDropdown = () => setDropdownOpen(false);
  const toggleSessionMenu = () => setSessionMenuOpen(!isSessionMenuOpen);
  const closeSessionMenu = () => setSessionMenuOpen(false);
  const toggleMobileMenu = () => setMobileMenuOpen(!isMobileMenuOpen);
  const closeMobileMenu = () => setMobileMenuOpen(false);
  return (
    <nav className="bg-[#0D0D0D] sticky top-0 z-50">
      <div className="max-w-screen-xl flex flex-wrap items-center justify-between mx-auto p-4">
        <a href="#" className="flex items-center">
          <Link
            to={PATH_PUBLIC.movie}
            className="self-center text-2xl font-semibold text-[#F244D5]"
          >
            DDFilm
          </Link>
        </a>

        {/* Mobile menu button (Hamburger icon) */}
        <div className="flex md:hidden order-2">
          {" "}
          {/* Mobile order and show on mobile only */}
          <button
            onClick={toggleMobileMenu}
            type="button"
            className="inline-flex items-center p-2 w-10 h-10 justify-center text-sm text-gray-500 rounded-lg hover:bg-gray-100 focus:outline-none focus:ring-2 focus:ring-gray-200 dark:text-gray-400 dark:hover:bg-gray-700 dark:focus:ring-gray-600"
            aria-controls="mobile-menu"
            aria-expanded={isMobileMenuOpen ? "true" : "false"}
          >
            <span className="sr-only">Open main menu</span>
            <svg
              className="w-5 h-5"
              aria-hidden="true"
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 17 14"
            >
              <path
                stroke="currentColor"
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth="2"
                d="M1 1h15M1 7h15M1 13h15"
              />
            </svg>
          </button>
        </div>

        <div className="flex items-center md:order-last order-3 md:order-2">
          {isLoggedIn() ? (
            <div className="relative">
              <button
                type="button"
                className="flex items-center text-sm bg-[#2F0740] text-white rounded-full p-2 focus:ring-4 focus:ring-[#710973]"
                id="user-menu-button"
                aria-expanded={isDropdownOpen ? "true" : "false"}
                onClick={toggleDropdown}
              >
                <span className="sr-only">Open user menu</span>
                <div className="w-8 h-8 flex items-center justify-center rounded-full bg-[#F244D5] text-[#140626] font-bold">
                  {user?.userName?.[0]?.toUpperCase() || "U"}
                </div>
                <span className="ml-2 text-[#F244D5]">{user?.userName}</span>
              </button>

              {/* Dropdown Menu */}
              {isDropdownOpen && (
                <div className="absolute right-0 mt-2 z-50 w-48 text-base list-none bg-[#2F0740] divide-y divide-[#710973] rounded-lg shadow">
                  <ul className="py-2" onMouseLeave={closeDropdown}>
                    <li>
                      <button
                        onClick={logout}
                        className="w-full text-left block px-4 py-2 text-sm text-[#F244D5] hover:bg-[#710973] hover:text-white"
                      >
                        Log Out
                      </button>
                    </li>
                  </ul>
                </div>
              )}
            </div>
          ) : (
            <Link
              to={PATH_PUBLIC.login}
              className="px-4 py-2 bg-[#710973] text-white rounded-full hover:bg-[#F244D5] transition-all"
            >
              Log In
            </Link>
          )}
        </div>

        {/* Navigation Links - Hidden on mobile, flex on desktop */}
        <div
          className={`items-center justify-between w-full md:flex md:w-auto order-1 md:order-1 ${
            isMobileMenuOpen ? "flex" : "hidden"
          }`}
          id="mobile-menu"
        >
          <ul className="flex flex-col font-medium p-4 md:p-0 mt-4 border border-[#0D0D0D] rounded-lg bg-[#0D0D0D] md:space-x-8 md:flex-row md:mt-0 md:border-0">
            <li className="flex items-center">
              <SearchBar onSearch={handleSearch} />
            </li>

            <li>
              <Link
                to={PATH_PUBLIC.movie}
                className="block py-2 px-3 text-[#F244D5] hover:text-[#710973] transition-all"
              >
                Movies
              </Link>
            </li>

            {/* Sessions Dropdown */}

            <li className="relative">
              <button
                onClick={toggleSessionMenu}
                className="block py-2 px-3 text-[#F244D5] hover:text-[#710973] transition-all"
              >
                Sessions
              </button>

              {isSessionMenuOpen && (
                <div
                  className="absolute top-full mt-2 left-0 bg-[#2F0740] text-[#F244D5] rounded-lg shadow-lg"
                  onMouseLeave={closeSessionMenu}
                >
                  <ul className="py-2">
                    <li>
                      <Link
                        to={`${PATH_PRIVATE.allSessions}`}
                        onClick={() => setSessionMenuOpen(false)}
                        className="block px-4 py-2 text-sm hover:bg-[#710973] hover:text-white transition-all"
                      >
                        All Sessions
                      </Link>
                    </li>
                    <li>
                      <Link
                        to={`${PATH_PRIVATE.mySessions}`}
                        onClick={() => setSessionMenuOpen(false)}
                        className="block px-4 py-2 text-sm hover:bg-[#710973] hover:text-white transition-all"
                      >
                        My Sessions
                      </Link>
                    </li>
                  </ul>
                </div>
              )}
            </li>
          </ul>
        </div>
      </div>
    </nav>
  );
};

export default Navbar;
