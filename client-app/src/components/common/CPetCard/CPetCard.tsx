import React from 'react';
import './CPetCard.scss';
import { CCardMatchesButton } from '../CCardMatchesButton/CCardMatchesButton';

interface CPetCardProps {
    name: string,
    photo: string
}

export const CPetCard: React.FC<CPetCardProps> = (pet) => {
    return (
        <article className='pet__card'>
            <img src={pet.photo} alt="pet" />
            <div className='pet__card__content'>
                <h3>{pet.name}</h3>
                <div className='pet__card__buttons__wrapper'>
                    <CCardMatchesButton/>
                </div>
            </div>
        </article>
    )
}
