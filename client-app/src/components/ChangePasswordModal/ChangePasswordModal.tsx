import React from 'react';
import { Form, Field } from 'react-final-form';
import { toast } from 'react-toastify';

import themeStore from '../../stores/themeStore';
import { IUserPasswordData } from '../../interfaces/Interfaces';
import { changePasswordFormValidator } from '../../validators/userProfileFormValidators';
import agent from '../../api/axiosAgent';

import { CLabel } from '../common/CLabel/CLabel';
import { CSubmitButton } from '../common/CSubmitButton/CSubmitButton';
import './ChangePasswordModal.scss';

interface ChangePasswordModalProps {
    onClickChangePassword: () => void
}

const ChangePasswordModal: React.FC<ChangePasswordModalProps> = ({ onClickChangePassword }) => {

    const onChangePasswordSubmit = async (values: IUserPasswordData) => {
        try {
            const res = await agent.apiUser.changePassword(values);

            if (res.isSuccess) {
                toast.success(res.successMessage);

                onClickChangePassword();
            } else {
                toast.error(res.errorMessage);
            }
        } catch (err) {
            console.log(err);
        }
    };

    return (
        <div className="change-pass">
            <div className="change-pass__modal">
                <Form
                    onSubmit={onChangePasswordSubmit}
                    validate={changePasswordFormValidator}
                    render={({ handleSubmit }) => (
                        <form className={themeStore.isLightTheme ? 'change-pass__modal__form' : 'change-pass__modal__form change-pass__modal__form__dark'} onSubmit={handleSubmit}>

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