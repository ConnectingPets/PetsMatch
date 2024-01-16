import React,  { useEffect } from 'react';
import { Link, useNavigate } from 'react-router-dom';

import Header from '../../components/Header/Header';
import Footer from '../../components/Footer/Footer';

import './StartUpPage.scss';

interface StartUpPageProps { }

const StartUpPage: React.FC<StartUpPageProps> = () => {
    const navigate = useNavigate();

    useEffect(() => {
        const isLocalStorageUser = localStorage.getItem('token');

        if (isLocalStorageUser) {
            navigate('/dashboard');
        }
    }, [navigate]);

    return (
        <div className="container">
            <Header />

            <div className="container__img-wrapper">
                <img src="/images/dog-star.avif" alt="dog" />
            </div>

            <div className="container__overlay">
                <div>
                    <h1>Hello, and welcome to Pets Match</h1>
                    <Link to="/login-register">Login / Sign Up</Link>
                </div>
            </div>

            <div className="container__footer">
                <Footer />
            </div>
        </div>
    );
};

export default StartUpPage;