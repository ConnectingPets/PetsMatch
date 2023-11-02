import React from 'react';

import './Header.scss';

const Header: React.FC = () => {
    return (
        <header className="container__header">
            <div className="container__header__logo">
                <img src="/logo.png" alt="logo" />
                <h2>PetsMatch</h2>
            </div>

            <a href="/faq">FAQ</a>
        </header>
    );
};

export default Header;