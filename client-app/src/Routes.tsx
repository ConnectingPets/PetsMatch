import React from 'react';

import { Routes, Route } from 'react-router-dom';

import HomePage from './pages/HomePage/HomePage';
import Login from './pages/Login/Login';
import Register from './pages/Register/Register';
import Page404 from './pages/Page404/Page404';

const AllRoutes: React.FC = () => {
    return (
        <Routes>
            <Route path='/' element={<HomePage />} />
            <Route path='login' element={<Login />} />
            <Route path='register' element={<Register />} />
            <Route path='*' element={<Page404 />} />
        </Routes>
    );
};

export default AllRoutes;