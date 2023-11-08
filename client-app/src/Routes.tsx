import React from 'react';
import { Routes, Route } from 'react-router-dom';
import { LoginRegisterPage } from './pages/LoginRegisterPage/LoginRegisterPage';
import Page404 from './pages/Page404/Page404';
import { DashboardPage } from './pages/DashboardPage/DashboardPage';
import AboutFAQPage from './pages/FAQPage/AboutFAQPage';
import StartUpPage from './pages/StartUpPage/StartUpPage';
import { MatchesChatPage } from './pages/MatchesChatPage/MatchesChatPage';

const AllRoutes: React.FC = () => {
    return (
        <Routes>
            <Route path='/' element={<StartUpPage />} />
            <Route path='about-faq' element={<AboutFAQPage />} />
            <Route path='login-register' element={<LoginRegisterPage />} />
            <Route path='dashboard' element={<DashboardPage />} />
            <Route path='matches/:id' element={<MatchesChatPage />} />
            <Route path='*' element={<Page404 />} />
        </Routes>
    );
};

export default AllRoutes;