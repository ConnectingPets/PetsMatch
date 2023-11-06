import React from 'react';
import { CCardMatchesButton } from '../CCardMatchesButton/CCardMatchesButton';
import './CPetCard.scss';
import { CCardEditButton } from '../CCardEditButton/CCardEditButton';
import themeStore from '../../../stores/themeStore';

interface CPetCardProps {
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
                    <CCardEditButton/>
                    <CCardMatchesButton/>
                </div>
            </div>
        </article>
    )
}
