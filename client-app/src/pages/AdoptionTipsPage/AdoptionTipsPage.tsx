import React, { useState } from 'react';
import { MdOutlinePets } from 'react-icons/md';
import { observer } from 'mobx-react';

import themeStore from '../../stores/themeStore';
import { IAdoptionArticle } from '../../interfaces/Interfaces';

import { adoptionArticles } from './adoptionArticlesArray';
import ScrollToTop from '../../components/ScrollToTop/ScrollToTop';
import FormsHeader from '../../components/FormsHeader/FormsHeader';
import AdoptionArticle from '../../components/AdoptionArticle/AdoptionArticle';
import Footer from '../../components/Footer/Footer';
import './AdoptionTipsPage.scss';

interface AdoptionTipsPageProps { }

const AdoptionTipsPage: React.FC<AdoptionTipsPageProps> = observer(() => {
    const [currentPage, setCurrentPage] = useState<number>(1);
    const articlesPerPage = 6;

    const indexOfLastArticle = currentPage * articlesPerPage;
    const indexOfFirstArticle = indexOfLastArticle - articlesPerPage;
    const currentArticles = adoptionArticles.slice(indexOfFirstArticle, indexOfLastArticle);

    const paginate = (page: number) => setCurrentPage(page);

    return (
        <div className={themeStore.isLightTheme ? 'adoption-tips-wrapper' : 'adoption-tips-wrapper adoption-tips-wrapper__dark'}>
            <ScrollToTop page={currentPage} />

            <FormsHeader title='Adoption Tips' />
            <h3>Things To Do Before You Adopt a Pet</h3>

            {currentPage == 1 && (
                <article className={themeStore.isLightTheme ? 'adoption-tips-wrapper__article ' : 'adoption-tips-wrapper__article adoption-tips-wrapper__article__dark '}>
                    <div className='adoption-tips-wrapper__article__image'>
                        <img src="/images/ai.jpg" alt="pet image" />
                    </div>
                    
                    <div className='adoption-tips-wrapper__article__first'>
                        <span><MdOutlinePets /></span>
                        <p>So you've made the decision to get a dog or cat. Hooray! Pets are a lot of fun to play with and can be exceptional cuddling partners. In short, they are fantastic companions and even incredible support when we feel depressed and anxious.</p>

                        <p>A dog or cat is a multi-year commitment of care, which includes ensuring their safety, feeding and housing them, and keeping them healthy. Yep, there's a lot to know. So let's get you ready to bring home your new furry family member.</p>
                    </div>
                </article>
            )}

            {currentArticles.map((article: IAdoptionArticle, index: React.Key | null | undefined) => (
                <AdoptionArticle
                    key={index}
                    title={article.title}
                    image={article.image}
                    content={article.content}
                    extendedContent={article.extendedContent}
                />
            ))}

            <div className='adoption-tips-wrapper__pagination-btn'>
                {Array.from({ length: Math.ceil(adoptionArticles.length / articlesPerPage) }).map((_, index) => (
                    <button onClick={() => paginate(index + 1)} key={index}>
                        {index + 1}
                    </button>
                ))}
            </div>

            <Footer />
        </div>
    );
});

export default AdoptionTipsPage;