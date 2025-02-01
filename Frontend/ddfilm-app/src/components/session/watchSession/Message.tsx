interface MessageProps {
  userName: string;
  message: string;
  isCurrentUser: boolean;
}

const Message: React.FC<MessageProps> = ({
  userName,
  message,
  isCurrentUser,
}) => {
  return (
    <div
      className={`mb-3 max-w-[90%] ${
        isCurrentUser
          ? "ml-auto bg-[#F244D5]/40 text-[#75BFB8] text-right"
          : "mr-auto bg-[#2F0740]/70 text-[#F2F2E9] text-left"
      } p-4 rounded-2xl shadow-md break-words`}
    >
      <p className="font-semibold text-sm mb-1">{userName}:</p>
      <p className="text-base">{message}</p>
    </div>
  );
};

export default Message;
