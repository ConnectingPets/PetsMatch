import { observer } from "mobx-react";
import { useEffect, useRef } from "react";
import './ChatMessages.scss';

interface ChatMessagesProps {}

const messages = [
  { text: "Hello!", sender: true, timestamp: "2023-01-01T12:34:56Z" },
  { text: "Hi there!", timestamp: "2023-01-01T12:35:23Z" },
];

export const ChatMessages: React.FC<ChatMessagesProps> = observer(() => {
  const chatMessagesRef = useRef<HTMLDivElement>(null);

    useEffect(() => {
      scrollToBottom();
    });

    const scrollToBottom = () => {
      if (chatMessagesRef.current) {
        chatMessagesRef.current.scrollTop =
          chatMessagesRef.current.scrollHeight;
      }
    };

  return (
    <div className="chat-messages" ref={chatMessagesRef}>
      {messages.map((message, index) => (
        <div
          key={index}
          className={`message ${message.sender ? "sender" : ""}`}
        >
          <div className="message-text">{message.text}</div>
          <div className="timestamp">
            {new Date(message.timestamp).toLocaleTimeString()}
          </div>
        </div>
      ))}
    </div>
  );
});
