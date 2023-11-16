import React from 'react';
import { Routes, Route } from 'react-router-dom';

import AboutFAQPage from './pages/FAQPage/AboutFAQPage';
import StartUpPage from './pages/StartUpPage/StartUpPage';
import { LoginRegisterPage } from './pages/LoginRegisterPage/LoginRegisterPage';
import { DashboardPage } from './pages/DashboardPage/DashboardPage';
import AddPetPage from './pages/AddPetPage/AddPetPage';
import EditPetPage from './pages/EditPetPage/EditPetPage';
import Page404 from './pages/Page404/Page404';

const AllRoutes: React.FC = () => {
    return (
        <Routes>
            <Route path='/' element={<StartUpPage />} />
            <Route path='about-faq' element={<AboutFAQPage />} />
            <Route path='login-register' element={<LoginRegisterPage />} />
            <Route path='dashboard' element={<DashboardPage />} />
            <Route path='add-pet' element={<AddPetPage />} />
            <Route path='pet/:petId/edit' element={<EditPetPage />} />
            <Route path='*' element={<Page404 />} />
        </Routes>
    );
};

export default AllRoutes;