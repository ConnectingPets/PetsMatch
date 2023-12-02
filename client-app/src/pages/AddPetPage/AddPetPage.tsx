import React from 'react';
import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

import { IAnimal } from '../../interfaces/Interfaces';
import agent from '../../api/axiosAgent';
import { genderEnum, healthStatusEnum } from '../../utils/constants';

import AddOrEditPet from '../../components/AddOrEditPet/AddOrEditPet';

interface AddPetPageProps { }

const AddPetPage: React.FC<AddPetPageProps> = () => {
    const navigate = useNavigate();
    const addOrEditPet = 'add';

    const onAddPetSubmit = async (values: IAnimal) => {
        const { Age, BreedId, Gender, HealthStatus, IsEducated, IsHavingValidDocuments, Photo, ...otherValues } = values;

        const ageValue = Number(Age);
        const breedValue = Number(BreedId);
        // eslint-disable-next-line @typescript-eslint/ban-ts-comment
        // @ts-ignore
        const genderValue = genderEnum[Gender];
        // eslint-disable-next-line @typescript-eslint/ban-ts-comment
        // @ts-ignore
        const healthStatusValue = healthStatusEnum[HealthStatus];
        const isEducatedValue = IsEducated == 'Yes' ? true : false;
        const isHavingValidDocumentsValue = IsHavingValidDocuments == 'Yes' ? true : false;
        // eslint-disable-next-line @typescript-eslint/no-unused-vars
        const photoValue = Photo;

        const petData: IAnimal = {
            Age: ageValue,
            // eslint-disable-next-line @typescript-eslint/ban-ts-comment
            // @ts-ignore
            BreedId: breedValue,
            Gender: genderValue,
            HealthStatus: healthStatusValue,
            IsEducated: isEducatedValue,
            IsHavingValidDocuments: isHavingValidDocumentsValue,
            Name: otherValues.Name,
        };

        if (otherValues.Weight) {
            const weightValue = Number(otherValues.Weight);
            petData['Weight'] = weightValue;
        }

        if (otherValues.BirthDate) {
            const birthDateValue = new Date(otherValues.BirthDate);
            petData['BirthDate'] = birthDateValue;
        }

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