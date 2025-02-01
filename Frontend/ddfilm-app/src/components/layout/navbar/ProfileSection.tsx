import { Link } from "react-router-dom";
import { useState } from "react";
import { useAuth } from "../../../context/useAuth";
import { PATH_PUBLIC } from "../../../routes/paths";

const ProfileSection: React.FC = () => {
  const { isLoggedIn, user, logout } = useAuth();
  const [isDropdownOpen, setIsDropdownOpen] = useState(false);

  const toggleDropdown = () => {
    setIsDropdownOpen((prev) => !prev);
  };

  const closeDropdown = () => {
    setIsDropdownOpen(false);
  };

  return (
    <div className="flex items-center md:order-2">
      {isLoggedIn() ? (
        <div className="relative">
          <button
            type="button"
            className="flex items-center text-sm bg-[#1D1340] text-white rounded-full p-1.5 focus:ring-4 focus:ring-[#5D7A8C]"
            aria-expanded={isDropdownOpen ? "true" : "false"}
            onClick={toggleDropdown}
          >
            <span className="sr-only">Open user menu</span>
            <div className="w-8 h-8 flex items-center justify-center rounded-full bg-[#5D7A8C] text-[#223440] font-bold">
              {user?.userName?.[0]?.toUpperCase() || "U"}
            </div>
            <span className="ml-2 text-[#F2F2F2]">{user?.userName}</span>
          </button>

          {isDropdownOpen && (
            <div
              className="absolute right-0 mt-2 z-50 w-48 bg-[#F2F2F2] divide-y divide-gray-100 rounded-lg shadow dark:bg-[#1D1340] dark:divide-[#5D7A8C]"
              onMouseLeave={closeDropdown}
            >
              <ul className="py-2">
                <li>
                  <button
                    onClick={logout}
                    className="w-full text-left px-4 py-2 text-sm text-[#223440] hover:bg-[#BACBD9] dark:text-[#F2F2F2] dark:hover:bg-[#5D7A8C]"
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
          className="text-white bg-[#5D7A8C] hover:bg-[#BACBD9] font-medium rounded-lg text-sm px-4 py-2 focus:outline-none focus:ring-2 focus:ring-[#1D1340]"
        >
          Log In
        </Link>
      )}
    </div>
  );
};

export default ProfileSection;
