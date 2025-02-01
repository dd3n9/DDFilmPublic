import { useState } from "react";
import Modal from "../common/Modal";

interface SessionLoginModalProps {
  sessionId: string;
  onClose: () => void;
  onLogin: (sessionId: string, password: string) => void;
}

const SessionLoginModal: React.FC<SessionLoginModalProps> = ({
  sessionId,
  onClose,
  onLogin,
}) => {
  const [password, setPassword] = useState("");

  const handleLogin = () => {
    onLogin(sessionId, password);
  };

  return (
    <Modal onClose={onClose}>
      <h2 className="text-2xl font-bold text-[#F244D5] mb-6 text-center">
        Join Session
      </h2>
      <p className="text-white text-sm text-center mb-4">
        Enter the password to join:
      </p>
      <input
        type="password"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
        placeholder="Password"
        className="w-full px-5 py-3 rounded-full bg-[#1E0A29] text-white placeholder-[#710973] focus:outline-none focus:ring-4 focus:ring-[#F244D5] transition-all"
      />
      <div className="flex justify-center mt-6">
        <button
          onClick={handleLogin}
          className="px-5 py-2 bg-gradient-to-r from-[#F244D5] to-[#710973] text-white rounded-full hover:shadow-lg transition-all"
        >
          Confirm
        </button>
      </div>
    </Modal>
  );
};

export default SessionLoginModal;
