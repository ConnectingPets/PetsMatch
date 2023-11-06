import React from 'react';
import { Link } from 'react-router-dom';

import { BiCopyright } from 'react-icons/bi';

import Header from '../../components/Header/Header';

import './StartUpPage.scss';

interface StartUpPageProps { }

const StartUpPage: React.FC<StartUpPageProps> = () => {
    return (
        <div className="container">
            <Header />

            <div className="container__img-wrapper">
                <img src="/images/dog-star.avif" alt="dog" />
            </div>

            <div className="container__overlay">
                <div>
                    <h1>Hello, and welcome to PetsMatch</h1>
                    <Link to="/login-register">Login / Sign Up</Link>
                </div>
            </div>

            <div className="container__footer">
                <span><BiCopyright /> PetsMatch</span>
            </div>
        </div>
    );
};

export default StartUpPage;