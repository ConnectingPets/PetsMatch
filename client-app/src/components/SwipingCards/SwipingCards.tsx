import React, { useEffect, useMemo, useRef, useState } from 'react';
import { useParams } from 'react-router-dom';
import TinderCard from 'react-tinder-card';
import { PiHandHeartDuotone } from 'react-icons/pi';
import { FaRegPaperPlane } from 'react-icons/fa';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

import themeStore from '../../stores/themeStore';
import chatProfileStore from '../../stores/chatProfileStore';
import { IPossibleSwipes, ITinderCard } from '../../interfaces/Interfaces';
import agent from '../../api/axiosAgent';

import './SwipingCards.scss';

interface SwipingCardsProps {
    onPetChange?: (pet: IPossibleSwipes | undefined) => void;
    onNewMatch: () => void;
}

const SwipingCards: React.FC<SwipingCardsProps> = ({ onPetChange, onNewMatch }) => {
    const [welcomeMessage, setWelcomeMessage] = useState<boolean>(true);
    const [possibleSwipes, setPossibleSwipes] = useState<IPossibleSwipes[]>([]);
    const [currentIndex, setCurrentIndex] = useState(0);
    const currentIndexRef = useRef<number>(currentIndex);
    const [currentPet, setCurrentPet] = useState<IPossibleSwipes | undefined>(possibleSwipes?.[currentIndex]);
    const { id: petId } = useParams();

    useEffect(() => {
        agent.apiMatches.getAllPossibleSwipesForAnimal(petId!)
            .then(res => {
                setPossibleSwipes(res.data);
                setCurrentIndex(res.data.length);
            });
    }, [petId]);

    const swipesLength = (possibleSwipes && possibleSwipes.length);

    const childRef = useMemo(
        () => Array(swipesLength).fill(0).map(() => React.createRef<ITinderCard | null>()), [swipesLength]
    );

    const updateCurrentIndex = (index: number) => {
        setCurrentIndex(index);
        currentIndexRef.current = index;
    };

    const canGoBack = currentIndex < swipesLength - 1;
    const canSwipe = currentIndex >= 0;

    const swiped = async (index: number, dir: string) => {
        updateCurrentIndex(index - 1);

        const newPet = possibleSwipes?.[index - 1];
        setCurrentPet(newPet);
        onPetChange && onPetChange(newPet);

        if (dir == 'left' && currentPet?.animalId) {
            await agent.apiMatches.swipe(petId!, currentPet.animalId, false);
        } else if (dir == 'right' && currentPet?.animalId) {
            const res = await agent.apiMatches.swipe(petId!, currentPet.animalId, true);

            if (res.data) {
                const res = await agent.apiMatches.match(petId!, currentPet.animalId);
                toast.success(res.successMessage);

                if (res.isSuccess) {
                    onNewMatch();
                }
            }
        }
    };

    const outOfFrame = (index: number) => {
        currentIndexRef.current >= index && childRef[index].current?.restoreCard();
    };

    const swipe = (dir: string) => {
        if (canSwipe && currentIndex < swipesLength) {
            childRef[currentIndex].current?.swipe(dir);
        } else {
            swiped(currentIndex, dir);
        }
    };

    const goBack = () => {
        if (!canGoBack) {
            return;
        }

        const newIndex = currentIndex + 1;
        updateCurrentIndex(newIndex);
        childRef[newIndex].current?.restoreCard();
        
        const newPet = possibleSwipes[currentIndex + 1];
        onPetChange && onPetChange(newPet);
    };

    const onWelcomeMessageClick = () => {
        setWelcomeMessage(false);
        swipe('left');
    };

    return (
        <div className="card-wrapper">
            {welcomeMessage && (
                <div className="card-wrapper__welcomeMessage">
                    <p className={themeStore.isLightTheme ? '' : 'p-dark'}>Ready to swipe <PiHandHeartDuotone /></p>
                    <button onClick={onWelcomeMessageClick} className={themeStore.isLightTheme ? '' : 'btn-dark'}>Go <FaRegPaperPlane /></button>
                </div>
            )}

            {possibleSwipes && possibleSwipes.map((pet: { name: string, photo: string }, index) => (
                <TinderCard
                    key={pet.photo}
                    ref={childRef[index] as never}
                    onSwipe={(dir) => swiped(index, dir)}
                    preventSwipe={['up', 'down']}
                    onCardLeftScreen={() => outOfFrame(index)}
                    className={`card-wrapper__swipe ${chatProfileStore.isItShown ? 'card-wrapper__swipe__hidden' : null}`}
                >
                    <div style={{ backgroundImage: `url(${pet.photo})` }} className="card-wrapper__swipe__card">
                        <h3 className={themeStore.isLightTheme ? '' : 'h3-dark'}>{pet.name}</h3>
                    </div>
                </TinderCard>
            ))}

            {possibleSwipes && (
                <div className={`card-wrapper__buttons ${chatProfileStore.isItShown ? 'card-wrapper__buttons__hidden' : null}`}>
                    <button 
                        onClick={() => swipe('left')} 
                        style={{ backgroundColor: !canSwipe ? '#c3c4d3' : '' }} 
                        disabled={!canSwipe}
                    >X</button>
                    <button 
                        onClick={() => goBack()} 
                        style={{ backgroundColor: !canGoBack ? '#c3c4d3' : '' }} 
                        disabled={!canGoBack}
                    >Undo</button>
                    <button 
                        onClick={() => swipe('right')} 
                        style={{ backgroundColor: !canSwipe ? '#c3c4d3' : '' }} 
                        disabled={!canSwipe}
                        id={themeStore.isLightTheme ? '' : 'like-btn-dark'}
                    >Like</button>
                </div>
            )}
        </div>
    );
};

export default SwipingCards;
