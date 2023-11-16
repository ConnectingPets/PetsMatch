import React from 'react';
import { Link, useParams } from 'react-router-dom';
import {AiOutlineEdit} from 'react-icons/ai'
import themeStore from '../../../stores/themeStore';
import './CCardEditButton.scss';

interface CCardEditButtonProps { };

export const CCardEditButton: React.FC<CCardEditButtonProps> = () => {
    // const { petId } = useParams();

    return (    // TO DO add ${petId}
        <Link to={`/pet/abc123/edit`} className={themeStore.isLightTheme ? 'card__edit__button' : 'card__edit__button__dark'}> edit <AiOutlineEdit/></Link>
    )
}
