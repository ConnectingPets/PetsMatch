import { IUser } from '../interfaces/Interfaces';
import { IAnimal } from '../interfaces/Interfaces';
import { GenderEnum, HealthStatusEnum, genderEnum, healthStatusEnum } from './constants';

export const returnCorrectTypesForAddOrEditPetForm = (values: IAnimal) => {
    const { Age, BreedId, Gender, HealthStatus, IsEducated, IsHavingValidDocuments, Photos, ...otherValues } = values;

        const ageValue = Number(Age);
        const breedValue = Number(BreedId);
        const genderValue = genderEnum[Gender as unknown as keyof GenderEnum];
        const healthStatusValue = healthStatusEnum[HealthStatus as unknown as keyof HealthStatusEnum];
        const isEducatedValue = IsEducated == 'Yes' ? true : false;
        const isHavingValidDocumentsValue = IsHavingValidDocuments == 'Yes' ? true : false;
        const photoValue = Photos.map((file, index) => ({ File: file, IsMain: index == 0 ? true : false }));

        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        const petData: any = {
            Age: ageValue,
            // eslint-disable-next-line @typescript-eslint/ban-ts-comment
            // @ts-ignore
            BreedId: breedValue,
            Gender: genderValue,
            HealthStatus: healthStatusValue,
            IsEducated: isEducatedValue,
            IsHavingValidDocuments: isHavingValidDocumentsValue,
            Photos: photoValue,
            ...otherValues
        };

        if (otherValues.Weight) {
            const weightValue = Number(otherValues.Weight);
            petData['Weight'] = weightValue;
        }

        if (otherValues.BirthDate) {
            petData['BirthDate'] = otherValues.BirthDate;       
        }

        if (otherValues.IsForSale) {
          petData['IsForSale'] = otherValues.IsForSale == 'For sale' ? true : false;
        }

        if (otherValues.Price) {
          petData['Price'] = Number(otherValues.Price);
        }

        return petData;
};

export const returnCorrecTypesForEditUser = (values: IUser) => {
  const {Age, Gender, ...otherValues} = values;

  const userData: IUser = {
    ...otherValues
  };

  if (Age) {
    const age = Number(Age);
    userData.Age = age;
  }

  if (Gender) {
    const gender = genderEnum[Gender as unknown as keyof GenderEnum];
    userData.Gender = gender;
  }

  if (otherValues.City) {
    userData.City = otherValues.City.trim().charAt(0).toUpperCase() + otherValues.City.trim().slice(1);
  }

  return userData;
};
