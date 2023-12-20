import { observer } from "mobx-react";
import "./PetChat.scss";

interface PetChatProps {
  sendeeName: string
  sendeePhoto: string
  onHideChat: () => void
}

export const PetChat: React.FC<PetChatProps> = observer(({
  sendeeName,
  sendeePhoto,
  onHideChat
}) => {
  return (
    <>
      <div className="chat-container">
        <div className="chat-header">
          <div className="user-info">
            <img src={sendeePhoto} alt="User Photo" className="user-photo" />
            <span className="username">{sendeeName}</span>
          </div>
          <button onClick={onHideChat} className="exit-button">X</button>
        </div>
        <div className="chat-messages">
          <div className="message">
            <div className="message-text">Hello there!</div>
          </div>
          <div className="message sender">
            <div className="message-text">Hi! How are you?</div>
          </div>
        </div>
        <div className="message-input">
          <input type="text" placeholder="Type your message..." />
          <button>Send</button>
        </div>
      </div>
    </>
  );
});
