import React from 'react';
import { Link } from 'react-router-dom';
import {AiOutlineEdit} from 'react-icons/ai'
import './CCardEditButton.scss';

interface CCardEditButtonProps { };

export const CCardEditButton: React.FC<CCardEditButtonProps> = () => {
    return (
        <Link to={'#'} className='card__matches__button'> edit <AiOutlineEdit/></Link>
    )
}
