import React, { useEffect, useState } from 'react';
import { CMatchesHeader } from '../../components/common/CMatchesHeader/CMatchesHeader';
import { CChangeThemeButton } from '../../components/common/CChangeThemeButton/CChangeThemeButton';
import themeStore from '../../stores/themeStore';
import chatProfileStore from '../../stores/chatProfileStore';
import { CMatchCard } from '../../components/common/CMatchCard/CMatchCard';
import { observer } from 'mobx-react';
import { CShowHideButton } from '../../components/common/CShowHideButton/CShowHideButton';
import './MatchesChatPage.scss';
import agent from '../../api/axiosAgent';
import { useParams } from 'react-router-dom';
import { PetChat } from '../../components/PetChat/PetChat';
import chatStore from '../../stores/chatStore';
import SwipingCards from '../../components/SwipingCards/SwipingCards';
import { IPossibleSwipes } from '../../interfaces/Interfaces';
import PetProfile from '../../components/PetProfile/PetProfile';

interface MatchesChatPageProps { }

interface IMatch {
    animalId: string;
    name: string;
    photo: string;
    matchId: string;
    isChatStarted: boolean;
}

export const MatchesChatPage: React.FC<MatchesChatPageProps> = observer(() => {
    const [matchesOrMessages, setMatchesOrMessages] = useState(true);
    const [shownMatches, setShownMatches] = useState(true);
    const [matches, setMatches] = useState<IMatch[]>([]);
    const [messagesCards, setMessagesCards] = useState<IMatch[]>([]);
    const [matchId, setMatchId] = useState<string>();
    const { id } = useParams();
    const [currentPet, setCurrentPet] = useState<IPossibleSwipes | undefined>(undefined);

    useEffect(() => {
        if (id) {
            agent.apiMatches.animalMatches(id!)
                .then(res => {
                    setMatches(res.data);
                });
        }
    }, [id]);

    useEffect(() => {
        chatStore.hideChat();
    }, [id]);

    useEffect(() => {
        setMessagesCards(matches.filter((m) => m.isChatStarted));
    }, [matches]);

    const onPetChange = (pet: IPossibleSwipes | undefined) => {
        setCurrentPet(pet);
    };

    const matchesOption = () => {
        setMatchesOrMessages(true);
    };

    const messagesOption = () => {
        setMatchesOrMessages(false);
    };

    const showProfileHandler = () => {
        chatProfileStore.changeIsItShownState();
    };

    const showMatchesHandler = () => {
        setShownMatches(!shownMatches);
    };

    const onNewMatchOrUnmatch = async () => {
        const res = await agent.apiMatches.animalMatches(id!);

        setMatches(res.data);
    };

    const clearProfileSection = () => {
        setCurrentPet(undefined);
    };

    const updateMatches = () => {
        setMatches((prev) => {
            const updatedMatches = [...prev];

            const matchToUpdate = updatedMatches.find((m) => m.matchId === matchId);

            if (matchToUpdate) {
                matchToUpdate.isChatStarted = true;
            }

            return updatedMatches;
        });
    };

    const renderMatchesOrMessages = (array: IMatch[]) => {
        return (
            <>
                {array.map((match) => (
                    <CMatchCard
                        name={match.name}
                        photo={match.photo}
                        matchId={match.matchId}
                        petId={match.animalId}
                        onPetChange={onPetChange}
                        key={match.animalId}
                        onSetMatchId={() => handleSetMatchId(match.matchId)}
                    />
                ))}
            </>
        );
    };

    const handleSetMatchId = (newMatchId: string) => {
        setMatchId(newMatchId);
    };

    return (
        <section className={themeStore.isLightTheme ? 'matches__page' : 'matches__page  matches__page__dark'}>

            <div className='matches__page__theme__button'>
                <CChangeThemeButton />
            </div>
            <div className={!chatProfileStore.isItShown ? 'matches__page__matches__button' : 'matches__page__matches__button__hidden'}>
                <CShowHideButton param='Matches' clickHandler={showMatchesHandler} state={shownMatches} />
            </div>
            <div className='matches__page__profile__button'>
                <CShowHideButton param='Profile' clickHandler={showProfileHandler} state={chatProfileStore.isItShown} />
            </div>

            <section className={`${shownMatches ? 'matches__page__matches' : 'matches__page__matches matches__page__matches__hidden'} ${themeStore.isLightTheme ? null : 'matches__page__matches__dark'}`}>
                <CMatchesHeader />
                
                <article className={themeStore.isLightTheme ? 'matches__page__matches__links' : 'matches__page__matches__links matches__page__matches__links__dark'}>
                    <h4 className={matchesOrMessages ? 'matches__messages__option' : ''} onClick={matchesOption}>matches <span>{matches.length}</span></h4>
                    <h4 className={!matchesOrMessages ? 'matches__messages__option' : ''} onClick={messagesOption}>messages<span>{messagesCards.length}</span></h4>
                </article>

                <article className="matches__page__matches__render">
                    {matchesOrMessages
                        ? renderMatchesOrMessages(matches)
                        : renderMatchesOrMessages(messagesCards)}
                </article>
            </section>

            <section className={chatProfileStore.isItShown || shownMatches ? ' matches__page__chat' : ' matches__page__chat  matches__page__chat__large'}>
                {chatStore.isShown && <PetChat updateMatches={updateMatches} matchId={matchId!} onCloseChat={clearProfileSection} />}
                {!chatStore.isShown && <SwipingCards onPetChange={onPetChange} onNewMatch={onNewMatchOrUnmatch} />}
            </section>

            <section className={chatProfileStore.isItShown ? ' matches__page__profile' : 'matches__page__profile  matches__page__profile__hidden'}>
                {currentPet && <PetProfile pet={currentPet} onUnmatch={onNewMatchOrUnmatch} clearProfileSection={clearProfileSection} />}
            </section>

            <section className={!chatProfileStore.isItShown ? 'matches__page__see__profile' : 'matches__page__see__profile__hidden'}>
                <div className='matches__page__see__profile__image'>
                    <img src="/images/cat-with-comp.avif" alt="" />
                </div>
                <h3><span>who am i?</span> <br />🐶 see my profile! 🐱</h3>
            </section>

        </section>
    );
});
