import React from 'react';
import { Link } from 'react-router-dom';
import { AiOutlineEdit } from 'react-icons/ai';
import { FaEye } from 'react-icons/fa';
import { CiShop } from 'react-icons/ci';
import { GiShoppingCart } from 'react-icons/gi';
import { MdOutlinePets } from 'react-icons/md';

import themeStore from '../../../stores/themeStore';
import './CMarketCardButton.scss';

interface CMarketCardButtonProps {
    id?: string,
    button: string
}

export const CMarketCardButton: React.FC<CMarketCardButtonProps> = ({ id, button }) => {

    return (
        <>
            {button == 'Go to Market' && <Link to={'/market/catalog'} className={themeStore.isLightTheme ? 'card__button market' : 'card__button__dark market'}>Go to Market <CiShop /></Link>}

            {button == 'details' && <Link to={`/market/catalog/${id}/details`} className={themeStore.isLightTheme ? 'card__button' : 'card__button__dark'}>details <FaEye /></Link>}

            {button == 'edit' && <Link to={`/market/catalog/${id}/edit`} className={themeStore.isLightTheme ? 'card__button' : 'card__button__dark'}>edit <AiOutlineEdit /></Link>}

            {button == 'buy' && <Link to={`/market/catalog/${id}/buy`} className={themeStore.isLightTheme ? 'card__button' : 'card__button__dark'}>buy <GiShoppingCart /></Link>}

            {button == 'adopt' && <Link to={`/market/catalog/${id}/adopt`} className={themeStore.isLightTheme ? 'card__button' : 'card__button__dark'}>adopt <MdOutlinePets /></Link>}
        </>
    );
};
