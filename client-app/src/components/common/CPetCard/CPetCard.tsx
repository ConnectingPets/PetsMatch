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
    buttons: string,
    price?: number | null 
}

export const CPetCard: React.FC<CPetCardProps> = (pet) => {
    return (
        <article className={themeStore.isLightTheme ? 'pet__card' : 'pet__card__dark'}>
            <img src={pet.photo} alt="pet" />
            <div className='pet__card__content'>
                <h3>{pet.name} <span>{pet.price ? `$${pet.price}` : null}</span></h3>
                <div className='pet__card__buttons__wrapper'>
                    {pet.buttons == 'myPets' && (
                        <>
                            <CCardEditButton id={pet.id} />
                            <CCardMatchesButton id={pet.id} />
                        </>
                    )}
                    {pet.buttons == 'myPetsInMarket' && (
                        <>
                            <CMarketCardButton id={pet.id} button='details' />
                            <CMarketCardButton id={pet.id} button='edit' />
                        </>
                    )}
                    {pet.buttons == 'catalogMarket' && (
                        <>
                            <CMarketCardButton id={pet.id} button='details' />
                            <CMarketCardButton id={pet.id} button='buy' />
                        </>
                    )}
                    {pet.buttons == 'catalogAdoption' && (
                        <>
                            <CMarketCardButton id={pet.id} button='details' />
                            <CMarketCardButton id={pet.id} button='adopt' />
                        </>
                    )}
                </div>
            </div>
        </article>
    );
};
