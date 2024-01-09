import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

import { IAnimal } from '../../interfaces/Interfaces';
import { returnCorrectTypesForAddOrEditPetForm } from '../../utils/convertTypes';
import { GenderEnum, HealthStatusEnum, genderEnum, healthStatusEnum } from '../../utils/constants';
import { isMoreThan30DaysAgo } from '../../utils/utils';
import agent from '../../api/axiosAgent';

import AddOrEditPet from '../../components/AddOrEditPet/AddOrEditPet';

interface EditPetPageProps { }

interface PetValues {
    age: number,
    birthDate: string,
    breedId: number,
    categoryId: number,
    description: string,
    gender: number,
    healthStatus: number,
    isEducated: boolean,
    isHavingValidDocuments: boolean,
    lastModifiedBreed: string,
    lastModifiedGender: string,
    lastModifiedName: string,
    name: string,
    photos: object[],
    socialMedia: string,
    weight: number
}

const returnCorrectTypes = (data: PetValues) => {
    const genderValue = Object.keys(genderEnum).filter(x => genderEnum[x as keyof GenderEnum] == data.gender)[0];
    const healthStatusValue = Object.keys(healthStatusEnum).filter(x => healthStatusEnum[x as keyof HealthStatusEnum] == data.healthStatus)[0];

    const isEducatedValue = data.isEducated == true ? 'Yes' : 'No';
    const isHavingValidDocumentsValue = data.isHavingValidDocuments == true ? 'Yes' : 'No';

    const isModifiedBreed = isMoreThan30DaysAgo(data.lastModifiedBreed);
    const isModifiedGender = isMoreThan30DaysAgo(data.lastModifiedGender);
    const isModifiedName = isMoreThan30DaysAgo(data.lastModifiedName);

    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    const petData: any = {
        Age: data.age,
        BreedId: data.breedId,
        AnimalCategory: String(data.categoryId),
        Description: data.description ? data.description : '',
        Gender: genderValue,
        HealthStatus: healthStatusValue,
        IsEducated: isEducatedValue,
        IsHavingValidDocuments: isHavingValidDocumentsValue,
        isModifiedBreed,
        isModifiedGender,
        isModifiedName,
        Name: data.name,
        Photos: data.photos,
        SocialMedia: data.socialMedia,
    };

    if (data.birthDate) {
        const inputDate = new Date(data.birthDate);
        const year = inputDate.getFullYear();
        const month = String(inputDate.getMonth() + 1).padStart(2, '0');
        const day = String(inputDate.getDate()).padStart(2, '0');
        const birthDateValue = `${year}-${month}-${day}`;

        petData['BirthDate'] = birthDateValue;
    }

    if (data.weight) {
        petData['Weight'] = data.weight;
    }

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

                if (res.isSuccess) {
                    toast.success(res.successMessage);
                } else {
                    toast.error(res.errorMessage);
                }
            }
        } catch (err) {
            console.error(err);
        }
    };

    return (
        <AddOrEditPet addOrEditPet={addOrEditPet} petData={petData} onEditPetSubmit={onEditPetSubmit} petId={petId} />
    );
};

export default EditPetPage;