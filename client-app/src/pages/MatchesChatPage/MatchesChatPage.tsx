import React, { useState } from 'react';
import { CMatchesHeader } from '../../components/common/CMatchesHeader/CMatchesHeader';
import { CChangeThemeButton } from '../../components/common/CChangeThemeButton/CChangeThemeButton';
import themeStore from '../../stores/themeStore';
import chatProfileStore from '../../stores/chatProfileStore';
import { CMatchCard } from '../../components/common/CMatchCard/CMatchCard';
import { observer } from 'mobx-react';
import { CShowHideButton } from '../../components/common/CShowHideButton/CShowHideButton';
import './MatchesChatPage.scss';

const pets = [
    {
        name: "Eminem",
        photo: "https://cdn.theatlantic.com/thumbor/vDZCdxF7pRXmZIc5vpB4pFrWHKs=/559x0:2259x1700/1080x1080/media/img/mt/2017/06/shutterstock_319985324/original.jpg"
    },
    {
        name: "Vanilla Ice",
        photo: "https://highlandcanine.com/wp-content/uploads/2021/01/vizsla-running.jpg"
    },
    {
        name: "2pac",
        photo: "https://i.guim.co.uk/img/media/26392d05302e02f7bf4eb143bb84c8097d09144b/446_167_3683_2210/master/3683.jpg?width=1200&height=900&quality=85&auto=format&fit=crop&s=11e949fc5d06576bc8b80ec192896753"
    },
    {
        name: "snoop dogg",
        photo: "https://a-z-animals.com/media/2021/12/Prettiest-_-Cutest-Dogs-header.jpg"
    },
    {
        name: "BIG",
        photo: "https://i.pinimg.com/originals/0d/08/60/0d0860d917320784369a58a1f01187d3.jpg"
    },
]

interface MatchesChatPageProps { };

export const MatchesChatPage: React.FC<MatchesChatPageProps> = observer(() => {

    const [matchesOrMessages, setMatchesOrMessages] = useState(true);
    const [shownMatches, setShownMatches] = useState(true);

    const matchesOption = () => {
        setMatchesOrMessages(true);
    }

    const messagesOption = () => {
        setMatchesOrMessages(false);
    }

    const showProfileHandler = () => {
        chatProfileStore.changeIsItShownState();
    }

    const showMatchesHandler = () => {
        setShownMatches(!shownMatches)
    }

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
                    <h4 className={matchesOrMessages ? 'matches__messages__option' : ''} onClick={matchesOption}>matches <span>{pets.length}</span></h4>
                    <h4 className={!matchesOrMessages ? 'matches__messages__option' : ''} onClick={messagesOption}>messages<span>{3}</span></h4>
                </article>

                <article className='matches__page__matches__render' >
                    {
                        matchesOrMessages
                            ? <>{pets.map(x => <CMatchCard name={x.name} photo={x.photo} key={Math.random()} />)}</>
                            : null
                    }
                </article>
            </section>

            <section className={chatProfileStore.isItShown || shownMatches ? ' matches__page__chat' : ' matches__page__chat  matches__page__chat__large'}>
                <p style={{ padding: "0 5rem", lineHeight: "3rem" }}>Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit amet cumque rerum alias quasi? Tenetur, cum hic? Illo accusamus, amet enim, rerum at nostrum iste architecto cum velit nihil nulla!<br/>CHAT</p>
            </section>

            <section className={chatProfileStore.isItShown ? ' matches__page__profile' : 'matches__page__profile  matches__page__profile__hidden'}>
                <p style={{ padding: "8rem" }}>PROFILE</p>
            </section>

            <section className={!chatProfileStore.isItShown ? 'matches__page__see__profile' : 'matches__page__see__profile__hidden'}>
                <div className='matches__page__see__profile__image'>
                    <img src="/images/cat-with-comp.avif" alt="" />
                </div>
                <h3><span>who am i?</span> <br />🐶 see my profile! 🐱</h3>
            </section>

        </section>
    )
})
