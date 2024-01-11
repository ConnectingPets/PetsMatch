import React from 'react';
import { CCardMatchesButton } from '../CCardMatchesButton/CCardMatchesButton';
import { CCardEditButton } from '../CCardEditButton/CCardEditButton';
import themeStore from '../../../stores/themeStore';
import './CPetCard.scss';
import { CMarketCardButton } from '../CMarketCardButton/CMarketCardButton';

interface CPetCardProps {
    id: string,
    name: string,
    photo: string,
    buttons: string
}

export const CPetCard: React.FC<CPetCardProps> = (pet) => {
    return (
        <article className={themeStore.isLightTheme ? 'pet__card' : 'pet__card__dark'}>
            <img src={pet.photo} alt="pet" />
            <div className='pet__card__content'>
                <h3>{pet.name}</h3>
                <div className='pet__card__buttons__wrapper'>
                    {pet.buttons == 'edit' && (
                        <>
                            <CCardEditButton id={pet.id} />
                            <CCardMatchesButton id={pet.id} />
                        </>
                    )}
                    {pet.buttons == 'market' && (
                        <>
                            <CMarketCardButton id={pet.id} button='view' />
                            <CMarketCardButton id={pet.id} button='edit' />
                        </>
                    )}
                </div>
            </div>
        </article>
    );
};
