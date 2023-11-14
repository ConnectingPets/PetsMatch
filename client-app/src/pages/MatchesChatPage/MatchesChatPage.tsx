import React, { useState } from 'react';
import { CMatchesHeader } from '../../components/common/CMatchesHeader/CMatchesHeader';
import { CChangeThemeButton } from '../../components/common/CChangeThemeButton/CChangeThemeButton';
import themeStore from '../../stores/themeStore';
import './MatchesChatPage.scss';
import { CMatchCard } from '../../components/common/CMatchCard/CMatchCard';

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
        photo: "https://www.cdc.gov/healthypets/images/pets/cute-dog-headshot.jpg?_=42445"
    },
    {
        name: "BIG",
        photo: "https://i.pinimg.com/originals/0d/08/60/0d0860d917320784369a58a1f01187d3.jpg"
    },
]

interface MatchesChatPageProps { };

export const MatchesChatPage: React.FC<MatchesChatPageProps> = () => {

    const [showMatches, setShowMatches] = useState(true);

    const showMatchesHandler = () => {
        setShowMatches(true);
    }

    const showMessagesHandler = () => {
        setShowMatches(false);
    }

    return (
        <section className='matches__page'>
            <div className='matches__page__theme__button'>
                <CChangeThemeButton />
            </div>
            <section className='matches matches__page__matches'>
                <CMatchesHeader />
                <article className='matches__page__matches__links'>
                    <h4 className={showMatches ? 'matches__messages__option' : ''} onClick={showMatchesHandler}>matches</h4>
                    <h4 className={!showMatches ? 'matches__messages__option' : ''} onClick={showMessagesHandler}>messages</h4>
                </article>

                <article className='matches__page__matches__cards'>
                    {
                        showMatches
                            ? <>{pets.map(x => <CMatchCard name={x.name} photo={x.photo} key={Math.random()} />)}</>
                            : null
                    }
                </article>
            </section>

            <section className='chat matches__page__chat'></section>

            <section className='profile matches__page__profile'> </section>

        </section>
    )
}
