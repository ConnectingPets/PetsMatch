import React from 'react';
import { MdOutlinePets } from 'react-icons/md';
import { observer } from 'mobx-react';

import themeStore from '../../stores/themeStore';

import FormsHeader from '../../components/FormsHeader/FormsHeader';
import AdoptionArticle from '../../components/AdoptionArticle/AdoptionArticle';
import { a1ExtendedContent } from './adoptionArticlesContent';
import Footer from '../../components/Footer/Footer';
import './AdoptionTipsPage.scss';

interface AdoptionTipsPageProps { }

const AdoptionTipsPage: React.FC<AdoptionTipsPageProps> = observer(() => {
    return (
        <div className={themeStore.isLightTheme ? 'adoption-tips-wrapper' : 'adoption-tips-wrapper adoption-tips-wrapper__dark'}>
            <FormsHeader title='Adoption Tips' />
            <h3>Things To Do Before You Adopt a Pet</h3>

            <article className={themeStore.isLightTheme ? 'adoption-tips-wrapper__article ' : 'adoption-tips-wrapper__article adoption-tips-wrapper__article__dark '}>
                <div className='adoption-tips-wrapper__article__first'>
                    <span><MdOutlinePets /></span>
                    <p>So you've made the decision to get a dog or cat. Hooray! Pets are a lot of fun to play with and can be exceptional cuddling partners. In short, they are fantastic companions and even incredible support when we feel depressed and anxious.</p>

                    <p>A dog or cat is a multi-year commitment of care, which includes ensuring their safety, feeding and housing them, and keeping them healthy. Yep, there's a lot to know. So let's get you ready to bring home your new furry family member.</p>
                </div>

                <div className='adoption-tips-wrapper__article__image'>
                    <img src="/images/ai.jpg" alt="pet image" />
                </div>
            </article>

            <AdoptionArticle
                title={'Prepare Yourself and Your Family'}
                image={'/images/a1i.jpeg'}
                content={'Before you take the leap of adopting a new pet, you might consider fostering a dog or cat from a local shelter. Our friends at PetFinder make it easy to find local pet shelters so you can find out about fostering as well as find that new family member to adopt.'}
                extendedContent={a1ExtendedContent}
            />

            <Footer />
        </div>
    );
});

export default AdoptionTipsPage;