import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { observer } from 'mobx-react';
import { Form, Field } from 'react-final-form';
import { CgAsterisk } from 'react-icons/cg';
import { TbArrowBack } from 'react-icons/tb';
import { FaTrashAlt } from 'react-icons/fa';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

import themeStore from '../../stores/themeStore';
import { Breeds, Categories, IAnimal } from '../../interfaces/Interfaces';
import { addOrEditPetFormValidator } from '../../validators/addOrEditPetFormValidator';
import agent from '../../api/axiosAgent';

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
    petData?: IAnimal | null,
    onEditPetSubmit?: (values: IAnimal) => void,
    petId?: string
}

const AddOrEditPet: React.FC<AddOrEditPetProps> = observer(({ addOrEditPet, onAddPetSubmit, petData, onEditPetSubmit, petId }) => {
    const navigate = useNavigate();
    const [categories, setCategories] = useState<Categories[]>([]);
    const [selectedCategory, setSelectedCategory] = useState<string | null>(null);
    const [isCategoryDisabled, setIsCategoryDisabled] = useState<boolean>(false);
    const [breeds, setBreeds] = useState<Breeds[]>([]);
    const [isDeleteClick, setIsDeleteClick] = useState<boolean>(false);

    useEffect(() => {
        agent.apiAnimal.getAllCategories()
            .then(res => {
                setCategories(res.data);
            });
    }, []);

    useEffect(() => {
        if (selectedCategory) {
            agent.apiAnimal.getAllBreeds(Number(selectedCategory))
                .then(res => {
                    setBreeds(res.data);
                })
                .catch(error => {
                    console.error(error);
                });
        }
    }, [selectedCategory]);

    const loadBreeds = (categoryId: string) => {
        setSelectedCategory(String(categoryId));
        setIsCategoryDisabled(true);
    };

    const onBackToCategory = () => {
        setIsCategoryDisabled(false);
    };

    const onDeleteOrCancelClick = () => {
        setIsDeleteClick(state => !state);
    };

    const onConfirmDelete = async () => {
        try {
            if (petId) {
                const res = await agent.apiAnimal.deleteAnimal(petId);

                navigate('/dashboard');

                if (res.isSuccess) {
                    toast.success(res.successMessage);
                } else {
                    toast.error(res.errorMessage);
                }
            }
        } catch (err) {
            console.error(err);
        }
    };

    return (
        <div className={themeStore.isLightTheme ? 'forms__container' : 'forms__container forms__container__dark'}>
            <FormsHeader title={addOrEditPet == 'add' ? 'Add Pet' : 'Edit Pet Info'} />

            <section className="forms__container__form-wrapper">
                <p>Fields with "<CgAsterisk className="asterisk" />" are required!</p>

                <Form
                    initialValues={petData}
                    validate={addOrEditPetFormValidator}
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
                                {({ input, meta }) => (
                                    <>
                                        <div className="required">
                                            <CLabel inputName='Name' title='Name' />
                                            <CgAsterisk className="asterisk" />
                                        </div>
                                        <input type="text" {...input} name='Name' id='Name' placeholder='Rico' />
                                        {meta.touched && meta.error && <span>{meta.error}</span>}
                                    </>
                                )}
                            </Field>

                            <div className="pairs">
                                <Field name='AnimalCategory'>
                                    {({ input, meta }) => (
                                        <div className={isCategoryDisabled ? 'wrapper disabled' : 'wrapper'}>
                                            <div className="required">
                                                <CLabel inputName='AnimalCategory' title='Category' />
                                                <CgAsterisk className="asterisk" />
                                                <select {...input} onChange={(e) => loadBreeds(e.target.value)} name="AnimalCategory" id="AnimalCategory">
                                                    <option>  </option>
                                                    {categories && categories.map(c => <option value={c.animalCategoryId} key={c.animalCategoryId}>{c.name}</option>)}
                                                </select>
                                            </div>
                                            {meta.touched && meta.error && !isCategoryDisabled && <span>{meta.error}</span>}
                                        </div>
                                    )}
                                </Field>

                                <Field name='BreedId'>
                                    {({ input, meta }) => (
                                        <div className={!isCategoryDisabled ? 'wrapper disabled' : 'wrapper'}>
                                            <div className="required">
                                                <CLabel inputName='BreedId' title='Breed' />
                                                <CgAsterisk className="asterisk" />
                                                <select {...input} name="BreedId.breedId" id="Breed">
                                                    {!isCategoryDisabled && <option>  </option>}
                                                    {isCategoryDisabled && breeds.map(b => <option value={b.breedId} key={b.breedId}>{b.name}</option>)}
                                                </select>
                                                <button type="button" onClick={onBackToCategory}><TbArrowBack /> Reset</button>
                                            </div>
                                            {meta.touched && meta.error && <span>{meta.error}</span>}
                                        </div>
                                    )}
                                </Field>
                            </div>

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
                                            <div className="required">
                                                <CLabel inputName='Age' title='Age' />
                                                <CgAsterisk className="asterisk" />
                                                <input type="text" {...input} name='Age' id='Age' placeholder='5' />
                                            </div>
                                            {meta.touched && meta.error && <span>{meta.error}</span>}
                                        </div>
                                    )}
                                </Field>

                                <Field name='Gender'>
                                    {({ input, meta }) => (
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
                                            {meta.touched && meta.error && <span>{meta.error}</span>}
                                        </div>
                                    )}
                                </Field>
                            </div>

                            <div className="pairs">
                                <Field name='IsEducated'>
                                    {({ input, meta }) => (
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
                                            {meta.touched && meta.error && <span>{meta.error}</span>}
                                        </div>
                                    )}
                                </Field>

                                <Field name='Weight'>
                                    {({ input, meta }) => (
                                        <div className="wrapper">
                                            <div className="content">
                                                <CLabel inputName='Weight' title='Weight in kg' />
                                                <input type="text" {...input} name='Weight' id='Weight' placeholder='15' />
                                            </div>
                                            {meta.touched && meta.error && <span>{meta.error}</span>}
                                        </div>
                                    )}
                                </Field>
                            </div>

                            <div className="pairs">
                                <Field name='HealthStatus'>
                                    {({ input, meta }) => (
                                        <div className="wrapper">
                                            <div className="required">
                                                <CLabel inputName='HealthStatus' title='Health Status' />
                                                <CgAsterisk className="asterisk" />
                                                <select {...input} name="HealthStatus" id="HealthStatus">
                                                    <option>  </option>
                                                    <option>Vaccinated</option>
                                                    <option>Dewormed</option>
                                                </select>
                                            </div>
                                            {meta.touched && meta.error && <span>{meta.error}</span>}
                                        </div>
                                    )}
                                </Field>

                                <Field name='IsHavingValidDocuments'>
                                    {({ input, meta }) => (
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
                                            {meta.touched && meta.error && <span>{meta.error}</span>}
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
                                {({ input, meta }) => (
                                    <>
                                        <CLabel inputName='SocialMedia' title='Social Media' />
                                        <input type="url" {...input} name='SocialMedia' id='SocialMedia' placeholder='https://.......' />
                                        {meta.touched && meta.error && <span>{meta.error}</span>}
                                    </>
                                )}
                            </Field>

                            <Field name='Photos'>
                                {({ input, meta }) => (
                                    <>
                                        <PetImages input={input} />
                                        {meta.touched && meta.error && <span>{meta.error}</span>}
                                    </>
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
                <DeleteModal subjectForDelete={petData?.Name} onDeleteOrCancelClick={onDeleteOrCancelClick} onConfirmDelete={onConfirmDelete} />
            )}

            <Footer />
        </div>
    );
});

export default AddOrEditPet;