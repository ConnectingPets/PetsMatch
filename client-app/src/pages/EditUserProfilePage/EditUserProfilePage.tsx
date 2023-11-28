import React, { useState } from 'react';
import { observer } from 'mobx-react';
import { Form, Field } from 'react-final-form';
import { CgAsterisk } from 'react-icons/cg';
import { FaTrashAlt } from 'react-icons/fa';

import themeStore from '../../stores/themeStore';
import { IUser } from '../../interfaces/Interfaces';
import { editUserProfileFormValidator } from '../../validators/userProfileFormValidators';

import FormsHeader from '../../components/FormsHeader/FormsHeader';
import { CLabel } from '../../components/common/CLabel/CLabel';
import UserPhoto from '../../components/UserPhoto/UserPhoto';
import { CSubmitButton } from '../../components/common/CSubmitButton/CSubmitButton';
import DeleteModal from '../../components/DeleteModal/DeleteModal';
import Footer from '../../components/Footer/Footer';

interface EditUserProfilePageProps { }

const EditUserProfilePage: React.FC<EditUserProfilePageProps> = observer(() => {
    const [errors, setErrors] = useState<IUser | null>(null);
    const [isDeleteClick, setIsDeleteClick] = useState<boolean>(false);

    const title = 'Edit My Profile';
    const subjectForDelete = 'this profile';

    const user: IUser = {
        Id: '123',
        Name: 'John Doe',
        Email: 'john-doe@gmail.com',
        Description: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Quos, ea iure totam quibusdam officiis eius soluta vitae quidem nemo nostrum perspiciatis exercitationem blanditiis voluptatem magnam libero ratione assumenda quod doloremque?',
        Age: 25,
        Education: 'Economic',
        Photo: '??',
        JobTitle: 'Sales Manager',
        Gender: 'Male',
        Address: '72 Evesham Rd',
        City: 'Liverpool'
    };

    //  TO DO show user photo

    const onEditUserProfileSubmit = (values: IUser) => {
        setErrors(null);
        const err = editUserProfileFormValidator(values);

        if (Object.keys(err).length != 0) {
            setErrors(err);
        } else {
            console.log(values);
        }
    };

    const onDeleteOrCancelClick = () => {
        setIsDeleteClick(state => !state);
    };

    const onConfirmDelete = () => {

        // TO DO .....
    };

    return (
        <div className={themeStore.isLightTheme ? 'forms__container' : 'forms__container forms__container__dark'}>
            <FormsHeader title={title} />

            <section className="forms__container__form-wrapper">
                <p>Fields with "<CgAsterisk className="asterisk" />" are required!</p>

                <Form
                    initialValues={user}
                    onSubmit={onEditUserProfileSubmit}
                    render={({ handleSubmit }) => (

                        <form className={themeStore.isLightTheme ? '' : 'dark'} onSubmit={handleSubmit}>
                            <Field name='Name'>
                                {({ input }) => (
                                    <>
                                        <div className="required">
                                            <CLabel inputName='Name' title='Name' />
                                            <CgAsterisk className="asterisk" />
                                        </div>
                                        <input type="text" {...input} name='Name' id='Name' placeholder='John' />
                                        {errors && <span>{errors.Name}</span>}
                                    </>
                                )}
                            </Field>

                            <Field name='Email'>
                                {({ input }) => (
                                    <>
                                        <div className="required">
                                            <CLabel inputName='Email' title='Email' />
                                            <CgAsterisk className="asterisk" />
                                        </div>
                                        <input type="text" {...input} name='Email' id='Email' placeholder='john-doe@gmail.com' />
                                        {errors && <span>{errors.Email}</span>}
                                    </>
                                )}
                            </Field>

                            <Field name='Description'>
                                {({ input }) => (
                                    <>
                                        <CLabel inputName='Description' title='Description' />
                                        <textarea {...input} name="Description" id="Description" placeholder='  .....' />
                                        {errors && <span>{errors.Description}</span>}
                                    </>
                                )}
                            </Field>

                            <div className="pairs">
                                <Field name='Age'>
                                    {({ input }) => (
                                        <div className="wrapper">
                                            <CLabel inputName='Age' title='Age' />
                                            <input type="text" {...input} name='Age' id='Age' placeholder='25' />
                                            {errors && <span>{errors.Age}</span>}
                                        </div>
                                    )}
                                </Field>

                                <Field name='Gender'>
                                    {({ input }) => (
                                        <div className="wrapper">
                                            <CLabel inputName='Gender' title='Gender' />
                                            <select {...input} name="Gender" id="Gender">
                                                <option>  </option>
                                                <option>Male</option>
                                                <option>Female</option>
                                                <option>Other</option>
                                            </select>
                                            {errors && <span>{errors.Gender}</span>}
                                        </div>
                                    )}
                                </Field>
                            </div>

                            <Field name='Education'>
                                {({ input }) => (
                                    <>
                                        <CLabel inputName='Education' title='Education' />
                                        <input type='text' {...input} name='Education' id='Education' placeholder='Economic' />
                                        {errors && <span>{errors.Education}</span>}
                                    </>
                                )}
                            </Field>

                            <Field name='JobTitle'>
                                {({ input }) => (
                                    <>
                                        <CLabel inputName='JobTitle' title='Job Title' />
                                        <input type="text" {...input} name='JobTitle' id='JobTitle' placeholder='Sales Manager' />
                                        {errors && <span>{errors.JobTitle}</span>}
                                    </>
                                )}
                            </Field>

                            <Field name='Address'>
                                {({ input }) => (
                                    <>
                                        <CLabel inputName='Address' title='Address' />
                                        <input type="text" {...input} name='Address' id='Address' placeholder='72 Evesham Rd' />
                                        {errors && <span>{errors.Address}</span>}
                                    </>
                                )}
                            </Field>

                            <Field name='City'>
                                {({ input }) => (
                                    <>
                                        <CLabel inputName='City' title='City' />
                                        <input type="text" {...input} name='City' id='City' placeholder='Liverpool' />
                                        {errors && <span>{errors.City}</span>}
                                    </>
                                )}
                            </Field>

                            <Field name='Photo'>
                                {({ input }) => (
                                    <UserPhoto errors={errors} input={input} />
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