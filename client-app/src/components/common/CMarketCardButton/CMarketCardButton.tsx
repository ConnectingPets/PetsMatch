import React from 'react';
import { Link } from 'react-router-dom';
import {AiOutlineEdit} from 'react-icons/ai';
import {FaEye} from 'react-icons/fa';

import themeStore from '../../../stores/themeStore';
import './CMarketCardButton.scss';

interface CMarketCardButtonProps {
    id: string,
    button: string
}

export const CMarketCardButton: React.FC<CMarketCardButtonProps> = ({ id, button }) => {

    return (
        <Link to={`/market/pet/${id}/${button == 'edit' ? 'edit' : ''}`} className={themeStore.isLightTheme ? 'card__button' : 'card__button__dark'}>{button == 'edit' ? 'edit' : 'view'}{button == 'edit' ? <AiOutlineEdit/> : <FaEye />}</Link>
    );
};
