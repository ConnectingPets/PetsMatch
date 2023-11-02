import React from 'react';
import { CLabel } from '../common/CLabel/CLabel';
import { CInput } from '../common/CInput/CInput';
import { CSubmitButton } from '../common/CSubmitButton/CSubmitButton';
import './Register.scss';

interface RegisterProps { }

export const Register: React.FC<RegisterProps> = () => {
    return (
        <section className='register__form__section'>
            <form className='register__form'>
                <div>
                    <CLabel inputName={'fullName'} title={'Full Name'} />
                    <CInput type='text' id='fullName' name='fullName' placeholder='John Sillver' />
                </div>
                <div>
                    <CLabel inputName={'email'} title={'Email'} />
                    <CInput type='email' id='email' name='email' placeholder='john-sillver@gmail.com' />
                </div>
                <div>
                    <CLabel inputName={'password'} title={'Password'} />
                    <CInput type='password' id='password' name='password' placeholder='* * * * * * *' />
                </div>
                <div>
                    <CLabel inputName={'password'} title={'Retype Password'} />
                    <CInput type='password' id='password' name='password' placeholder='* * * * * * *' />
                </div>
                <CSubmitButton textContent='Register' />
            </form>
        </section>
    )
}
