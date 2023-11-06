import React from 'react';
import { Form, Field } from 'react-final-form';
import { CLabel } from '../common/CLabel/CLabel';
// import { CInput } from '../common/CInput/CInput';
import { CSubmitButton } from '../common/CSubmitButton/CSubmitButton';
import './Register.scss';

interface RegisterProps {
    showLogin: () => void;
}

interface Errors {
    fullName: string | undefined,
    email: string | undefined,
    password: string | undefined,
    repassword: string | undefined,
}

export const Register: React.FC<RegisterProps> = ({
    showLogin
}) => {

    const onSubmit = () => {
        // TODO
    }

    const validate = (e: any) => {
        const errors: Errors = {
            fullName: undefined,
            email: undefined,
            password: undefined,
            repassword: undefined,
        };

        if (e.fullName && e.fullName.length < 3) {
            errors.fullName = 'to short'
        }
        if (e.email && e.email.length < 3) {
            errors.email = 'to short'
        }
        if (e.password && e.password.length < 3) {
            errors.password = 'to short'
        }
        if (e.repassword && e.repassword !== e.password) {
            errors.repassword = 'not valid'
        }

        return errors;
    }

    return (
        <section className='register__form__section'>
            <Form
                onSubmit={onSubmit}
                validate={validate}
                render={({ handleSubmit }) => (
                    <form className='register__form' onSubmit={handleSubmit}>

                        <Field name="fullName">
                            {({ input, meta }) => (
                                <div>
                                    <CLabel inputName={'fullName'} title={'Full Name'} />
                                    <input className='register__form__input' type="text" {...input} name='fullName' id='fullName' placeholder="John Sillver" />
                                    {meta.touched && meta.error && <div className='register__form__error__message'>{meta.error}</div>}
                                </div>
                            )}
                        </Field>
                        {/* <div>
                          <CLabel inputName={'fullName'} title={'Full Name'} />
                           <CInput type='text' id='fullName' name='fullName' placeholder='John Sillver' />
                         </div> */}
                        <Field name="email">
                            {({ input, meta }) => (
                                <div>
                                    <CLabel inputName={'email'} title={'Email'} />
                                    <input className='register__form__input' type="email" {...input} name='email' id='email' placeholder="john-sillver@gmail.com" />
                                    {meta.touched && meta.error && <div className='register__form__error__message'>{meta.error}</div>}
                                </div>
                            )}
                        </Field>
                        {/* <div>
                            <CLabel inputName={'email'} title={'Email'} />
                            <CInput type='email' id='email' name='email' placeholder='john-sillver@gmail.com' />
                        </div> */}
                        <Field name="password">
                            {({ input, meta }) => (
                                <div>
                                    <CLabel inputName={'password'} title={'Password'} />
                                    <input className='register__form__input' type="password" {...input} name='password' id='password' placeholder="* * * * * * *" />
                                    {meta.touched && meta.error && <div className='register__form__error__message'>{meta.error}</div>}
                                </div>
                            )}
                        </Field>
                        
                        <Field name="repassword">
                            {({ input, meta }) => (
                                <div>
                                    <CLabel inputName={'repassword'} title={'Retype Password'} />
                                    <input className='register__form__input' type="password" {...input} name='repassword' id='repassword' placeholder="* * * * * * *" />
                                    {meta.touched && meta.error && <div className='register__form__error__message'>{meta.error}</div>}
                                </div>
                            )}
                        </Field>
                        {/* <div>
                            <CLabel inputName={'repassword'} title={'Retype Password'} />
                            <CInput type='password' id='repassword' name='repassword' placeholder='* * * * * * *' />
                        </div> */}

                        <CSubmitButton textContent='Register' />
                        <p className='account__message' onClick={showLogin}>If you have an account click here!</p>
                    </form>
                )} />
        </section>
    )
}
