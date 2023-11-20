import React from 'react';
import { Link } from 'react-router-dom';
import { FaShieldDog } from 'react-icons/fa6';
import { observer } from 'mobx-react';
import themeStore from '../../../stores/themeStore';
import './CMatchesHeader.scss';

interface CMatchesHeaderProps { }

export const CMatchesHeader: React.FC<CMatchesHeaderProps> = observer(() => {
    return (
        <section className={themeStore.isLightTheme ? 'matches__header' : 'matches__header  matches__header__dark'}>
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
})
