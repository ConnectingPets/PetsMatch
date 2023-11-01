import React from 'react';
import './Login.scss';
import { CLabel } from '../common/CLabel/CLabel';

interface LoginProps { };

export const Login: React.FC<LoginProps> = () => {
    return (
        <section className='login__form__section'>
            <form>
               <div>
                    <CLabel inputName={'username'} title={'Username'} />
               </div>
            </form>
        </section>
    )
}
