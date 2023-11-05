import React from 'react';
import { Link } from 'react-router-dom';
import { AiOutlineFire } from 'react-icons/ai';
import './CCardMatchesButton.scss';

interface CCardMatchesButtonProps { };

export const CCardMatchesButton: React.FC<CCardMatchesButtonProps> = () => {
    return (
        //TODO endpoint
        <Link to={'#'} className='card__matches__button'> matches <AiOutlineFire/></Link>
    )
}
