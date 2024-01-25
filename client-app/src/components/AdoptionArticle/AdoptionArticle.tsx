import React, { ReactElement, useState } from 'react';
import { MdOutlinePets } from 'react-icons/md';

import themeStore from '../../stores/themeStore';

interface AdoptionArticleProps {
    title: string
    image: string
    content: string
    extendedContent?: ReactElement
}

const AdoptionArticle: React.FC<AdoptionArticleProps> = ({ title, image, content, extendedContent }) => {
    const [isExtended, setIsExtended] = useState<boolean>(false);

    const onToggleExtended = () => {
        setIsExtended(!isExtended);
    };

    return (
        <article className={themeStore.isLightTheme ? 'adoption-tips-wrapper__article ' : 'adoption-tips-wrapper__article adoption-tips-wrapper__article__dark '}>
            {!isExtended && (
                <div className='adoption-tips-wrapper__article__image'>
                    <img src={image} alt="pet image" />
                </div>
            )}

            <div onClick={onToggleExtended} className='adoption-tips-wrapper__article__content'>
                <h3>{title}</h3>
                {isExtended && <span><MdOutlinePets /></span>}
                <p>{content}</p>
                {isExtended && (
                    <>
                        {extendedContent}
                    </>
                )}
                {isExtended && <span><MdOutlinePets /></span>}
                <button>{isExtended ? 'Show less' : 'Show more'}</button>
            </div>
        </article>
    );
};

export default AdoptionArticle;