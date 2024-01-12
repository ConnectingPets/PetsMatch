import React from 'react';
import { Routes, Route } from 'react-router-dom';

import AboutFAQPage from './pages/FAQPage/AboutFAQPage';
import StartUpPage from './pages/StartUpPage/StartUpPage';
import { MatchesChatPage } from './pages/MatchesChatPage/MatchesChatPage';
import { LoginRegisterPage } from './pages/LoginRegisterPage/LoginRegisterPage';
import { DashboardPage } from './pages/DashboardPage/DashboardPage';
import AddPetPage from './pages/AddPetPage/AddPetPage';
import EditPetPage from './pages/EditPetPage/EditPetPage';
import EditUserProfilePage from './pages/EditUserProfilePage/EditUserProfilePage';
import AddPetInMarket from './pages/AddPetInMarket/AddPetInMarket';
import EditPetInMarket from './pages/EditPetInMarket/EditPetInMarket';
import Page404 from './pages/Page404/Page404';

const AllRoutes: React.FC = () => {
    return (
        <Routes>
            <Route path='/' element={<StartUpPage />} />
            <Route path='about-faq' element={<AboutFAQPage />} />
            <Route path='login-register' element={<LoginRegisterPage />} />
            <Route path='dashboard' element={<DashboardPage />} />
            <Route path='matches/:id' element={<MatchesChatPage />} />
            <Route path='add-pet' element={<AddPetPage />} />
            <Route path='pet/:petId/edit' element={<EditPetPage />} />
            <Route path='user/edit-profile' element={<EditUserProfilePage />} />
            <Route path='market/add-pet' element={<AddPetInMarket />} />
            <Route path='market/pet/:petId/edit' element={<EditPetInMarket />} />
            <Route path='*' element={<Page404 />} />
        </Routes>
    );
};

export default AllRoutes;