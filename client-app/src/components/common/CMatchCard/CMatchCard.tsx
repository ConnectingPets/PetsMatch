import React from 'react';
import themeStore from '../../../stores/themeStore.ts';
import { observer } from 'mobx-react';
import './CMatchCard.scss';

interface CMatchCardProps {
    name: string,
    photo: string
}

export const CMatchCard: React.FC<CMatchCardProps> = observer(({
name,
photo
})=>{
 
    return (
        <article className={themeStore.isLightTheme ? 'match__card match__card__light' : 'match__card match__card__dark'}>
            <div className='match__card__image__wrapper'>
                <div></div>
                <img src={photo} alt="" />
                <h4>{name}</h4>
            </div>
        </article>
    )
})
