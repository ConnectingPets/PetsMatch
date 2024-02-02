import React from 'react';
import { Navigate } from 'react-router-dom';

import userStore from '../../stores/userStore';

interface AuthGuardProps {
    element: React.ReactElement
}

const AuthGuard: React.FC<AuthGuardProps> = ({ element }) => {
    return (
        <>
            {userStore.isLoggedIn
                ? element
                : <Navigate to={'/'} />}
        </>
    );
};

export default AuthGuard;