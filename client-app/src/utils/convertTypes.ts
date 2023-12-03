import { IAnimal } from '../interfaces/Interfaces';
import { GenderEnum, HealthStatusEnum, genderEnum, healthStatusEnum } from './constants';

export const returnCorrectTypesForAddOrEditPetForm = (values: IAnimal) => {
    const { Age, BreedId, Gender, HealthStatus, IsEducated, IsHavingValidDocuments, Photo, ...otherValues } = values;

        const ageValue = Number(Age);
        const breedValue = Number(BreedId);
        const genderValue = genderEnum[Gender as unknown as keyof GenderEnum];
        const healthStatusValue = healthStatusEnum[HealthStatus as unknown as keyof HealthStatusEnum];
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
            ...otherValues,
        };

        if (otherValues.Weight) {
            const weightValue = Number(otherValues.Weight);
            petData['Weight'] = weightValue;
        }

        if (otherValues.BirthDate) {
            const birthDateValue = new Date(otherValues.BirthDate);
            petData['BirthDate'] = birthDateValue;
        }

        return petData;
};