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
import AdoptionMarketCatalogPage from './pages/AdoptionMarketCatalogPage/AdoptionMarketCatalogPage';
import PetInMarketDetailsPage from './pages/PetInMarketDetailsPage/PetInMarketDetailsPage';
import AddPetInMarket from './pages/AddPetInMarket/AddPetInMarket';
import EditPetInMarket from './pages/EditPetInMarket/EditPetInMarket';
import AdoptionTipsPage from './pages/AdoptionTipsPage/AdoptionTipsPage';
import Page404 from './pages/Page404/Page404';
import AuthGuard from './components/AuthGuard/AuthGuard';

const AllRoutes: React.FC = () => {
    return (
        <Routes>
            <Route path='/' element={<StartUpPage />} />
            <Route path='about-faq' element={<AboutFAQPage />} />
            <Route path='login-register' element={<LoginRegisterPage />} />
            <Route path='dashboard' element={<AuthGuard element={<DashboardPage />} />} />
            <Route path='matches/:id' element={<AuthGuard element={<MatchesChatPage />} />} />
            <Route path='add-pet' element={<AuthGuard element={<AddPetPage />} />} />
            <Route path='pet/:petId/edit' element={<AuthGuard element={<EditPetPage />} />} />
            <Route path='user/edit-profile' element={<AuthGuard element={<EditUserProfilePage />} />} />
            <Route path='market/catalog' element={<AuthGuard element={<AdoptionMarketCatalogPage />} />} />
            <Route path='market/catalog/:petId/details' element={<AuthGuard element={<PetInMarketDetailsPage />} />} />
            <Route path='market/add-pet' element={<AuthGuard element={<AddPetInMarket />} />} />
            <Route path='market/catalog/:petId/edit' element={<AuthGuard element={<EditPetInMarket />} />} />
            <Route path='market/catalog/adoption-tips' element={<AuthGuard element={<AdoptionTipsPage />} />} />
            <Route path='*' element={<Page404 />} />
        </Routes>
    );
};

export default AllRoutes;