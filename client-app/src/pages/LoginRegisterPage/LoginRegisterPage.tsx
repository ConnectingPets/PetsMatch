import React, { useState } from 'react';
import { Login } from '../../components/Login/Login';
import { Register } from '../../components/Register/Register';
import './LoginRegisterPage.scss';

interface LoginRegisterPageProps { }

export const LoginRegisterPage: React.FC<LoginRegisterPageProps> = () => {

    const [isRegistered, setIsRegistered] = useState<boolean>(true);

    const showLogin = () => {
        setIsRegistered(true);
    };

    const showRegister = () => {
        setIsRegistered(false);
    };

    return (
        <div className='login__register__wrapper'>
            <section className='login__register__options'>
                <div>
                    <h3 className={isRegistered ? 'login__register__option': ''} onClick={showLogin}>login</h3>
                </div>
                <div>
                    <h3 className={!isRegistered ? 'login__register__option': ''} onClick={showRegister}>register</h3>
                </div>
            </section>

            <section className='login__register__forms__wrapper'>
                {
                    isRegistered
                        ? <Login showRegister={showRegister}/>
                        : <Register showLogin={showLogin}/>
                }
            </section>
        </div>
    )
}
