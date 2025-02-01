import { useState } from "react";
import Modal from "../common/Modal";

interface CreateSessionModalProps {
  onClose: () => void;
  onSubmit: (name: string, password: string) => void;
}

const CreateSessionModal: React.FC<CreateSessionModalProps> = ({
  onClose,
  onSubmit,
}) => {
  const [name, setName] = useState("");
  const [password, setPassword] = useState("");

  const handleSubmit = () => {
    if (name.trim() === "" || password.trim() === "") {
      alert("Name and password are required.");
      return;
    }
    onSubmit(name, password);
    setName("");
    setPassword("");
  };

  return (
    <Modal onClose={onClose}>
      <h2 className="text-2xl font-bold text-[#F244D5] mb-6 text-center">
        Create Session
      </h2>
      <div className="mb-4">
        <label className="block mb-2 text-sm font-medium text-[#F244D5]">
          Session Name
        </label>
        <input
          type="text"
          value={name}
          onChange={(e) => setName(e.target.value)}
          className="w-full px-5 py-3 rounded-full bg-[#1E0A29] text-white placeholder-[#710973] focus:outline-none focus:ring-4 focus:ring-[#F244D5] transition-all"
          placeholder="Enter session name"
        />
      </div>
      <div className="mb-6">
        <label className="block mb-2 text-sm font-medium text-[#F244D5]">
          Password
        </label>
        <input
          type="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          className="w-full px-5 py-3 rounded-full bg-[#1E0A29] text-white placeholder-[#710973] focus:outline-none focus:ring-4 focus:ring-[#F244D5] transition-all"
          placeholder="Enter session password"
        />
      </div>
      <div className="flex justify-center">
        <button
          onClick={handleSubmit}
          className="px-5 py-2 bg-gradient-to-r from-[#F244D5] to-[#710973] text-white rounded-full hover:shadow-lg transition-all"
        >
          Create
        </button>
      </div>
    </Modal>
  );
};

export default CreateSessionModal;
