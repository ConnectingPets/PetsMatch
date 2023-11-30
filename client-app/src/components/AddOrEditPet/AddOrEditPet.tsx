import React, { useState } from 'react';
import { observer } from 'mobx-react';
import { Form, Field } from 'react-final-form';
import { CgAsterisk } from 'react-icons/cg';
import { TbArrowBack } from "react-icons/tb";
import { FaTrashAlt } from 'react-icons/fa';

import themeStore from '../../stores/themeStore';
import { IAnimal } from '../../interfaces/Interfaces';

import FormsHeader from '../FormsHeader/FormsHeader';
import { CLabel } from '../../components/common/CLabel/CLabel';
import { CSubmitButton } from '../../components/common/CSubmitButton/CSubmitButton';
import PetImages from '../PetImages/PetImages';
import DeleteModal from '../DeleteModal/DeleteModal';
import Footer from '../../components/Footer/Footer';

import '../../global-styles/forms.scss';

interface AddOrEditPetProps {
    addOrEditPet: string,
    onAddPetSubmit?: (values: IAnimal) => void,
    data?: IAnimal,
    onEditPetSubmit?: (values: IAnimal) => void,
    errors: IAnimal | null
}

const AddOrEditPet: React.FC<AddOrEditPetProps> = observer(({ addOrEditPet, onAddPetSubmit, data, onEditPetSubmit, errors }) => {
    const [breed, setBreed] = useState<boolean>(false);
    const [isDeleteClick, setIsDeleteClick] = useState<boolean>(false);

    // TO DO show images on edit-view

    const subjectForDelete = 'Tutsy';

    const onBackToCategory = () => {
        setBreed(false);
    };

    const onDeleteOrCancelClick = () => {
        setIsDeleteClick(state => !state);
    };

    const onConfirmDelete = () => {

        // TO DO .....
    };

    return (
        <div className={themeStore.isLightTheme ? 'forms__container' : 'forms__container forms__container__dark'}>
            <FormsHeader title={addOrEditPet == 'add' ? 'Add Pet' : 'Edit Pet Info'} />

            <section className="forms__container__form-wrapper">
                <p>Fields with "<CgAsterisk className="asterisk" />" are required!</p>

                <Form
                    initialValues={data}
                    onSubmit={(values: IAnimal) => {
                        if (addOrEditPet === 'add' && onAddPetSubmit) {
                            onAddPetSubmit(values);
                        } else if (addOrEditPet === 'edit' && onEditPetSubmit) {
                            onEditPetSubmit(values);
                        }
                    }}
                    render={({ handleSubmit }) => (
                        <form className={themeStore.isLightTheme ? '' : 'dark'} onSubmit={handleSubmit}>
                            <Field name='Name'>
                                {({ input }) => (
                                    <>
                                        <div className="required">
                                            <CLabel inputName='Name' title='Name' />
                                            <CgAsterisk className="asterisk" />
                                        </div>
                                        <input type="text" {...input} name='Name' id='Name' placeholder='Rico' />
                                        {errors && <span>{errors.Name}</span>}
                                    </>
                                )}
                            </Field>

                            <div className="pairs">
                                {!breed && (
                                    <Field name='AnimalCategory'>
                                        {({ input }) => (
                                            <div className="wrapper">
                                                <div className="required">
                                                    <CLabel inputName='AnimalCategory' title='Category' />
                                                    <CgAsterisk className="asterisk" />
                                                    <select {...input} name="AnimalCategory" id="AnimalCategory">
                                                        <option>  </option>
                                                        <option>Dog</option>
                                                        <option>Cat</option>
                                                    </select>
                                                </div>
                                                {errors && <span>{errors.AnimalCategory}</span>}
                                            </div>
                                        )}
                                    </Field>
                                )}

                                {breed && (
                                    <Field name='Breed'>
                                        {({ input }) => (
                                            <div className="wrapper">
                                                <div className="required">
                                                    <CLabel inputName='Breed' title='Breed' />
                                                    <CgAsterisk className="asterisk" />
                                                    <select {...input} name="Breed" id="Breed">
                                                        <option>  </option>
                                                        <option>Setter</option>
                                                        <option>Golden Retriever</option>
                                                        <option>Poodle</option>
                                                        <option>Labrador</option>
                                                        <option>Bulldog</option>
                                                        <option>Beagle</option>
                                                        <option>German Shorthaired Pointer</option>
                                                        <option>Welsh Corgi</option>
                                                        <option>Boxer</option>
                                                    </select>
                                                    <button onClick={onBackToCategory}><TbArrowBack /> Back to Category</button>
                                                </div>
                                                {errors && <span>{errors.Breed}</span>}
                                            </div>
                                        )}
                                    </Field>
                                )}
                            </div>

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
                                            <div className="required">
                                                <CLabel inputName='Age' title='Age' />
                                                <CgAsterisk className="asterisk" />
                                                <input type="text" {...input} name='Age' id='Age' placeholder='5' />
                                            </div>
                                            {errors && <span>{errors.Age}</span>}
                                        </div>
                                    )}
                                </Field>

                                <Field name='Gender'>
                                    {({ input }) => (
                                        <div className="wrapper">
                                            <div className="required">
                                                <CLabel inputName='Gender' title='Gender' />
                                                <CgAsterisk className="asterisk" />
                                                <select {...input} name="Gender" id="Gender">
                                                    <option>  </option>
                                                    <option>Male</option>
                                                    <option>Female</option>
                                                </select>
                                            </div>
                                            {errors && <span>{errors.Gender}</span>}
                                        </div>
                                    )}
                                </Field>
                            </div>

                            <div className="pairs">
                                <Field name='IsEducated'>
                                    {({ input }) => (
                                        <div className="wrapper">
                                            <div className="required">
                                                <CLabel inputName='IsEducated' title='Educated' />
                                                <CgAsterisk className="asterisk" />
                                                <select {...input} name="IsEducated" id="IsEducated">
                                                    <option>  </option>
                                                    <option>No</option>
                                                    <option>Yes</option>
                                                </select>
                                            </div>
                                            {errors && <span>{errors.IsEducated}</span>}
                                        </div>
                                    )}
                                </Field>

                                <Field name='Weight'>
                                    {({ input }) => (
                                        <div className="wrapper">
                                            <div className="content">
                                                <CLabel inputName='Weight' title='Weight in kg' />
                                                <input type="text" {...input} name='Weight' id='Weight' placeholder='15' />
                                            </div>
                                            {errors && <span>{errors.Weight}</span>}
                                        </div>
                                    )}
                                </Field>
                            </div>

                            <div className="pairs">
                                <Field name='HealthStatus'>
                                    {({ input }) => (
                                        <div className="wrapper">
                                            <div className="required">
                                                <CLabel inputName='HealthStatus' title='Health Status' />
                                                <CgAsterisk className="asterisk" />
                                                <select {...input} name="HealthStatus" id="HealthStatus">
                                                    <option>  </option>
                                                    <option>Not vaccinated</option>
                                                    <option>Vaccinated</option>
                                                </select>
                                            </div>
                                            {errors && <span>{errors.HealthStatus}</span>}
                                        </div>
                                    )}
                                </Field>

                                <Field name='IsHavingValidDocuments'>
                                    {({ input }) => (
                                        <div className="wrapper last-wrapper">
                                            <div className="required">
                                                <CLabel inputName='IsHavingValidDocuments' title='Valid Documents' />
                                                <CgAsterisk className="asterisk" />
                                                <select {...input} name="IsHavingValidDocuments" id="IsHavingValidDocuments">
                                                    <option>  </option>
                                                    <option>No</option>
                                                    <option>Yes</option>
                                                </select>
                                            </div>
                                            {errors && <span>{errors.IsHavingValidDocuments}</span>}
                                        </div>
                                    )}
                                </Field>
                            </div>

                            <Field name='BirthDate'>
                                {({ input }) => (
                                    <>
                                        <CLabel inputName='BirthDate' title='Birth Date' />
                                        <input type="date" {...input} name='BirthDate' id='BirthDate' />
                                    </>
                                )}
                            </Field>

                            <Field name='SocialMedia'>
                                {({ input }) => (
                                    <>
                                        <CLabel inputName='SocialMedia' title='Social Media' />
                                        <input type="url" {...input} name='SocialMedia' id='SocialMedia' placeholder='https://.......' />
                                        {errors && <span>{errors.SocialMedia}</span>}
                                    </>
                                )}
                            </Field>

                            <Field name='Photo'>
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