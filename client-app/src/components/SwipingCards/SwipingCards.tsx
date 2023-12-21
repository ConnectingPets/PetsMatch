import React, { useEffect, useMemo, useRef, useState, useCallback } from 'react';
import TinderCard from 'react-tinder-card';

import { IPossibleSwipes, ITinderCard } from '../../interfaces/Interfaces';
import agent from '../../api/axiosAgent';

import './SwipingCards.scss';

interface SwipingCardsProps {
    onPetChange?: (pet: IPossibleSwipes | undefined) => void;
}

const SwipingCards: React.FC<SwipingCardsProps> = ({ onPetChange }) => {
    const [possibleSwipes, setPossibleSwipes] = useState<IPossibleSwipes[]>([]);
    const swipesLength = (possibleSwipes && possibleSwipes.length) || 0;
    const [currentIndex, setCurrentIndex] = useState(swipesLength - 1);
    const currentIndexRef = useRef<number>(currentIndex);
    const [, setCurrentPet] = useState<IPossibleSwipes | undefined>(possibleSwipes?.[currentIndex]);

    useEffect(() => {
        agent.apiMatches.getAllPossibleSwipesForAnimal()
            .then(res => {
                setPossibleSwipes(res.data);
            });
    }, []);

    const childRef = useMemo(
        () => Array(swipesLength).fill(0).map(() => React.createRef<ITinderCard | null>()), [swipesLength]
    );

    const updateCurrentIndex = (index: number) => {
        setCurrentIndex(index);
        currentIndexRef.current = index;
    };

    const canGoBack = currentIndex < swipesLength - 1;
    const canSwipe = currentIndex >= 0;

    const swiped = useCallback((index: number) => {
        updateCurrentIndex(index - 1);

        const newPet = possibleSwipes?.[index - 1];
        setCurrentPet(newPet);
        onPetChange && onPetChange(newPet);
    }, [onPetChange, possibleSwipes]);

    const outOfFrame = useCallback((index: number) => {
        currentIndexRef.current >= index && childRef[index].current?.restoreCard();
    }, [childRef]);

    const swipe = useCallback(async (dir: string) => {
        if (canSwipe && currentIndex < swipesLength) {
            await childRef[currentIndex].current?.swipe(dir);
        }
    }, [canSwipe, childRef, currentIndex, swipesLength]);

    const goBack = useCallback(async () => {
        if (!canGoBack) {
            return;
        }

        const newIndex = currentIndex + 1;
        updateCurrentIndex(newIndex);
        await childRef[newIndex].current?.restoreCard();
    }, [canGoBack, currentIndex, childRef]);

    return (
        <div className="card-wrapper">
            {possibleSwipes && possibleSwipes.map((pet: { name: string, photo: string }, index) => (
                <TinderCard
                    key={pet.photo}
                    ref={childRef[index] as never}
                    onSwipe={() => swiped(index)}
                    preventSwipe={['up', 'down']}
                    onCardLeftScreen={() => outOfFrame(index)}
                    className="card-wrapper__swipe"
                >
                    <div style={{ backgroundImage: `url(${pet.photo})` }} className="card-wrapper__swipe__card">
                        <h3>{pet.name}</h3>
                    </div>
                </TinderCard>
            ))}

            <div className="card-wrapper__buttons">
                <button onClick={() => swipe('left')} style={{ backgroundColor: !canSwipe ? '#c3c4d3' : '' }} disabled={!canGoBack} >X</button>
                <button onClick={() => goBack()} style={{ backgroundColor: !canGoBack ? '#c3c4d3' : '' }} disabled={!canGoBack} >Undo</button>
                <button onClick={() => swipe('right')} style={{ backgroundColor: !canSwipe ? '#c3c4d3' : '' }} disabled={!canGoBack} >Like</button>
            </div>
        </div>
    );
};

export default SwipingCards;
