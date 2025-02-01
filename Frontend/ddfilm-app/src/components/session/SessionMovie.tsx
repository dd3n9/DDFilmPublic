import { Link } from "react-router-dom";
import { PATH_PRIVATE } from "../../routes/paths";

interface SessionMenuProps {
  isOpen: boolean;
  toggleMenu: () => void;
}

const SessionMenu: React.FC<SessionMenuProps> = ({ isOpen, toggleMenu }) => {
  return (
    <>
      <button
        onClick={toggleMenu}
        className="block py-2 px-3 text-[#F2F2F2] hover:text-[#BACBD9] transition-all"
      >
        Sessions
      </button>
      {isOpen && (
        <div className="absolute top-full mt-2 left-0 bg-[#223440] text-[#F2F2F2] rounded-lg shadow-lg">
          <ul className="py-2">
            <li>
              <Link
                to={PATH_PRIVATE.allSessions}
                className="block px-4 py-2 text-sm hover:bg-[#5D7A8C] transition-all"
              >
                All Sessions
              </Link>
            </li>
            <li>
              <Link
                to={PATH_PRIVATE.mySessions}
                className="block px-4 py-2 text-sm hover:bg-[#5D7A8C] transition-all"
              >
                My Sessions
              </Link>
            </li>
          </ul>
        </div>
      )}
    </>
  );
};

export default SessionMenu;
