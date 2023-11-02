import React from 'react';

import { BiCopyright } from 'react-icons/bi';

import Header from '../../components/Header/Header';

import './StartUpPage.scss';

const StartUpPage: React.FC = () => {
    return (
        <div className="container">
            <Header />

            <div className="container__img-wrapper">
                <img src="/images/dog-with-butterflies.jpg" alt="happy-dog" />

                {/* <img src="/images/happy-dog.avif" alt="happy-dog" /> */}

                {/* <img src="/images/cat-with-comp.avif" alt="happy-dog" /> */}
            </div>

            <div className="container__overlay">
                <div>
                    <h1>Hello, and welcome to PetsMatch App</h1>
                    <a href="/login-register">Login / Sign Up</a>
                </div>
            </div>

            <div className="container__footer">
                <span><BiCopyright /> PetsMatch</span>
            </div>
        </div>
    );
};

export default StartUpPage;