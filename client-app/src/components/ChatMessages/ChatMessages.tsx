import { observer } from "mobx-react";
import { useEffect, useRef } from "react";
import "./ChatMessages.scss";
import { useParams } from "react-router-dom";
import { IMessage } from "../../interfaces/Interfaces";

interface ChatMessagesProps {
  messages: IMessage[]
}

export const ChatMessages: React.FC<ChatMessagesProps> = observer(({messages}) => {
  const chatMessagesRef = useRef<HTMLDivElement>(null);
  const { id } = useParams();

  useEffect(() => {
    scrollToBottom();
  }, [messages]);

  const scrollToBottom = () => {
    if (chatMessagesRef.current) {
      chatMessagesRef.current.scrollTop = chatMessagesRef.current.scrollHeight;
    }
  };

  return (
    <div className="chat-messages" ref={chatMessagesRef}>
      {messages.map((message, index) => (
        <div
          key={index}
          className={`message ${message.animalId === id ? "sender" : "recipient"}`}
        >
          <div className="message-text">{message.content}</div>
          <div className="timestamp">{new Date(message.sentOn).toLocaleString()}</div>
        </div>
      ))}
    </div>
  );
});
