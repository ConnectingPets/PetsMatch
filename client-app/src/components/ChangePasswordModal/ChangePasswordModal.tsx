import React from 'react';
import { Form, Field } from 'react-final-form';

import { CLabel } from '../common/CLabel/CLabel';
import { CSubmitButton } from '../common/CSubmitButton/CSubmitButton';
import './ChangePasswordModal.scss';

interface ChangePasswordModalProps {
    onClickChangePassword: () => void
}

interface PasswordValues {
    OldPassword: string
    NewPassword: string
    ConfirmPassword: string
}

const ChangePasswordModal: React.FC<ChangePasswordModalProps> = ({ onClickChangePassword }) => {

    const onChangePasswordSubmit = (values: PasswordValues) => {
        console.log(values);
    };

    return (
        <div className="change-pass">
            <div className="change-pass__modal">
                <Form
                    onSubmit={onChangePasswordSubmit}
                    // validate={}
                    render={({ handleSubmit }) => (
                        <form className='change-pass__modal__form' onSubmit={handleSubmit}>

                            <Field name="OldPassword">
                                {({ input, meta }) => (
                                    <>
                                        <CLabel inputName={'OldPassword'} title={'Old Password'} />
                                        <input type="password" {...input} className='change-pass__modal__form__input' name='OldPassword' id='OldPassword' placeholder="* * * * * * *" />
                                        {meta.touched && meta.error && <div className='change-pass__modal__form__error-message'>{meta.error}</div>}
                                    </>
                                )}
                            </Field>

                            <Field name="NewPassword">
                                {({ input, meta }) => (
                                    <>
                                        <CLabel inputName={'NewPassword'} title={'New Password'} />
                                        <input type="password" {...input} className='change-pass__modal__form__input' name='NewPassword' id='NewPassword' placeholder="* * * * * * *" />
                                        {meta.touched && meta.error && <div className='change-pass__modal__form__error-message'>{meta.error}</div>}
                                    </>
                                )}
                            </Field>

                            <Field name="ConfirmPassword">
                                {({ input, meta }) => (
                                    <>
                                        <CLabel inputName={'ConfirmPassword'} title={'Confirm New Password'} />
                                        <input type="password" {...input} className='change-pass__modal__form__input' name='ConfirmPassword' id='ConfirmPassword' placeholder="* * * * * * *" />
                                        {meta.touched && meta.error && <div className='change-pass__modal__form__error-message'>{meta.error}</div>}
                                    </>
                                )}
                            </Field>

                            <CSubmitButton textContent='Change' />
                            <button onClick={() => onClickChangePassword()} type="button">Cancel</button>
                        </form>
                    )} />
            </div>
        </div>
    );
};

export default ChangePasswordModal;