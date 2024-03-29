import React from 'react';
import { useNavigate } from 'react-router-dom';
import { Form, Field } from 'react-final-form';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

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

export const Login: React.FC<LoginProps> = () => {
    const navigate = useNavigate();

    const onSubmit = async (values: IUser) => {
        const RememberMe = values.RememberMe;

        try {
            const result = await agent.apiUser.login(values);

            if (result.isSuccess) {
                if (RememberMe) {
                    const {
                        name: Name,
                        photoUrl: PhotoUrl,
                        age: Age,
                        jobTitle: JobTitle,
                        city: City,
                        address: Address,
                        education: Education,
                        gender: Gender,
                        token
                    } = result.data;
                    const Email = values.Email;

                    userStore.setUser({
                        Name,
                        PhotoUrl,
                        Age,
                        Email,
                        JobTitle,
                        City,
                        Address,
                        Education,
                        Gender,
                        Roles: []
                    }, token);
                }

                userStore.setIsLoggedIn();

                navigate('/dashboard');
            } else {
                toast.error(result.errorMessage);
            }
        } catch (err) {
            console.log(err);

            toast.error('Login failed. Please check your credentials.');
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

                        <div className='login__form__remember-me'>
                            <Field type="checkbox" name='RememberMe'>
                                {({ input }) => (
                                    <div>
                                        <CLabel inputName='RememberMe' title='Remember me' />
                                        <input type="checkbox" {...input} name='RememberMe' id='RememberMe' />
                                    </div>
                                )}
                            </Field>
                        </div>

                        <CSubmitButton textContent='Login' />
                    </form>
                )
                } />
        </section>
    );
};
