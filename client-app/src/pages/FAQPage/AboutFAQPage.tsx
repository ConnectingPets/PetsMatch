import React from 'react';

import StartUpPageLogo from '../../components/StartUpPageLogo/StartUpPageLogo';
import About from '../../components/About/About';
import Faq from '../../components/FAQ/FAQ';
import Footer from '../../components/Footer/Footer';

import './AboutFAQPage.scss';

interface AboutFAQPageProps { }

const AboutFAQPage: React.FC<AboutFAQPageProps> = () => {
    return (
        <div className="info__container">
            <header className="info__container__header">
                <StartUpPageLogo />
            </header>

            <About />

            <Faq />

            <div className="info__container__footer">
                <Footer />
            </div>
        </div>
    );
};

export default AboutFAQPage;