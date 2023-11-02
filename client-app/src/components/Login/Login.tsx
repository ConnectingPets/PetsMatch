import React from 'react';
import { CLabel } from '../common/CLabel/CLabel';
import { CInput } from '../common/CInput/CInput';
import { CSubmitButton } from '../common/CSubmitButton/CSubmitButton';
import './Login.scss';

interface LoginProps {
    showRegister: () => void;
};

export const Login: React.FC<LoginProps> = ({
    showRegister
}) => {
    return (
        <section className='login__form__section'>
            <form className='login__form'>
                <div>
                    <CLabel inputName={'email'} title={'Email'} />
                    <CInput type='email' id='email' name='email' placeholder='john-sillver@gmail.com' />
                </div>
                <div>
                    <CLabel inputName={'password'} title={'Password'} />
                    <CInput type='password' id='password' name='password' placeholder='* * * * * * *' />
                </div>
                <CSubmitButton textContent='Login' />
                <p className='account__message' onClick={showRegister}>If you don't have an account click here!</p>
            </form>
        </section>
    )
}
