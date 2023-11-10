import React from 'react';

import { observer } from 'mobx-react';
import { Form, Field } from 'react-final-form';
import themeStore from '../../stores/themeStore';

import { CLogo } from '../../components/common/CLogo/CLogo';
import { CChangeThemeButton } from '../../components/common/CChangeThemeButton/CChangeThemeButton';
import { CLabel } from '../../components/common/CLabel/CLabel';
import './AddPetPage.scss';
import { CSubmitButton } from '../../components/common/CSubmitButton/CSubmitButton';
import Footer from '../../components/Footer/Footer';

interface AddPetPageProps { }

const AddPetPage: React.FC<AddPetPageProps> = observer(() => {

    const onAddPetSubmit = () => {
        // .......
    };

    return (
        <div className={themeStore.isLightTheme ? 'add-pet__container' : 'add-pet__container add-pet__container__dark'}>
            <header>
                <CLogo />
                <h1 className={themeStore.isLightTheme ? '' : 'dark'}>Add Pet</h1>
                <CChangeThemeButton />
            </header>

            <section className="add-pet__container__form">
                <Form
                    onSubmit={onAddPetSubmit}
                    render={({ handleSubmit }) => (
                        <form className={themeStore.isLightTheme ? '' : 'dark'} onSubmit={handleSubmit}>
                            <Field name='name'>
                                {({ input }) => (
                                    <>
                                        <CLabel inputName='name' title='Name' />
                                        <input type="text" {...input} name='name' id='name' placeholder='Rico' />
                                    </>
                                )}
                            </Field>

                            <Field name='description'>
                                {({ input }) => (
                                    <>
                                        <CLabel inputName='description' title='Description' />
                                        <input type="text" {...input} name='description' id='description' placeholder='?' />
                                    </>
                                )}
                            </Field>

                            <Field name='age'>
                                {({ input }) => (
                                    <>
                                        <CLabel inputName='age' title='Age' />
                                        <input type="text" {...input} name='age' id='age' placeholder='5' />
                                    </>
                                )}
                            </Field>

                            <Field name='birthDate'>
                                {({ input }) => (
                                    <>
                                        <CLabel inputName='birthDate' title='Birth Date' />
                                        <input type="text" {...input} name='birthDate' id='birthDate' placeholder='10.11.2018' />
                                    </>
                                )}
                            </Field>

                            <Field name='educated'>
                                {({ input }) => (
                                    <>
                                        <CLabel inputName='educated' title='Educated' />
                                        <input type="text" {...input} name='educated' id='educated' placeholder='?' />
                                    </>
                                )}
                            </Field>

                            <Field name='photo'>
                                {({ input }) => (
                                    <>
                                        <CLabel inputName='photo' title='Photo' />
                                        <input type="text" {...input} name='photo' id='photo' placeholder='?' />
                                    </>
                                )}
                            </Field>

                            <Field name='healthStatus'>
                                {({ input }) => (
                                    <>
                                        <CLabel inputName='healthStatus' title='Health Status' />
                                        <input type="text" {...input} name='healthStatus' id='healthStatus' placeholder='?' />
                                    </>
                                )}
                            </Field>

                            <Field name='gender'>
                                {({ input }) => (
                                    <>
                                        <CLabel inputName='gender' title='Gender' />
                                        <input type="text" {...input} name='gender' id='gender' placeholder='male' />
                                    </>
                                )}
                            </Field>

                            <Field name='socialMedia'>
                                {({ input }) => (
                                    <>
                                        <CLabel inputName='socialMedia' title='Social Media' />
                                        <input type="text" {...input} name='socialMedia' id='socialMedia' placeholder='' />
                                    </>
                                )}
                            </Field>

                            <Field name='weight'>
                                {({ input }) => (
                                    <>
                                        <CLabel inputName='weight' title='Weight' />
                                        <input type="text" {...input} name='weight' id='weight' placeholder='60cm' />
                                    </>
                                )}
                            </Field>

                            <Field name='isHavingValidDocuments'>
                                {({ input }) => (
                                    <>
                                        <CLabel inputName='isHavingValidDocuments' title='Valid Documents' />
                                        <input type="text" {...input} name='isHavingValidDocuments' id='isHavingValidDocuments' placeholder='?' />
                                    </>
                                )}
                            </Field>

                            <Field name='address'>
                                {({ input }) => (
                                    <>
                                        <CLabel inputName='address' title='Address' />
                                        <input type="text" {...input} name='address' id='address' placeholder='29 Elmore Rd' />
                                    </>
                                )}
                            </Field>

                            <Field name='city'>
                                {({ input }) => (
                                    <>
                                        <CLabel inputName='city' title='City' />
                                        <input type="text" {...input} name='city' id='city' placeholder='Bristol' />
                                    </>
                                )}
                            </Field>

                            <CSubmitButton textContent='Add Pet' />
                        </form>
                    )}
                />
            </section>

            <Footer />
        </div>
    );
});

export default AddPetPage;