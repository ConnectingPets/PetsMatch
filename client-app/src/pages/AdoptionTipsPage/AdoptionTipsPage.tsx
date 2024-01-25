import React from 'react';
import { MdOutlinePets } from 'react-icons/md';
import { observer } from 'mobx-react';

import themeStore from '../../stores/themeStore';

import FormsHeader from '../../components/FormsHeader/FormsHeader';
import AdoptionArticle from '../../components/AdoptionArticle/AdoptionArticle';
import { a1ExtendedContent, a2ExtendedContent, a3ExtendedContent, a4ExtendedContent, a5ExtendedContent, a6ExtendedContent, a7ExtendedContent, a8ExtendedContent } from './adoptionArticlesContent';
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

            <AdoptionArticle
                title={'Spend Time With Them Before You Bring Them Home'}
                image={'/images/a2i.webp'}
                content={'Go to the shelter or breeder and meet the dog or cat you want to adopt. Shelters now can offer a lot of information about the animal, but it is not a replacement for actually spending time with them in person.'}
                extendedContent={a2ExtendedContent}
            />

            <AdoptionArticle
                title={'Set Up a Space Just For Them'}
                image={'/images/a3i.jpg'}
                content={'Once you have made the decision to adopt and found your new four-legged family member, it\'s time to get ready to bring them home. Your home will be a completely foreign place for them, so carve out some space just for them.'}
                extendedContent={a3ExtendedContent}
            />

            <AdoptionArticle
                title={'Pet Proof Your Home'}
                image={'/images/a4i.jpg'}
                content={'This is a temporary step while you orient your new pet with your home. Puppies are notorious for chewing up anything within their reach.  And cats love to explore and, well... knock things off counter tops. So... decide where you DON\'T want them to go and close off that part of your home. You can introduce them to these areas after they get settled.'}
                extendedContent={a4ExtendedContent}
            />

            <AdoptionArticle
                title={'Find a Local Veterinarian'}
                image={'/images/a5i.jpg'}
                content={'You should plan to schedule your pet\'s first visit right away.  The shelter will be able to tell you what vaccines they have had and when their next shots are due.  They will also be able to tell you if they have any health issues.'}
                extendedContent={a5ExtendedContent}
            />

            <AdoptionArticle
                title={'Look Into Pet Insurance'}
                image={'/images/a6i.jpg'}
                content={'The average person spends between $300 and $400 per year on their pet\'s care. This doesn\'t include veterinary visits, which can add cost anywhere from $200 for a check up and to $5,000 for treatments and surgeries.'}
                extendedContent={a6ExtendedContent}
            />

            <AdoptionArticle
                title={'Plan to Introduce Them to Other Pets'}
                image={'/images/a7i.jpeg'}
                content={'Introducing your current pet to your new pet is a very important part of your planning process.  Rather than fill this guide with how to navigate this with both dogs and cats, we have gathered the best information we could find for your specific situation.'}
                extendedContent={a7ExtendedContent}
            />

            <AdoptionArticle
                title={'Find a Good Dog Trainer'}
                image={'/images/a8i.jpeg'}
                content={'Conventional wisdom suggests that dog training is actually designed to train the people in the dog\'s life to communicate well with the dog. This makes a lot of sense as your goal with a trainer is to understand what your new furry family member needs, wants, and is trying to communicate with you.  And, when successful, helps the pet know where they fit in the pecking order in the household.'}
                extendedContent={a8ExtendedContent}
            />

            <AdoptionArticle
                title={'Be Patient With Yourself!'}
                image={'/images/a9i.jpg'}
                content={'You will likely make lots of mistakes in your first days and weeks with your new dog or cat.  You are all adjusting to living in the same house and creating a new rhythm together. If things go sideways, refer back to articles just like this one.  Ask your pet parent friends and family how they settled into a routine with their furry family.  And be patient with yourself. You will find your way together.'}
                // extendedContent={a8ExtendedContent}
            />

            <Footer />
        </div>
    );
});

export default AdoptionTipsPage;