import React from "react";
import { observer } from "mobx-react";
import { CChatExitButton } from "../common/CChatExitButton/CChatExitButton";
import './ChatHeader.scss';
import chatStore from "../../stores/chatStore";

interface ChatHeaderProps {
  onCloseChat: () => void
}

export const ChatHeader: React.FC<ChatHeaderProps> = observer(({ onCloseChat }) => {

  return (
    <div className="chat-header">
      <div className="user-info">
        <img src={chatStore.sendeePhoto} alt="User Photo" className="user-photo" />
        <span className="username">{chatStore.sendeeName}</span>
      </div>
      <CChatExitButton onCloseChat={onCloseChat} />
    </div>
  );
});
