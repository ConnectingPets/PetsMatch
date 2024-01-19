import React from 'react';
import { useNavigate } from 'react-router-dom';
import { Form, Field } from 'react-final-form';
import { observer } from 'mobx-react';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

import { IUser } from '../../interfaces/Interfaces';
import { registerFormValidator } from '../../validators/userProfileFormValidators';
import agent from '../../api/axiosAgent';

import { CLabel } from '../common/CLabel/CLabel';
import { CSubmitButton } from '../common/CSubmitButton/CSubmitButton';
import './Register.scss';

interface RegisterProps {
    showLogin: () => void;
}

export const Register: React.FC<RegisterProps> = observer(() => {
    const navigate = useNavigate();

    const onSubmit = async (values: IUser) => {

        try {
            const result = await agent.apiUser.register(values);

            if (result.isSuccess) {

                navigate('/dashboard');
            } else {
                toast.error(result.errorMessage);
            }
        } catch (err) {
            console.log(err);

            toast.error('Register failed. Please check your credentials.');
        }
    };

    return (
        <section className="register__form__section">
            <Form
                onSubmit={onSubmit}
                validate={registerFormValidator}
                render={({ handleSubmit }) => (
                    <form className="register__form" onSubmit={handleSubmit}>

                        <Field name="Name">
                            {({ input, meta }) => (
                                <div>
                                    <CLabel inputName={'Name'} title={'Name'} />
                                    <input className="register__form__input" type="text" {...input} name="Name" id="Name" placeholder="John Sillver" />
                                    {meta.touched && meta.error && <div className="register__form__error__message">{meta.error}</div>}
                                </div>
                            )}
                        </Field>

                        <Field name="Email">
                            {({ input, meta }) => (
                                <div>
                                    <CLabel inputName={'Email'} title={'Email'} />
                                    <input className="register__form__input" type="text" {...input} name="Email" id="Email" placeholder="john-sillver@gmail.com" />
                                    {meta.touched && meta.error && <div className="register__form__error__message">{meta.error}</div>}
                                </div>
                            )}
                        </Field>

                        <Field name="Password">
                            {({ input, meta }) => (
                                <div>
                                    <CLabel inputName={'Password'} title={'Password'} />
                                    <input className="register__form__input" type="password" {...input} name="Password" id="Password" placeholder="* * * * * * *" />
                                    {meta.touched && meta.error && <div className="register__form__error__message">{meta.error}</div>}
                                </div>
                            )}
                        </Field>

                        <Field name="ConfirmPassword">
                            {({ input, meta }) => (
                                <div>
                                    <CLabel inputName={'ConfirmPassword'} title={'Confirm Password'} />
                                    <input className="register__form__input" type="password" {...input} name="ConfirmPassword" id="ConfirmPassword" placeholder="* * * * * * *" />
                                    {meta.touched && meta.error && <div className="register__form__error__message">{meta.error}</div>}
                                </div>
                            )}
                        </Field>

                        <section className="register__form__roles">
                            <CLabel inputName={''} title={'Select Role'} />

                            <Field type="checkbox" name="Roles" value="Matching">
                                {({ input }) => (
                                    <div className="register__form__roles__matching">
                                        <label htmlFor='Roles'>Matching</label>
                                        <input type="checkbox" {...input} name="Roles" value="Matching" className="checkbox__input" />
                                    </div>
                                )}
                            </Field>

                            <Field type="checkbox" name="Roles" value="Marketplace">
                                {({ input, meta }) => (
                                    <div className="register__form__roles__marketplace">
                                        <div>
                                            <label htmlFor='Roles'>Marketplace</label>
                                            <input type="checkbox" {...input} name="Roles" value="Marketplace" className="checkbox__input" />
                                        </div>
                                        {meta.touched && meta.error && <div className="register__form__error__message">{meta.error}</div>}
                                    </div>
                                )}
                            </Field>
                        </section>

                        <CSubmitButton textContent='Register' />
                    </form>
                )} />
        </section>
    );
});
