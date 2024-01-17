import { Link } from 'react-router-dom';
import './CLogo.scss';

export const CLogo = () => {
    return (
        <Link to='/dashboard' className="logo__image__wrapper">
            <img src="/logo.png" alt="logo" />
        </Link>
    );
};
