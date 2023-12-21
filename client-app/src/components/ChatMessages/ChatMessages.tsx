import { observer } from "mobx-react";
import { useEffect, useRef, useState } from "react";
import "./ChatMessages.scss";
import { useParams } from "react-router-dom";
import agent from "../../api/axiosAgent";
import chatStore from "../../stores/chatStore";

interface ChatMessagesProps {}

interface IMessage {
  content: string;
  animalId: string;
  sentOn: string;
}

export const ChatMessages: React.FC<ChatMessagesProps> = observer(() => {
  const chatMessagesRef = useRef<HTMLDivElement>(null);
  const [messages, setMessages] = useState<IMessage[]>([]);
  const { id } = useParams();

  useEffect(() => {
    agent.apiMessages.getChatHistory(chatStore.matchId!).then((res) => {
      console.log(res.data);
      setMessages(res.data.reverse());
    });
  }, [id]);

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
