import React from 'react';
import { Link } from 'react-router-dom';

import './Header.scss';

interface HeaderProps { }

const Header: React.FC<HeaderProps> = () => {
    return (
        <header className="container__header">
            <div className="container__header__logo">
                <img src="/logo.png" alt="logo" />
                <h2>PetsMatch</h2>
            </div>

            <Link to="/faq">About / FAQ</Link>
        </header>
    );
};

export default Header;