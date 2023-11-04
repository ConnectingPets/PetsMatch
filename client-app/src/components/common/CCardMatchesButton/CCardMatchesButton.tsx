import React from 'react';
import { Link } from 'react-router-dom';
import './CCardMatchesButton.scss';

interface CCardMatchesButtonProps { };

export const CCardMatchesButton: React.FC<CCardMatchesButtonProps> = () => {
    return (
        //TODO endpoint
        <Link to={'#'} className='matches__button'> matches</Link>
    )
}
