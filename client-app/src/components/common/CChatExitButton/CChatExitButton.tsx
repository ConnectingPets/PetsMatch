import React from "react";
import { observer } from "mobx-react";
import './CChatExitButton.scss';
import chatStore from "../../../stores/chatStore";

interface CChatExitButtonProps {
  onCloseChat: () => void
}

export const CChatExitButton: React.FC<CChatExitButtonProps> = observer(({ onCloseChat }) => {

  const onClickExitButton = () => {
    chatStore.hideChat();

    onCloseChat();
  };
  
  return (
    <button onClick={onClickExitButton} className="exit-button">
      <div className="exit-icon">
        <div className="circle">
          <span className="x">X</span>
        </div>
      </div>
    </button>
  );
});
