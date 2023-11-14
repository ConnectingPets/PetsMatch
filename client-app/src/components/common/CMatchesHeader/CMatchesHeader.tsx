import React from 'react';
import { Link } from 'react-router-dom';
import { FaShieldDog } from 'react-icons/fa6';
import './CMatchesHeader.scss';

interface CMatchesHeaderProps { }

export const CMatchesHeader: React.FC<CMatchesHeaderProps> = () => {
    return (
        <section className='matches__header'>
            <article className='matches__header__user'>
                <div className='matches__header__user__image'>
                    <img src="/images/dog-star.avif" alt="" />
                </div>
                <h5>ben affleck</h5>
            </article>
            <Link to={'/about-faq'}>
                <FaShieldDog />
            </Link>
        </section>
    )
}
