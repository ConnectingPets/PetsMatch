import { observer } from "mobx-react";
import "./PetChat.scss";
import { ChatMessages } from "../ChatMessages/ChatMessages";
import { ChatHeader } from "../ChatHeader/ChatHeader";

interface PetChatProps {}

export const PetChat: React.FC<PetChatProps> = observer(() => {
  return (
    <div className="chat-container">
      <ChatHeader />
      <ChatMessages />
      <div className="message-input">
        <input type="text" placeholder="Type your message..." />
        <button>Send</button>
      </div>
    </div>
  );
});
