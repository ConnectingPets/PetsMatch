import React, { useState } from 'react';

import { Animal } from '../../interfaces/Interfaces';
import { addOrEditPetFormValidator } from '../../validators/addOrEditPetFormValidator';

import AddOrEditPet from '../../components/AddOrEditPet/AddOrEditPet';

interface AddPetPageProps { }

const AddPetPage: React.FC<AddPetPageProps> = () => {
    const [errors, setErrors] = useState<Animal | null>(null);

    const addOrEditPet = 'add';

    const onAddPetSubmit = (values: Animal) => {
        setErrors(null);
        const err = addOrEditPetFormValidator(values);

        if (Object.keys(err).length !== 0) {
            setErrors(err);
        } else {
            console.log(values);
        }
    };

    return (
        <AddOrEditPet addOrEditPet={addOrEditPet} onAddPetSubmit={onAddPetSubmit} errors={errors}/>
    );
};

export default AddPetPage;