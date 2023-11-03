import React from 'react';
import { Routes, Route } from 'react-router-dom';
import { DashboardPage } from './pages/DashboardPage/DashboardPage';
import {LoginRegisterPage} from './pages/LoginRegisterPage/LoginRegisterPage';

import Page404 from './pages/Page404/Page404';

const AllRoutes: React.FC = () => {
    return (
        <Routes>
            {/* <Route path='/' element={<HomePage />} /> */}
            <Route path='login-register' element={<LoginRegisterPage />} />
            <Route path='dashboard' element={<DashboardPage/>}/>
            <Route path='*' element={<Page404 />} />
        </Routes>
    );
};

export default AllRoutes;