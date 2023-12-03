import React from 'react';
import { Link } from 'react-router-dom';
import {AiOutlineEdit} from 'react-icons/ai';
import themeStore from '../../../stores/themeStore';
import './CCardEditButton.scss';

interface CCardEditButtonProps {
    id: string
}

export const CCardEditButton: React.FC<CCardEditButtonProps> = ({ id }) => {

    return (
        <Link to={`/pet/${id}/edit`} className={themeStore.isLightTheme ? 'card__edit__button' : 'card__edit__button__dark'}> edit <AiOutlineEdit/></Link>
    );
};
