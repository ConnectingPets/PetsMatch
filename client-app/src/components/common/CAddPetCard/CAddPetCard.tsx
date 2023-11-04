import React from 'react';
import { Link } from 'react-router-dom';
import {BsPlusCircleFill} from 'react-icons/bs'
import './CAddPetCard.scss';

interface CAddPetCardProps { };

export const CAddPetCard: React.FC<CAddPetCardProps> = () => {
    return (
        <article className='add__pet__card'>
            <section className='add__pet__card__cover'>
                <Link className='add__pet__card__cover__content' to={"#"}>
                    <BsPlusCircleFill />
                    <h3>add pet</h3>
                </Link>
                <h4>more pets <br/> more friends </h4>
            </section>
        </article>
    )
}
