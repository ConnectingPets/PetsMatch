import React from 'react';
import { observer } from 'mobx-react';
import themeStore from '../../../stores/themeStore';
import { FaSun, FaMoon } from 'react-icons/fa';
import './CChangeThemeButton.scss';

interface CChangeThemeButtonProps { }

export const CChangeThemeButton: React.FC<CChangeThemeButtonProps> = observer(() => {
    return (
        <div className='change__theme__button' onClick={() => themeStore.changeTheme()}>
            <FaMoon />
            <FaSun />
            <span className={themeStore.isLightTheme ? 'span__left': 'span__right'}></span>
        </div>
    )
})
