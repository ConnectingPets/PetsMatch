import React from 'react';
import { Link } from 'react-router-dom';
import { AiOutlineFire } from 'react-icons/ai';
import themeStore from '../../../stores/themeStore';
import './CCardMatchesButton.scss';

interface CCardMatchesButtonProps {
    id: string
}

export const CCardMatchesButton: React.FC<CCardMatchesButtonProps> = ({ 
    id
}) => {
    return (
        <Link to={`/matches/${id}`} className={themeStore.isLightTheme ? 'card__matches__button' : 'card__matches__button__dark'}> matches <AiOutlineFire/></Link>
    );
};
