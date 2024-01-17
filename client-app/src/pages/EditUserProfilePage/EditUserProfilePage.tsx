import React, { useState, useEffect } from 'react';
import { observer } from 'mobx-react';
import { Form, Field } from 'react-final-form';
import { CgAsterisk } from 'react-icons/cg';
import { FaTrashAlt } from 'react-icons/fa';
import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';

import themeStore from '../../stores/themeStore';
import { IUser, IUserProfile } from '../../interfaces/Interfaces';
import { editUserProfileFormValidator } from '../../validators/userProfileFormValidators';
import userStore from '../../stores/userStore';
import agent from '../../api/axiosAgent';

import FormsHeader from '../../components/FormsHeader/FormsHeader';
import { CLabel } from '../../components/common/CLabel/CLabel';
import UserPhoto from '../../components/UserPhoto/UserPhoto';
import { CSubmitButton } from '../../components/common/CSubmitButton/CSubmitButton';
import DeleteModal from '../../components/DeleteModal/DeleteModal';
import Footer from '../../components/Footer/Footer';
import { returnCorrecTypesForEditUser } from '../../utils/convertTypes';

interface EditUserProfilePageProps { }

const userData = (user: IUserProfile) => {
    return {
        Address: user.address,
        Age: user.age,
        City: user.city,
        Education: user.education,
        Email: user.email,
        Gender: user.gender,
        JobTitle: user.jobTitle,
        Name: user.name,
        Photo: user.photo,
        Roles: user.roles
    };
};

const EditUserProfilePage: React.FC<EditUserProfilePageProps> = observer(() => {
    const [user, setUser] = useState<IUserProfile | object>({});
    const navigate = useNavigate();

    const [isDeleteClick, setIsDeleteClick] = useState<boolean>(false);

    const title = 'Edit My Profile';
    const subjectForDelete = 'this profile';

    useEffect(() => {
        agent.apiUser.getUserProfile()
            .then(res => {
                const user = userData(res.data);

                setUser(user);
            });
    }, []);

    const onEditUserProfileSubmit = async (values: IUser) => {
        const userData = returnCorrecTypesForEditUser(values);

        try {
            const result = await agent.apiUser.editUser(userData);

            if (result.isSuccess) {
                userStore.setUser({ ...values, PhotoUrl: userStore.user?.PhotoUrl }, userStore.authToken!);

                navigate('/dashboard');

                toast.success(result.successMessage);
            } else {
                toast.error(result.errorMessage);
            }
        } catch (err) {
            console.error(err);
        }
    };

    const onDeleteOrCancelClick = () => {
        setIsDeleteClick(state => !state);
    };

    const onConfirmDelete = async () => {
        try {
            const result = await agent.apiUser.deleteUser();

            if (result.isSuccess) {
                userStore.clearUser();

                navigate('/');

                toast.success(result.successMessage);
            } else {
                toast.error(result.errorMessage);
            }
        } catch (err) {
            console.error(err);
        }
    };

    const onDeleteRoleClick = async (value: string) => {
        await agent.apiUser.deleteRole(value);
    };

    return (
        <div className={themeStore.isLightTheme ? 'forms__container' : 'forms__container forms__container__dark'}>
            <FormsHeader title={title} />

            <section className="forms__container__form-wrapper">
                <p>Fields with "<CgAsterisk className="asterisk" />" are required!</p>

                <Form
                    initialValues={user}
                    onSubmit={onEditUserProfileSubmit}
                    validate={editUserProfileFormValidator}
                    render={({ handleSubmit }) => (

                        <form className={themeStore.isLightTheme ? '' : 'dark'} onSubmit={handleSubmit}>
                            <Field name='Name'>
                                {({ input, meta }) => (
                                    <>
                                        <div className="required">
                                            <CLabel inputName='Name' title='Name' />
                                            <CgAsterisk className="asterisk" />
                                        </div>
                                        <input type="text" {...input} name='Name' id='Name' placeholder='John' />
                                        {meta.touched && meta.error && <span>{meta.error}</span>}
                                    </>
                                )}
                            </Field>

                            <Field name='Email'>
                                {({ input, meta }) => (
                                    <>
                                        <div className="required">
                                            <CLabel inputName='Email' title='Email' />
                                            <CgAsterisk className="asterisk" />
                                        </div>
                                        <input type="text" {...input} name='Email' id='Email' placeholder='john-doe@gmail.com' />
                                        {meta.touched && meta.error && <span>{meta.error}</span>}
                                    </>
                                )}
                            </Field>

                            <Field name='Description'>
                                {({ input, meta }) => (
                                    <>
                                        <CLabel inputName='Description' title='Description' />
                                        <textarea {...input} name="Description" id="Description" placeholder='  .....' />
                                        {meta.touched && meta.error && <span>{meta.error}</span>}
                                    </>
                                )}
                            </Field>

                            <div className="pairs">
                                <Field name='Age'>
                                    {({ input, meta }) => (
                                        <div className="wrapper">
                                            <CLabel inputName='Age' title='Age' />
                                            <input type="text" {...input} name='Age' id='Age' placeholder='25' />
                                            {meta.touched && meta.error && <span>{meta.error}</span>}
                                        </div>
                                    )}
                                </Field>

                                <Field name='Gender'>
                                    {({ input, meta }) => (
                                        <div className="wrapper">
                                            <CLabel inputName='Gender' title='Gender' />
                                            <select {...input} name="Gender" id="Gender">
                                                <option>  </option>
                                                <option>Male</option>
                                                <option>Female</option>
                                                <option>Other</option>
                                            </select>
                                            {meta.touched && meta.error && <span>{meta.error}</span>}
                                        </div>
                                    )}
                                </Field>
                            </div>

                            <section className="register__form__roles">
                                <Field type="checkbox" name="Roles" value="Matching">
                                    {({ input }) => (
                                        <div>
                                            <label htmlFor='Roles'>Matching</label>
                                            <input onClick={() => onDeleteRoleClick(input.value)} type="checkbox" {...input} name="Roles" value="Matching" className="checkbox__input" />
                                        </div>
                                    )}
                                </Field>

                                <Field type="checkbox" name="Roles" value="Marketplace">
                                    {({ input, meta }) => (
                                        <div className="register__form__roles__marketplace">
                                            <div>
                                                <label htmlFor='Roles'>Marketplace</label>
                                                <input onClick={() => onDeleteRoleClick(input.value)} type="checkbox" {...input} name="Roles" value="Marketplace" className="checkbox__input" />
                                            </div>
                                            {meta.touched && meta.error && <div className="register__form__error__message">{meta.error}</div>}
                                        </div>
                                    )}
                                </Field>
                            </section>

                            <Field name='Education'>
                                {({ input, meta }) => (
                                    <>
                                        <CLabel inputName='Education' title='Education' />
                                        <input type='text' {...input} name='Education' id='Education' placeholder='Economic' />
                                        {meta.touched && meta.error && <span>{meta.error}</span>}
                                    </>
                                )}
                            </Field>

                            <Field name='JobTitle'>
                                {({ input, meta }) => (
                                    <>
                                        <CLabel inputName='JobTitle' title='Job Title' />
                                        <input type="text" {...input} name='JobTitle' id='JobTitle' placeholder='Sales Manager' />
                                        {meta.touched && meta.error && <span>{meta.error}</span>}
                                    </>
                                )}
                            </Field>

                            <Field name='Address'>
                                {({ input, meta }) => (
                                    <>
                                        <CLabel inputName='Address' title='Address' />
                                        <input type="text" {...input} name='Address' id='Address' placeholder='72 Evesham Rd' />
                                        {meta.touched && meta.error && <span>{meta.error}</span>}
                                    </>
                                )}
                            </Field>

                            <Field name='City'>
                                {({ input, meta }) => (
                                    <>
                                        <CLabel inputName='City' title='City' />
                                        <input type="text" {...input} name='City' id='City' placeholder='Liverpool' />
                                        {meta.touched && meta.error && <span>{meta.error}</span>}
                                    </>
                                )}
                            </Field>

                            <Field name='Photo'>
                                {({ input, meta }) => (
                                    <>
                                        <UserPhoto input={input} initialValue={userStore.user?.PhotoUrl} />
                                        {meta.touched && meta.error && <span>{meta.error}</span>}
                                    </>
                                )}
                            </Field>

                            <CSubmitButton textContent='Edit profile' />
                            <button type="button" onClick={onDeleteOrCancelClick} className="deleteBtn"><FaTrashAlt /></button>
                        </form>
                    )}
                />
            </section>

            {isDeleteClick && (
                <DeleteModal subjectForDelete={subjectForDelete} onDeleteOrCancelClick={onDeleteOrCancelClick} onConfirmDelete={onConfirmDelete} />
            )}

            <Footer />
        </div>
    );
});

export default EditUserProfilePage;