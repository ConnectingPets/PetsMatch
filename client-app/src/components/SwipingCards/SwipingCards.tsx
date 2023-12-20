import React, {  } from 'react';
import TinderCard from 'react-tinder-card';

import './SwipingCards.scss';

interface SwipingCardsProps { }

const pets = [
    {
        name: 'Eminem',
        photo: 'https://cdn.theatlantic.com/thumbor/vDZCdxF7pRXmZIc5vpB4pFrWHKs=/559x0:2259x1700/1080x1080/media/img/mt/2017/06/shutterstock_319985324/original.jpg'
    },
    {
        name: 'Vanilla Ice',
        photo: 'https://highlandcanine.com/wp-content/uploads/2021/01/vizsla-running.jpg'
    },
    {
        name: '2pac',
        photo: 'https://i.guim.co.uk/img/media/26392d05302e02f7bf4eb143bb84c8097d09144b/446_167_3683_2210/master/3683.jpg?width=1200&height=900&quality=85&auto=format&fit=crop&s=11e949fc5d06576bc8b80ec192896753'
    },
    {
        name: 'snoop dogg',
        photo: 'https://a-z-animals.com/media/2021/12/Prettiest-_-Cutest-Dogs-header.jpg'
    },
    {
        name: 'BIG',
        photo: 'https://i.pinimg.com/originals/0d/08/60/0d0860d917320784369a58a1f01187d3.jpg'
    },
];

const SwipingCards: React.FC<SwipingCardsProps> = () => {

    const outOfFrame = (name: string) => {
        console.log(name + ' left');
    };

    return (      
            <div className="card-wrapper">
                {pets.map((pet) => (
                    <TinderCard 
                        key={pet.name}
                        preventSwipe={['up', 'down']}
                        onCardLeftScreen={() => outOfFrame(pet.name)} 
                        className="card-wrapper__swipe"
                    >
                        <div style={{ backgroundImage: `url(${pet.photo})` }} className="card-wrapper__swipe__card">
                            <h3>{pet.name}</h3>
                        </div>
                    </TinderCard>
                ))}
            </div>
    );
};

export default SwipingCards;
