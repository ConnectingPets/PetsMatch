import React from "react";
import { useParams } from "react-router-dom";
import { observer } from "mobx-react";
import { CChatExitButton } from "../common/CChatExitButton/CChatExitButton";
import './ChatHeader.scss';
import chatStore from "../../stores/chatStore";
import agent from "../../api/axiosAgent";
import { toast } from "react-toastify";
import themeStore from "../../stores/themeStore";

interface ChatHeaderProps {
  onUnmatch: () => void
}

interface IChatStore {
  sendeeName: string | null
  sendeePhoto: string | undefined
  isShown: boolean
  matchId: string | null
}

export const ChatHeader: React.FC<ChatHeaderProps> = observer(({ onUnmatch }) => {
  const { id: petId } = useParams();

  const onUnmatchClick = async () => {
    try {
      const petMatches = await agent.apiMatches.animalMatches(petId!);

      if (petMatches.isSuccess) {
        const petTwoId = petMatches.data.filter((p: IChatStore) => p.matchId == chatStore.matchId)[0].animalId;

        const res = await agent.apiMatches.unmatch(petId!, petTwoId);

        if (res.isSuccess) {
          toast.success(res.successMessage);

          onUnmatch();
          chatStore.hideChat();
        } else {
          toast.error(res.errorMessage);
        }
      } else {
        toast.error(petMatches.errorMessage);
      }
    } catch (err) {
      console.log(err);

      toast.error('An error occurred while processing the request');
    }
  };

  return (
    <div className="chat-header">
      <div className="user-info">
        <img src={chatStore.sendeePhoto} alt="User Photo" className="user-photo" />
        <span className="username">{chatStore.sendeeName}</span>
        <button onClick={onUnmatchClick} className={themeStore.isLightTheme ? '' : 'btn-dark'}>UNMATCH</button>
      </div>
      <CChatExitButton />
    </div>
  );
});
