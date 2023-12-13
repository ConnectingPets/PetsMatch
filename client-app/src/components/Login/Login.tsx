import React from 'react';
import { Form, Field } from 'react-final-form';
import { CLabel } from '../common/CLabel/CLabel';
import { CSubmitButton } from '../common/CSubmitButton/CSubmitButton';
import './Login.scss';

interface LoginProps {
    showRegister: () => void;
};

interface Errors {
    email: string | undefined,
    password: string | undefined,
}


export const Login: React.FC<LoginProps> = ({
    showRegister
}) => {

    const onSubmit = () => {
        // TODO
    }

    const validate = (e: any) => {
        const errors: Errors = {
            email: undefined,
            password: undefined,
        };

        if (e.email && e.email.length < 8) {
            errors.email = 'to short'
        }
        if (e.password && e.password.length < 8) {
            errors.password = 'to short'
        }

        return errors;
    }

    return (
        <section className='login__form__section'>
            <Form
                onSubmit={onSubmit}
                validate={validate}
                render={({ handleSubmit }) => (
                    <form className='login__form' onSubmit={handleSubmit}>

                        <Field name="email">
                            {({ input, meta }) => (
                                <div>
                                    <CLabel inputName={'email'} title={'Email'} />
                                    <input className='login__form__input' type="email" {...input} name='email' id='email' placeholder="john-sillver@gmail.com" />
                                    {meta.touched && meta.error && <div className='login__form__error__message'>{meta.error}</div>}
                                </div>
                            )}
                        </Field>

                        <Field name="password">
                            {({ input, meta }) => (
                                <div>
                                    <CLabel inputName={'password'} title={'Password'} />
                                    <input className='login__form__input' type="password" {...input} name='password' id='password' placeholder="* * * * * * *" />
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
    )
}
