import Modal from "../common/Modal";

interface ConfirmLogoutModalProps {
  sessionId: string;
  onClose: () => void;
  onConfirm: () => void;
}

const ConfirmLogoutModal: React.FC<ConfirmLogoutModalProps> = ({
  sessionId,
  onClose,
  onConfirm,
}) => {
  return (
    <Modal onClose={onClose}>
      <h2 className="text-2xl font-bold text-[#F244D5] mb-4 text-center">
        Confirm Logout
      </h2>
      <p className="text-white text-center mb-6">
        Are you sure you want to leave the session{" "}
        <strong className="text-[#E94560]">№{sessionId}</strong>?
      </p>
      <div className="flex justify-center">
        <button
          onClick={onConfirm}
          className="px-5 py-2 bg-gradient-to-r from-[#F244D5] to-[#710973] text-white rounded-full hover:shadow-lg transition-all"
        >
          Logout
        </button>
      </div>
    </Modal>
  );
};

export default ConfirmLogoutModal;
