import React, { useState } from 'react';

import './LoginRegisterPage.scss';
import { Login } from '../../components/Login/Login';

interface LoginRegisterPageProps { };

export const LoginRegisterPage: React.FC<LoginRegisterPageProps> = () => {

    const [isRegistered, setIsRegistered] = useState<boolean>(true);

    const showLogin = () => {
        setIsRegistered(true);
        console.log(isRegistered);
    };

    const showRegister = () => {
        setIsRegistered(false);
        console.log(isRegistered);

    };



    return (
        <div className='login__register__wrapper'>
            <section className='login__register__options'>
                <div>
                    <h3 onClick={showLogin}>login</h3>
                </div>
                <div>
                    <h3 onClick={showRegister}>register</h3>
                </div>
            </section>

            <section className='login__register__forms__wrapper'>
                {
                    isRegistered
                        ? <Login />
                        : null
                }
            </section>
        </div>
    )
}
