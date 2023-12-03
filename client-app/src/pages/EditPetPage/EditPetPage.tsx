import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

import { IAnimal } from '../../interfaces/Interfaces';
import { returnCorrectTypesForAddOrEditPetForm } from '../../utils/convertTypes';
import { GenderEnum, HealthStatusEnum, genderEnum, healthStatusEnum } from '../../utils/constants';
import agent from '../../api/axiosAgent';

import AddOrEditPet from '../../components/AddOrEditPet/AddOrEditPet';

interface EditPetPageProps { }

interface PetValues {
    age: number,
    birthDate: string,
    description: string,
    gender: number,
    healthStatus: number,
    isEducated: boolean,
    isHavingValidDocuments: boolean,
    name: string,
    socialMedia: string,
    weight: number
}

const returnCorrectTypes = (data: PetValues) => {
    const genderValue = Object.keys(genderEnum).filter(x => genderEnum[x as keyof GenderEnum] == data.gender)[0];
    const healthStatusValue = Object.keys(healthStatusEnum).filter(x => healthStatusEnum[x as keyof HealthStatusEnum] == data.healthStatus)[0];

    const inputDate = new Date(data.birthDate);
    const year = inputDate.getFullYear();
    const month = String(inputDate.getMonth() + 1).padStart(2, '0');
    const day = String(inputDate.getDate()).padStart(2, '0');
    const birthDateValue = `${year}-${month}-${day}`;

    const isEducatedValue = data.isEducated == true ? 'Yes' : 'No';
    const isHavingValidDocumentsValue = data.isHavingValidDocuments == true ? 'Yes' : 'No';

    const petData = {
        Age: data.age,
        BirthDate: birthDateValue,
        Description: data.description,
        Gender: genderValue,
        HealthStatus: healthStatusValue,
        IsEducated: isEducatedValue,
        IsHavingValidDocuments: isHavingValidDocumentsValue,
        Name: data.name,
        SocialMedia: data.socialMedia,
        Weight: data.weight
    };

    return petData;
};

const EditPetPage: React.FC<EditPetPageProps> = () => {
    const { petId } = useParams();
    const navigate = useNavigate();
    const [petData, setPetData] = useState<IAnimal | null>(null);
    const addOrEditPet = 'edit';

    useEffect(() => {
        if (petId) {
            agent.apiAnimal.getAnimalById(petId)
                .then(res => {
                    const petValues = returnCorrectTypes(res.data);
                    setPetData(petValues);
                });
        }
    }, [petId]);

    const onEditPetSubmit = async (values: IAnimal) => {
        const newValues = returnCorrectTypesForAddOrEditPetForm(values);

        try {
            if (petId) {
                const res = await agent.apiAnimal.editAnimalById(petId, newValues);

                navigate('/dashboard');
                toast.success(res.successMessage);
            }
        } catch (err) {
            console.error(err);
        }
    };

    return (
        <AddOrEditPet addOrEditPet={addOrEditPet} petData={petData} onEditPetSubmit={onEditPetSubmit} />
    );
};

export default EditPetPage;