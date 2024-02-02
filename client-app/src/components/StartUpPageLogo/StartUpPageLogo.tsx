import React from 'react';
import { Link } from 'react-router-dom';

import userStore from '../../stores/userStore';
import './StartUpPageLogo.scss';

interface StartUpPageLogoProps { }

const StartUpPageLogo: React.FC<StartUpPageLogoProps> = () => {
    return (
        <Link to={userStore.isLoggedIn ? '/dashboard' : '/'}>
            <img src="/logo.png" alt="logo" />
            <h2>Pets Match</h2>
        </Link>
    );
};

export default StartUpPageLogo;