import React from 'react';
import { CCardMatchesButton } from '../CCardMatchesButton/CCardMatchesButton';
import { CCardEditButton } from '../CCardEditButton/CCardEditButton';
import themeStore from '../../../stores/themeStore';
import './CPetCard.scss';

interface CPetCardProps {
    id: string,
    name: string,
    photo: string
}

export const CPetCard: React.FC<CPetCardProps> = (pet) => {
    return (
        <article className={themeStore.isLightTheme ? 'pet__card' : 'pet__card__dark'}>
            <img src={pet.photo} alt="pet" />
            <div className='pet__card__content'>
                <h3>{pet.name}</h3>
                <div className='pet__card__buttons__wrapper'>
                    <CCardEditButton id={pet.id} />
                    <CCardMatchesButton id={pet.id} />
                </div>
            </div>
        </article>
    );
};
