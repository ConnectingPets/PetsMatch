import React, { useState } from 'react';

import { Animal } from '../../interfaces/Interfaces';
import { addOrEditPetFormValidator } from '../../validators/addOrEditPetFormValidator';

import AddOrEditPet from '../../components/AddOrEditPet/AddOrEditPet';

interface EditPetPageProps { }

const EditPetPage: React.FC<EditPetPageProps> = () => {
    const [errors, setErrors] = useState<Animal | null>(null);

    const addOrEditPet = 'edit';

    const data = {
        name: 'Rumen',
        animalCategory: 'dog',
        breed: 'setter',
        description: 'Lorem ipsum dolor, sit amet consectetur adipisicing elit. Sit, quis culpa molestiae modi et repellendus eum facere dolorem soluta quibusdam temporibus, vitae totam eligendi nesciunt atque! Ea commodi quae expedita!',
        age: '2',
        birthDate: '2023-11-16',
        isEducated: 'Yes',
        photo: '???',
        // photo: {
        //     lastModified: 1699371298685,
        //     name: 'dog-404.png',
        //     size: 77796,
        //     type: 'image/png',
        //     webkitRelativePath: ''
        // },
        healthStatus: 'Vaccinated',
        gender: 'Male',
        socialMedia: 'http://localhost:3000/add-pet',
        weight: '20',
        isHavingValidDocuments: 'Yes'
};

const onEditPetSubmit = (values: Animal) => {
    setErrors(null);
    const err = addOrEditPetFormValidator(values);

    if (Object.keys(err).length !== 0) {
        setErrors(err);
    } else {
        console.log(values);
    }
};

return (
    <AddOrEditPet addOrEditPet={addOrEditPet} data={data} onEditPetSubmit={onEditPetSubmit} errors={errors} />
);
};

export default EditPetPage;