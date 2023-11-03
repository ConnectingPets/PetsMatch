import React from 'react';
import { Routes, Route } from 'react-router-dom';
import StartUpPage from './pages/StartUpPage/StartUpPage';
import HomePage from './pages/HomePage/HomePage';
import {LoginRegisterPage} from './pages/LoginRegisterPage/LoginRegisterPage';

import Page404 from './pages/Page404/Page404';

const AllRoutes: React.FC = () => {
    return (
        <Routes>
            <Route path='start-up-page' element={<StartUpPage />} />
            <Route path='/' element={<HomePage />} />
            <Route path='login-register' element={<LoginRegisterPage />} />
            <Route path='*' element={<Page404 />} />
        </Routes>
    );
};

export default AllRoutes;