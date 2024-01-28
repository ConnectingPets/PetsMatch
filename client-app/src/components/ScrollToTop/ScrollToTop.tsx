import React, { useEffect } from 'react';
import { useLocation } from 'react-router-dom';

interface ScrollToTopProps {
    page?: number
}

const ScrollToTop: React.FC<ScrollToTopProps> = ({ page }) => {
    const { pathname } = useLocation();

    useEffect(() => {
        window.scrollTo(0, 0);
    }, [pathname, page]);

    return null;
};

export default ScrollToTop;