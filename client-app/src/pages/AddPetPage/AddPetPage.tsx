import React from 'react';

import { IAnimal } from '../../interfaces/Interfaces';

import AddOrEditPet from '../../components/AddOrEditPet/AddOrEditPet';

interface AddPetPageProps { }

const AddPetPage: React.FC<AddPetPageProps> = () => {

    const addOrEditPet = 'add';

    const onAddPetSubmit = (values: IAnimal) => {

        console.log(values);
    };

    return (
        <AddOrEditPet addOrEditPet={addOrEditPet} onAddPetSubmit={onAddPetSubmit} />
    );
};

export default AddPetPage;