import React, { useCallback, useEffect, useRef, useState } from "react";
import Message from "./Message";
import { sendMessage } from "@microsoft/signalr/dist/esm/Utils";
import { useAuth } from "../../../context/useAuth";
import { LOCAL_VIDEO, provideMediaRefType } from "../../../utils/globalConfig";

interface ChatMessage {
  userName: string;
  message: string;
}

interface WatchSessionProps {
  onLeaveSession: () => void;
  sessionId: string;
  startDemo: (sessionId: string, userName: string) => Promise<void>;
  endDemo: (sessionId: string, userName: string) => Promise<void>;
  messages: ChatMessage[];
  sendMessage: (
    message: string,
    sessionId: string,
    userName: string
  ) => Promise<void>; // Тип sendMessage
  isStreamingActive: boolean;
  isLocalStreamReady: boolean;
  provideMediaRef: provideMediaRefType;
  clients: string[];
  streamerConnectionId: string;
  streamerUserName: string;
}

const WatchSession: React.FC<WatchSessionProps> = (props) => {
  const { user } = useAuth();
  const [message, setMessage] = useState("");
  const chatEndRef = useRef<HTMLDivElement | null>(null);

  const scrollToBottom = useCallback(() => {
    chatEndRef.current?.scrollIntoView({ behavior: "smooth" });
  }, []);

  useEffect(() => {}, []);

  const onSendMessage = useCallback(() => {
    if (message.trim()) {
      props.sendMessage(
        message,
        props.sessionId,
        user?.userName ?? "defaultUserName"
      );
      setMessage("");
      scrollToBottom();
    }
  }, [message, sendMessage, scrollToBottom]);

  useEffect(() => {
    scrollToBottom();
  }, [props.messages, scrollToBottom]);

  const toggleFullScreen = (element: HTMLElement) => {
    if (
      !(document as any).fullscreenElement &&
      !(document as any).mozFullScreenElement &&
      !(document as any).webkitFullscreenElement &&
      !(document as any).msFullscreenElement
    ) {
      // IE/Edge
      if (element.requestFullscreen) {
        element.requestFullscreen();
      } else if ((element as any).mozRequestFullScreen) {
        /* Firefox */
        (element as any).mozRequestFullScreen();
      } else if ((element as any).webkitRequestFullscreen) {
        /* Chrome, Safari and Opera */
        (element as any).webkitRequestFullscreen();
      } else if ((element as any).msRequestFullscreen) {
        /* IE/Edge */
        (element as any).msRequestFullscreen();
      }
    } else {
      if (document.exitFullscreen) {
        document.exitFullscreen();
      } else if ((document as any).mozCancelFullScreen) {
        /* Firefox */
        (document as any).mozCancelFullScreen();
      } else if ((document as any).webkitExitFullscreen) {
        /* Chrome, Safari and Opera */
        (document as any).webkitExitFullscreen();
      } else if ((document as any).msExitFullscreen) {
        /* IE/Edge */
        (document as any).msExitFullscreen();
      }
    }
  };

  return (
    <div className="flex h-screen items-center justify-center">
      <div className="flex flex-col md:flex-row w-[85%] h-[80%] max-w-6xl rounded-3xl overflow-hidden bg-[#2F0740]/80 shadow-2xl">
        <div className="flex-1 flex flex-col m-4 bg-[#140626]/70 rounded-3xl p-6 shadow-inner">
          <h2 className="text-center text-[#F244D5] font-bold text-2xl mb-4">
            Broadcast window
          </h2>

          {!props.isStreamingActive ? (
            <div className="text-center">
              <button
                className="px-6 py-3 bg-[#F244D5] text-[#140626] rounded-full hover:bg-[#710973]/90 transition-colors font-semibold text-lg"
                onClick={() =>
                  props.startDemo(
                    props.sessionId,
                    user?.userName ?? "defaultUserName"
                  )
                }
              >
                Start Demo
              </button>
              <p className="text-gray-400 mt-2">
                Tap to start the screen demonstration.
              </p>
            </div>
          ) : (
            <>
              {user?.userName === props.streamerUserName ? (
                <div className="mb-4 relative">
                  <h3 className="text-lg text-white mb-2">Your screen</h3>

                  <video
                    ref={(node) => props.provideMediaRef(LOCAL_VIDEO, node)}
                    autoPlay
                    playsInline
                    className="w-full rounded-md cursor-pointer"
                    onClick={(e) =>
                      toggleFullScreen(e.target as HTMLVideoElement)
                    }
                  />
                  <button
                    className="absolute bottom-2 left-2 bg-[#F244D5] text-[#140626] rounded-full hover:bg-[#710973]/90 transition-colors font-semibold text-sm px-4 py-2"
                    onClick={() =>
                      props.endDemo(
                        props.sessionId,
                        user?.userName ?? "defaultUserName"
                      )
                    }
                    aria-label="End broadcast"
                    title="End broadcast"
                  >
                    End broadcast
                  </button>
                </div>
              ) : (
                <div className="mb-4 relative">
                  <h3 className="text-lg text-white mb-2">Broadcast</h3>
                  <video
                    ref={(node) =>
                      props.provideMediaRef(props.streamerConnectionId, node)
                    }
                    autoPlay
                    playsInline
                    className="w-full rounded-md cursor-pointer"
                    onClick={(e) =>
                      toggleFullScreen(e.target as HTMLVideoElement)
                    }
                  />
                </div>
              )}
            </>
          )}
        </div>

        <div className="w-full md:w-2/5 flex flex-col m-4 bg-[#710973]/80 rounded-3xl p-6 shadow-lg">
          <div className="flex justify-between items-center mb-4">
            <h2 className="text-lg font-bold text-[#F244D5]">Chat</h2>
            <button
              className="px-4 py-2 bg-[#F244D5] text-[#140626] rounded-full hover:bg-[#710973]/90 transition-colors font-semibold text-sm"
              onClick={props.onLeaveSession}
            >
              Exit
            </button>
          </div>

          <div className="flex-grow overflow-y-auto bg-[#140626]/40 rounded-2xl p-4 space-y-3">
            {props.messages.map((msg, index) => (
              <Message
                key={index}
                userName={msg.userName}
                message={msg.message}
                isCurrentUser={msg.userName === user?.userName}
              />
            ))}
            <div ref={chatEndRef} />
          </div>

          <div className="mt-3 flex items-center">
            <input
              type="text"
              placeholder="Enter a message..."
              value={message}
              onChange={(e) => setMessage(e.target.value)}
              className="flex-grow bg-[#2F0740] text-[#F244D5] rounded-lg px-4 py-2 mr-2 placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-[#F244D5]"
            />
            <button
              className="px-6 py-2 bg-[#F244D5] text-[#140626] rounded-full hover:bg-[#710973]/90 font-semibold transition-all"
              onClick={onSendMessage}
            >
              Send
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default WatchSession;
