import React, { useState } from 'react';

import { IAnimal } from '../../interfaces/Interfaces';
import { addOrEditPetFormValidator } from '../../validators/addOrEditPetFormValidator';

import AddOrEditPet from '../../components/AddOrEditPet/AddOrEditPet';

interface EditPetPageProps { }

const EditPetPage: React.FC<EditPetPageProps> = () => {
    const [errors, setErrors] = useState<IAnimal | null>(null);

    const addOrEditPet = 'edit';

    const data = {
        AnimalId: '123',
        Name: 'Rumen',
        AnimalCategory: 'dog',
        Breed: 'setter',
        Description: 'Lorem ipsum dolor, sit amet consectetur adipisicing elit. Sit, quis culpa molestiae modi et repellendus eum facere dolorem soluta quibusdam temporibus, vitae totam eligendi nesciunt atque! Ea commodi quae expedita!',
        Age: '2',
        BirthDate: '2023-11-16',
        IsEducated: 'Yes',
        Photo: '???',
        // Photo: {
        //     lastModified: 1699371298685,
        //     name: 'dog-404.png',
        //     size: 77796,
        //     type: 'image/png',
        //     webkitRelativePath: ''
        // },
        HealthStatus: 'Vaccinated',
        Gender: 'Male',
        SocialMedia: 'http://localhost:3000/add-pet',
        Weight: '20',
        IsHavingValidDocuments: 'Yes'
};

const onEditPetSubmit = (values: IAnimal) => {
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