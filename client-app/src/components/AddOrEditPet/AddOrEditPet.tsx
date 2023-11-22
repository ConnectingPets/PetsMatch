import React, { useState } from 'react';
import { observer } from 'mobx-react';
import { Form, Field } from 'react-final-form';
import { CgAsterisk } from 'react-icons/cg';
import { FaTrashAlt } from 'react-icons/fa';

import themeStore from '../../stores/themeStore';
import { Animal } from '../../interfaces/Interfaces';

import { CLogo } from '../../components/common/CLogo/CLogo';
import { CChangeThemeButton } from '../../components/common/CChangeThemeButton/CChangeThemeButton';
import { CLabel } from '../../components/common/CLabel/CLabel';
import { CSubmitButton } from '../../components/common/CSubmitButton/CSubmitButton';
import PetImages from '../PetImages/PetImages';
import DeleteModal from '../DeleteModal/DeleteModal';
import Footer from '../../components/Footer/Footer';

import './AddOrEditPet.scss';

interface AddOrEditPetProps {
    addOrEditPet: string,
    onAddPetSubmit?: (values: Animal) => void,
    data?: Animal,
    onEditPetSubmit?: (values: Animal) => void,
    errors: Animal | null
}

const AddOrEditPet: React.FC<AddOrEditPetProps> = observer(({ addOrEditPet, onAddPetSubmit, data, onEditPetSubmit, errors }) => {
    const [isDeleteClick, setIsDeleteClick] = useState<boolean>(false);

    // TO DO show images on edit-view

    const subjectForDelete = 'Tutsy';

    const onDeleteOrCancelClick = () => {
        setIsDeleteClick(state => !state);
    };

    const onConfirmDelete = () => {

       // TO DO .....
    };

    return (
        <div className={themeStore.isLightTheme ? 'add-edit-pet__container' : 'add-edit-pet__container add-edit-pet__container__dark'}>
            <header>
                <CLogo />
                <h1 className={themeStore.isLightTheme ? '' : 'dark'}>{addOrEditPet == 'add' ? 'Add Pet' : 'Edit Info'}</h1>
                <CChangeThemeButton />
            </header>

            <section className="add-edit-pet__container__form-wrapper">
                <p>Fields with "<CgAsterisk className="asterisk" />" are required!</p>

                <Form
                    initialValues={data}
                    onSubmit={(values: Animal) => {
                        if (addOrEditPet === 'add' && onAddPetSubmit) {
                            onAddPetSubmit(values);
                        } else if (addOrEditPet === 'edit' && onEditPetSubmit) {
                            onEditPetSubmit(values);
                        }
                    }}
                    render={({ handleSubmit }) => (
                        <form className={themeStore.isLightTheme ? '' : 'dark'} onSubmit={handleSubmit}>
                            <Field name='name'>
                                {({ input }) => (
                                    <>
                                        <div className="required">
                                            <CLabel inputName='name' title='Name' />
                                            <CgAsterisk className="asterisk" />
                                        </div>
                                        <input type="text" {...input} name='name' id='name' placeholder='Rico' />
                                        {errors && <span>{errors.name}</span>}
                                    </>
                                )}
                            </Field>

                            <Field name='animalCategory'>
                                {({ input }) => (
                                    <>
                                        <div className="required">
                                            <CLabel inputName='animalCategory' title='Category' />
                                            <CgAsterisk className="asterisk" />
                                        </div>
                                        <input type="text" {...input} name='animalCategory' id='animalCategory' placeholder='dog' />
                                        {errors && <span>{errors.animalCategory}</span>}
                                    </>
                                )}
                            </Field>

                            <Field name='breed'>
                                {({ input }) => (
                                    <>
                                        <div className="required">
                                            <CLabel inputName='breed' title='Breed' />
                                            <CgAsterisk className="asterisk" />
                                        </div>
                                        <input type="text" {...input} name='breed' id='breed' placeholder='Golden Retriever' />
                                        {errors && <span>{errors.breed}</span>}
                                    </>
                                )}
                            </Field>

                            <Field name='description'>
                                {({ input }) => (
                                    <>
                                        <CLabel inputName='description' title='Description' />
                                        <textarea {...input} name="description" id="description" placeholder='  .....' />
                                        {errors && <span>{errors.description}</span>}
                                    </>
                                )}
                            </Field>

                            <div className="pairs">
                                <Field name='age'>
                                    {({ input }) => (
                                        <div className="wrapper">
                                            <div className="required">
                                                <CLabel inputName='age' title='Age' />
                                                <CgAsterisk className="asterisk" />
                                                <input type="text" {...input} name='age' id='age' placeholder='5' />
                                            </div>
                                            {errors && <span>{errors.age}</span>}
                                        </div>
                                    )}
                                </Field>

                                <Field name='gender'>
                                    {({ input }) => (
                                        <div className="wrapper">
                                            <div className="required">
                                                <CLabel inputName='gender' title='Gender' />
                                                <CgAsterisk className="asterisk" />
                                                <select {...input} name="gender" id="gender">
                                                    <option>  </option>
                                                    <option>Male</option>
                                                    <option>Female</option>
                                                </select>
                                            </div>
                                            {errors && <span>{errors.gender}</span>}
                                        </div>
                                    )}
                                </Field>
                            </div>

                            <div className="pairs">
                                <Field name='isEducated'>
                                    {({ input }) => (
                                        <div className="wrapper">
                                            <div className="required">
                                                <CLabel inputName='isEducated' title='Educated' />
                                                <CgAsterisk className="asterisk" />
                                                <select {...input} name="isEducated" id="isEducated">
                                                    <option>  </option>
                                                    <option>No</option>
                                                    <option>Yes</option>
                                                </select>
                                            </div>
                                            {errors && <span>{errors.isEducated}</span>}
                                        </div>
                                    )}
                                </Field>

                                <Field name='weight'>
                                    {({ input }) => (
                                        <div className="wrapper">
                                            <div className="content">
                                                <CLabel inputName='weight' title='Weight in kg' />
                                                <input type="text" {...input} name='weight' id='weight' placeholder='15' />
                                            </div>
                                            {errors && <span>{errors.weight}</span>}
                                        </div>
                                    )}
                                </Field>
                            </div>

                            <div className="pairs">
                                <Field name='healthStatus'>
                                    {({ input }) => (
                                        <div className="wrapper">
                                            <div className="required">
                                                <CLabel inputName='healthStatus' title='Health Status' />
                                                <CgAsterisk className="asterisk" />
                                                <select {...input} name="healthStatus" id="healthStatus">
                                                    <option>  </option>
                                                    <option>Not vaccinated</option>
                                                    <option>Vaccinated</option>
                                                </select>
                                            </div>
                                            {errors && <span>{errors.healthStatus}</span>}
                                        </div>
                                    )}
                                </Field>

                                <Field name='isHavingValidDocuments'>
                                    {({ input }) => (
                                        <div className="wrapper last-wrapper">
                                            <div className="required">
                                                <CLabel inputName='isHavingValidDocuments' title='Valid Documents' />
                                                <CgAsterisk className="asterisk" />
                                                <select {...input} name="isHavingValidDocuments" id="isHavingValidDocuments">
                                                    <option>  </option>
                                                    <option>No</option>
                                                    <option>Yes</option>
                                                </select>
                                            </div>
                                            {errors && <span>{errors.isHavingValidDocuments}</span>}
                                        </div>
                                    )}
                                </Field>
                            </div>

                            <Field name='birthDate'>
                                {({ input }) => (
                                    <>
                                        <CLabel inputName='birthDate' title='Birth Date' />
                                        <input type="date" {...input} name='birthDate' id='birthDate' />
                                    </>
                                )}
                            </Field>

                            <Field name='socialMedia'>
                                {({ input }) => (
                                    <>
                                        <CLabel inputName='socialMedia' title='Social Media' />
                                        <input type="url" {...input} name='socialMedia' id='socialMedia' placeholder='https://.......' />
                                        {errors && <span>{errors.socialMedia}</span>}
                                    </>
                                )}
                            </Field>

                            <Field name='photo'>
                                {({ input }) => (
                                    <PetImages errors={errors} input={input} />
                                )}
                            </Field>

                            <CSubmitButton textContent={addOrEditPet == 'add' ? 'Add Pet' : 'Edit'} />
                            {addOrEditPet == 'edit' && (
                                <button type="button" onClick={onDeleteOrCancelClick} className="deleteBtn"><FaTrashAlt /></button>
                            )}
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

export default AddOrEditPet;