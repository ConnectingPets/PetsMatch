import React from 'react';
import { Link } from 'react-router-dom';
import {BsPlusCircleFill} from 'react-icons/bs'
import themeStore from '../../../stores/themeStore';
import './CAddPetCard.scss';

interface CAddPetCardProps { };

export const CAddPetCard: React.FC<CAddPetCardProps> = () => {
    return (
        <article className={themeStore.isLightTheme ?'add__pet__card' : 'add__pet__card add__pet__card__dark'}>
            <section className={themeStore.isLightTheme ? 'add__pet__card__cover': 'add__pet__card__cover  add__pet__card__cover__dark'}>
                <Link className={themeStore.isLightTheme ?'add__pet__card__cover__content' : 'add__pet__card__cover__content  add__pet__card__cover__content__dark'} to="/add-pet">
                    <BsPlusCircleFill />
                    <h3>add pet</h3>
                </Link>
                <h4>more pets <br/> more friends </h4>
            </section>
        </article>
    )
}
