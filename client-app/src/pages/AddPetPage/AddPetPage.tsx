import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

import { IAnimal } from '../../interfaces/Interfaces';
import { returnCorrectTypesForAddOrEditPetForm } from '../../utils/convertTypes';
import agent from '../../api/axiosAgent';

import AddOrEditPet from '../../components/AddOrEditPet/AddOrEditPet';
import { CLoading } from '../../components/common/CLoading/CLoading';

interface AddPetPageProps { }

const AddPetPage: React.FC<AddPetPageProps> = () => {
    const navigate = useNavigate();
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const addOrEditPet = 'add';

    const onAddPetSubmit = async (values: IAnimal) => {
        const petData = returnCorrectTypesForAddOrEditPetForm(values);

        const formData = new FormData();

        Object.entries(petData).forEach(([key, value]) => {
            if (key === 'Photos' && Array.isArray(value)) {
                value.forEach((photo, index) => {
                    formData.append(`Photos[${index}].File`, (photo.File as Blob));
                    formData.append(`Photos[${index}].IsMain`, String(photo.IsMain));
                });
            } else {
                formData.append(key, (value as string));
            }
        });

        try {
            setIsLoading(true);
            const res = await agent.apiAnimal.addAnimal(formData);

            navigate('/dashboard');
            toast.success(res.successMessage);
        } catch (err) {
            console.error(err);
        }
    };

    return (
        <>
            <AddOrEditPet addOrEditPet={addOrEditPet} onAddPetSubmit={onAddPetSubmit} />
            {isLoading && <CLoading />}
        </>
    );
};

export default AddPetPage;