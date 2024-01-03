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
import SwipingCards from '../../components/SwipingCards/SwipingCards';
import { IPossibleSwipes } from '../../interfaces/Interfaces';
import PetProfile from '../../components/PetProfile/PetProfile';

interface MatchesChatPageProps { }

interface IMatch {
    id: string,
    name: string,
    photo: string
}

export const MatchesChatPage: React.FC<MatchesChatPageProps> = observer(() => {
    const [matchesOrMessages, setMatchesOrMessages] = useState(true);
    const [shownMatches, setShownMatches] = useState(true);
    const [matches, setMatches] = useState<IMatch[]>([]);
    const { id } = useParams();
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    const [shownChat, setShownChat] = useState(false);
    const [currentPet, setCurrentPet] = useState<IPossibleSwipes | undefined>(undefined);

    useEffect(() => {
        if (id) {
            agent.apiMatches.animalMatches(id!)
                .then(res => {
                    setMatches(res.data);
                });
        }
    }, [id]);

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
            <section className={shownMatches ? 'matches__page__matches' : 'matches__page__matches matches__page__matches__hidden'}>
                <CMatchesHeader />
                <article className={themeStore.isLightTheme ? 'matches__page__matches__links' : 'matches__page__matches__links matches__page__matches__links__dark'}>
                    <h4 className={matchesOrMessages ? 'matches__messages__option' : ''} onClick={matchesOption}>matches <span>{matches.length}</span></h4>
                    <h4 className={!matchesOrMessages ? 'matches__messages__option' : ''} onClick={messagesOption}>messages<span>{3}</span></h4>
                </article>

                <article className='matches__page__matches__render' >
                    {
                        matchesOrMessages
                            ? <>{matches.map(match => <CMatchCard name={match.name} photo={match.photo} key={match.id} />)}</>
                            : null
                    }
                </article>
            </section>

            <section className={chatProfileStore.isItShown || shownMatches ? ' matches__page__chat' : ' matches__page__chat  matches__page__chat__large'}>
                {!shownChat && <SwipingCards onPetChange={onPetChange} />}
                {shownChat && (
                    <p style={{ padding: '0 5rem', lineHeight: '3rem' }}>Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit amet cumque rerum alias quasi? Tenetur, cum hic? Illo accusamus, amet enim, rerum at nostrum iste architecto cum velit nihil nulla!<br />CHAT</p>
                )}
            </section>

            <section className={chatProfileStore.isItShown ? ' matches__page__profile' : 'matches__page__profile  matches__page__profile__hidden'}>
                {currentPet && <PetProfile pet={currentPet} />}
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
