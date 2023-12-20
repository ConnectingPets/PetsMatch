import React, { useEffect, useState } from 'react';
import TinderCard from 'react-tinder-card';

import agent from '../../api/axiosAgent';

import './SwipingCards.scss';

interface SwipingCardsProps { }

interface IPossibleSwipes {
    map(arg0: (pet: { name: string; photo: string; }) => import('react/jsx-runtime').JSX.Element): React.ReactNode;
    age: number,
    birthDate?: Date,
    breed: string,
    description?: string,
    gender: string,
    healthStatus: string,
    isEducated: false,
    isHavingValidDocuments: true,
    name: string,
    photo: string,
    socialMedia?: string,
    weight?: number
}

const SwipingCards: React.FC<SwipingCardsProps> = () => {
    const [possibleSwipes, setPossibleSwipes] = useState<IPossibleSwipes>();

    useEffect(() => {
        agent.apiMatches.getAllPossibleSwipesForAnimal()
            .then(res => {
                console.log(res.data);
                setPossibleSwipes(res.data);
            });
    }, []);

    const outOfFrame = (name: string) => {
        console.log(name + ' left');
    };

    return (
        <div className="card-wrapper">
            {possibleSwipes && possibleSwipes.map((pet: { name: string, photo: string }) => (
                <TinderCard
                    key={pet.photo}
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
