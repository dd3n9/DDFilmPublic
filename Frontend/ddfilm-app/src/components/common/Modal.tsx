interface ModalProps {
  onClose: () => void;
  children: React.ReactNode;
}

const Modal: React.FC<ModalProps> = ({ onClose, children }) => {
  return (
    <div className="fixed inset-0 bg-black bg-opacity-60 flex items-center justify-center z-50">
      <div className="relative bg-gradient-to-b from-[#2F0740] to-[#140626] rounded-3xl shadow-2xl p-8 w-full max-w-md">
        {children}
        <button
          onClick={onClose}
          className="absolute top-3 right-3 text-[#F244D5] hover:text-[#FF66E5] transition-all text-2xl"
        >
          &times;
        </button>
      </div>
    </div>
  );
};

export default Modal;
