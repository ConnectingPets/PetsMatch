import React from 'react';
import { useNavigate } from 'react-router-dom';
import { Form, Field } from 'react-final-form';

import { IUser } from '../../interfaces/Interfaces';
import { loginFormValidator } from '../../validators/userProfileFormValidators';
import agent from '../../api/axiosAgent';
import userStore from '../../stores/userStore';

import { CLabel } from '../common/CLabel/CLabel';
import { CSubmitButton } from '../common/CSubmitButton/CSubmitButton';
import './Login.scss';

interface LoginProps {
    showRegister: () => void;
}

export const Login: React.FC<LoginProps> = ({
    showRegister
}) => {
    const navigate = useNavigate();

    const onSubmit = async (values: IUser) => {
        try {
            const { name: Name, token } = await agent.apiUser.login(values);
            const Email = values.Email;
            
            userStore.setUser({ Name, Email }, token);

            navigate('/dashboard');
        } catch (err) {
            console.log(err);
        }
    };

    return (
        <section className='login__form__section'>
            <Form
                onSubmit={onSubmit}
                validate={loginFormValidator}
                render={({ handleSubmit }) => (
                    <form className='login__form' onSubmit={handleSubmit}>

                        <Field name="Email">
                            {({ input, meta }) => (
                                <div>
                                    <CLabel inputName={'Email'} title={'Email'} />
                                    <input className='login__form__input' type="text" {...input} name='Email' id='Email' placeholder="john-sillver@gmail.com" />
                                    {meta.touched && meta.error && <div className='login__form__error__message'>{meta.error}</div>}
                                </div>
                            )}
                        </Field>

                        <Field name="Password">
                            {({ input, meta }) => (
                                <div>
                                    <CLabel inputName={'Password'} title={'Password'} />
                                    <input className='login__form__input' type="password" {...input} name='Password' id='Password' placeholder="* * * * * * *" />
                                    {meta.touched && meta.error && <div className='login__form__error__message'>{meta.error}</div>}
                                </div>
                            )}
                        </Field>

                        <CSubmitButton textContent='Login' />
                        <p className='account__message' onClick={showRegister}>If you don't have an account click here!</p>
                    </form>
                )
                } />
        </section>
    );
};
