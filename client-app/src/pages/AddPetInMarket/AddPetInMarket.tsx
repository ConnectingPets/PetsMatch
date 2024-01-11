import React, { useState } from 'react';
// import { useNavigate } from 'react-router-dom';
// import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

import { IAnimal } from '../../interfaces/Interfaces';
import { returnCorrectTypesForAddOrEditPetForm } from '../../utils/convertTypes';
// import agent from '../../api/axiosAgent';


import { CLoading } from '../../components/common/CLoading/CLoading';
import AdoptionMarketplaceForm from '../../components/AdoptionMarketplaceForm/AdoptionMarketplaceForm';

interface AddPetInMarketProps { }

const AddPetInMarket: React.FC<AddPetInMarketProps> = () => {
    // const navigate = useNavigate();
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
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
        
console.log(values);

        // try {
        //     setIsLoading(true);
        //     const res = await agent.apiAnimal.addAnimal(formData);

        //     navigate('/dashboard');
        //     toast.success(res.successMessage);
        // } catch (err) {
        //     console.error(err);
        // }
    };

    return (
        <>
            <AdoptionMarketplaceForm addOrEditPet={addOrEditPet} onAddPetSubmit={onAddPetSubmit} />
            {isLoading && <CLoading />}
        </>
    );
};

export default AddPetInMarket;