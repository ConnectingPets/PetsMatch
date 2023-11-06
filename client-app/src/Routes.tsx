import React from 'react';
import { Routes, Route } from 'react-router-dom';
import StartUpPage from './pages/StartUpPage/StartUpPage';
import AboutFAQPage from './pages/FAQPage/AboutFAQPage';
import HomePage from './pages/HomePage/HomePage';
import {LoginRegisterPage} from './pages/LoginRegisterPage/LoginRegisterPage';

import Page404 from './pages/Page404/Page404';

const AllRoutes: React.FC = () => {
    return (
        <Routes>
            <Route path='/' element={<StartUpPage />} />
            <Route path='about-faq' element={<AboutFAQPage />} />
            <Route path='dashboard' element={<HomePage />} />
            <Route path='login-register' element={<LoginRegisterPage />} />
            <Route path='*' element={<Page404 />} />
        </Routes>
    );
};

export default AllRoutes;