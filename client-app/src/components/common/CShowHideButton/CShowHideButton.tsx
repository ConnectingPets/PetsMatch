import React, { useState } from 'react';
import { observer } from 'mobx-react';
import './CShowHideButton.scss';


interface CShowHideButtonProps {
    param: string,
    clickHandler: () => void
};

export const CShowHideButton: React.FC<CShowHideButtonProps> = observer(({
    param,
    clickHandler
}) => {

    const [isShown, setIsShown] = useState(true);

    const showHideHandler = () => {
        clickHandler();
        setIsShown(!isShown);
        // console.log(isShown);

    }

    return (
        <div className='show__hide__button' onClick={showHideHandler}>
            <div className={!isShown ? 'show__button__color' : 'hide__button__color'}>
                {
                    isShown
                        ? <h5>hide</h5>
                        : <h5>show</h5>
                }

            </div>
            <div>
                <p>{param}</p>
            </div>
        </div>
    )
})
    