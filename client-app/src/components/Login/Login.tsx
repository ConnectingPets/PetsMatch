import React from 'react';
import { CLabel } from '../common/CLabel/CLabel';
import { CInput } from '../common/CInput/CInput';
import './Login.scss';
import { CSubmitButton } from '../common/CSubmitButton/CSubmitButton';

interface LoginProps { };

export const Login: React.FC<LoginProps> = () => {
    return (
        <section className='login__form__section'>
            <form className='login__form'>
               <div>
                    <CLabel inputName={'email'} title={'Email'} />
                    <CInput type='email' id='email' name='email' placeholder='john-sillver@gmail.com'/>
               </div>
               <div>
                    <CLabel inputName={'password'} title={'Password'} />
                    <CInput type='password' id='password' name='password' placeholder='* * * * * * *'/>
               </div>
               <CSubmitButton textContent='Login'/>
            </form>
        </section>
    )
}
