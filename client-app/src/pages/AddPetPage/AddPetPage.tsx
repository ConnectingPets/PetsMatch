import React from 'react';
import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

import { IAnimal } from '../../interfaces/Interfaces';
import { returnCorrectTypesForAddOrEditPetForm } from '../../utils/convertTypes';
import agent from '../../api/axiosAgent';

import AddOrEditPet from '../../components/AddOrEditPet/AddOrEditPet';

interface AddPetPageProps { }

const AddPetPage: React.FC<AddPetPageProps> = () => {
    const navigate = useNavigate();
    const addOrEditPet = 'add';

    const onAddPetSubmit = async (values: IAnimal) => {
        const petData = returnCorrectTypesForAddOrEditPetForm(values);

        try {
            const res = await agent.apiAnimal.addAnimal(petData);
            
            navigate('/dashboard');
            toast.success(res.successMessage);
        } catch (err) {
            console.error(err);
        }
    };

    return (
        <AddOrEditPet addOrEditPet={addOrEditPet} onAddPetSubmit={onAddPetSubmit} />
    );
};

export default AddPetPage;