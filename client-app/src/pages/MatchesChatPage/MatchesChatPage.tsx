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

    const [showMatches, setShowMatches] = useState(true);

    const showMatchesHandler = () => {
        setShowMatches(true);
    }

    const showMessagesHandler = () => {
        setShowMatches(false);
    }

    const showProfileHandler = () => {
        chatProfileStore.changeIsItShownState();
    }

    return (
        <section className={themeStore.isLightTheme ? 'matches__page' : 'matches__page  matches__page__dark'}>
            <div className='matches__page__theme__button'>
                <CChangeThemeButton />
                <CShowHideButton param='Profile' clickHandler={showProfileHandler} />
            </div>
            <section className='matches__page__matches'>
                <CMatchesHeader />
                <article className={themeStore.isLightTheme ? 'matches__page__matches__links' : 'matches__page__matches__links matches__page__matches__links__dark'}>
                    <h4 className={showMatches ? 'matches__messages__option' : ''} onClick={showMatchesHandler}>matches <span>{pets.length}</span></h4>
                    <h4 className={!showMatches ? 'matches__messages__option' : ''} onClick={showMessagesHandler}>messages<span>{3}</span></h4>
                </article>

                <article className='matches__page__matches__render' >
                    {
                        showMatches
                            ? <>{pets.map(x => <CMatchCard name={x.name} photo={x.photo} key={Math.random()} />)}</>
                            : null
                    }
                </article>
            </section>

            <section className={chatProfileStore.isItShown ? ' matches__page__chat' : ' matches__page__chat  matches__page__chat__large'}>
            </section>

            <section className={chatProfileStore.isItShown ? ' matches__page__profile' : 'matches__page__profile  matches__page__profile__hidden'}>
                <article className={chatProfileStore.isItShown ?'matches__page__profile__content': 'matches__page__profile__content matches__page__profile__content__hidden'}>
                    <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Corrupti, doloremque earum? Quam voluptates nesciunt alias fugit! Incidunt atque doloribus excepturi nesciunt, cum velit provident id vitae deserunt vel corrupti aperiam?</p>
                    <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Nam inventore facilis dicta tenetur quo? Quibusdam totam veniam voluptas? Praesentium facilis voluptatibus cumque nam pariatur natus quod voluptatem quia officia tenetur.</p>
                </article>
            </section>

        </section>
    )
})
