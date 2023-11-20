import React from 'react';

import themeStore from '../../stores/themeStore';

import { CLogo } from '../../components/common/CLogo/CLogo';
import { CChangeThemeButton } from '../../components/common/CChangeThemeButton/CChangeThemeButton';

import './FormsHeader.scss';

interface FormsHeaderProps {
    title: string
}

const FormsHeader: React.FC<FormsHeaderProps> = ({ title }) => {
    return (
        <header className="forms-header">
            <CLogo />
            <h1 className={themeStore.isLightTheme ? '' : 'dark'}>{title}</h1>
            <CChangeThemeButton />
        </header>
    );
};

export default FormsHeader;