import { observer } from "mobx-react";
import './CChatExitButton.scss';
import chatStore from "../../../stores/chatStore";

interface CChatExitButtonProps {}

export const CChatExitButton: React.FC<CChatExitButtonProps> = observer(() => {
  return (
    <button onClick={() => chatStore.hideChat()} className="exit-button">
      <div className="exit-icon">
        <div className="circle">
          <span className="x">X</span>
        </div>
      </div>
    </button>
  );
});
