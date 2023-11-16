import React from 'react';
import './CSubmitButton.scss';
import themeStore from '../../../stores/themeStore';

interface CSubmitButtonProps {
    textContent: string,
}

export const CSubmitButton: React.FC<CSubmitButtonProps> = ({
    textContent
}) => {
    return (
        <input className={themeStore.isLightTheme ? 'form__submit__button' : 'form__submit__button__dark'} type="submit" value={textContent} />
    )
}
