import React from 'react';
import { Form, Field } from 'react-final-form';

import { IUser } from '../../interfaces/Interfaces';
import { userProfileFormValidator } from '../../validators/userProfileFormValidator';

import { CLabel } from '../common/CLabel/CLabel';
import { CSubmitButton } from '../common/CSubmitButton/CSubmitButton';
import './Register.scss';

interface RegisterProps {
    showLogin: () => void;
}

export const Register: React.FC<RegisterProps> = ({
    showLogin
}) => {

    const onSubmit = (values: IUser) => {
        console.log(values);
    };

    return (
        <section className='register__form__section'>
            <Form
                onSubmit={onSubmit}
                validate={userProfileFormValidator}
                render={({ handleSubmit }) => (
                    <form className='register__form' onSubmit={handleSubmit}>

                        <Field name="Name">
                            {({ input, meta }) => (
                                <div>
                                    <CLabel inputName={'Name'} title={'Name'} />
                                    <input className='register__form__input' type="text" {...input} name='Name' id='Name' placeholder="John Sillver" />
                                    {meta.touched && meta.error && <div className='register__form__error__message'>{meta.error}</div>}
                                </div>
                            )}
                        </Field>
                        
                        <Field name="Email">
                            {({ input, meta }) => (
                                <div>
                                    <CLabel inputName={'Email'} title={'Email'} />
                                    <input className='register__form__input' type="text" {...input} name='Email' id='Email' placeholder="john-sillver@gmail.com" />
                                    {meta.touched && meta.error && <div className='register__form__error__message'>{meta.error}</div>}
                                </div>
                            )}
                        </Field>

                        <Field name="Password">
                            {({ input, meta }) => (
                                <div>
                                    <CLabel inputName={'Password'} title={'Password'} />
                                    <input className='register__form__input' type="password" {...input} name='Password' id='Password' placeholder="* * * * * * *" />
                                    {meta.touched && meta.error && <div className='register__form__error__message'>{meta.error}</div>}
                                </div>
                            )}
                        </Field>
                        
                        <Field name="RePassword">
                            {({ input, meta }) => (
                                <div>
                                    <CLabel inputName={'RePassword'} title={'Retype Password'} />
                                    <input className='register__form__input' type="password" {...input} name='RePassword' id='RePassword' placeholder="* * * * * * *" />
                                    {meta.touched && meta.error && <div className='register__form__error__message'>{meta.error}</div>}
                                </div>
                            )}
                        </Field>

                        <CSubmitButton textContent='Register' />
                        <p className='account__message' onClick={showLogin}>If you have an account click here!</p>
                    </form>
                )} />
        </section>
    );
};
