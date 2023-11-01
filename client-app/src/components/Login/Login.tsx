import React from 'react';
import './Login.scss';
import { CLabel } from '../common/CLabel/CLabel';
import { CInput } from '../common/CInput/CInput';

interface LoginProps { };

export const Login: React.FC<LoginProps> = () => {
    return (
        <section className='login__form__section'>
            <form>
               <div>
                    <CLabel inputName={'username'} title={'Username'} />
                    <CInput type='text' id='username' name='username' placeholder='John Sillver'/>
               </div>
            </form>
        </section>
    )
}
