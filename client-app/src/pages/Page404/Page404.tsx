import React from 'react';

import { CLogo } from '../../components/common/CLogo/CLogo';
import Footer from '../../components/Footer/Footer';
import './Page404.scss';

interface Page404Props { }

const Page404: React.FC<Page404Props> = () => {
    return (
        <div className="container-404">
            <div className="container-404__logo">
                <CLogo />
            </div>

            <img className="container-404__img" src="/images/dogs-404.jpg" alt="dogs" />

            <div className="container-404__background">
                <div className="container-404__background__content">
                    <div>
                        <img src="/images/404.png" alt="404" />
                    </div>
                    <div className="container-404__background__content__link">
                        <button onClick={() => window.history.back()}>Go Back</button>
                    </div>
                </div>
            </div>

            <div className="container-404__footer">
                <Footer />
            </div>
        </div>
    );
};

export default Page404;