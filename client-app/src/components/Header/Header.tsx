import React from 'react';
import { Link } from 'react-router-dom';

import StartUpPageLogo from '../StartUpPageLogo/StartUpPageLogo';
import './Header.scss';

interface HeaderProps { }

const Header: React.FC<HeaderProps> = () => {
    return (
        <header className="container__header">
            <div className="container__header__logo">
                <StartUpPageLogo />
            </div>

            <Link to="/about-faq">About / FAQ</Link>
        </header>
    );
};

export default Header;