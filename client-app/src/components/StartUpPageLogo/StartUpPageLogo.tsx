import React from 'react';
import { Link } from 'react-router-dom';

import './startUpPageLogo.scss';

interface StartUpPageLogoProps { }

const StartUpPageLogo: React.FC<StartUpPageLogoProps> = () => {
    return (
        <Link to="/">
            <img src="/logo.png" alt="logo" />
            <h2>Pets Match</h2>
        </Link>
    );
};

export default StartUpPageLogo;