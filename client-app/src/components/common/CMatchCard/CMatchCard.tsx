import React from 'react';
import themeStore from '../../../stores/themeStore.ts';
import { observer } from 'mobx-react';
import './CMatchCard.scss';
import chatStore from '../../../stores/chatStore.ts';
import agent from '../../../api/axiosAgent.ts';
import { IPossibleSwipes } from '../../../interfaces/Interfaces.ts';

interface CMatchCardProps {
    name: string,
    photo: string,
    matchId: string,
    petId: string,
    onPetChange: (pet: IPossibleSwipes | undefined) => void,
    onSetMatchId: () => void
}

export const CMatchCard: React.FC<CMatchCardProps> = observer(({
    name,
    photo,
    petId,
    onPetChange,
    onSetMatchId
})=>{

    const onChatCardClick = async () => {
        chatStore.showChat(name, photo);
        onSetMatchId();

        try {
            const res = await agent.apiMatches.getPetProfile(petId);
            res.data.unmatchId = petId;

            onPetChange(res.data);
        } catch (err) {
            console.log(err);
        }
    };

    return (
        <article onClick={onChatCardClick} className={themeStore.isLightTheme ? 'match__card match__card__light' : 'match__card match__card__dark'}>
            <div className='match__card__image__wrapper'>
                <div></div>
                <img src={photo} alt="" />
                <h4>{name}</h4>
            </div>
        </article>
    );
});
