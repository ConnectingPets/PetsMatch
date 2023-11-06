import React from 'react';
import { BiCopyright } from 'react-icons/bi';

import About from '../../components/About/About';
import Faq from '../../components/FAQ/FAQ';

import './AboutFAQPage.scss';

interface AboutFAQPageProps { }

const AboutFAQPage: React.FC<AboutFAQPageProps> = () => {
    return (
        <div className="info__container">
            <About />

            <Faq />

            <footer>
                <span><BiCopyright /> PetsMatch</span>
            </footer>
        </div>
    );
};

export default AboutFAQPage;