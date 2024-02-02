import { Link } from 'react-router-dom';

import userStore from '../../../stores/userStore';
import './CLogo.scss';

export const CLogo = () => {
    return (
        <Link to={userStore.isLoggedIn ? '/dashboard' : '/'} className="logo__image__wrapper">
            <img src="/logo.png" alt="logo" />
        </Link>
    );
};
