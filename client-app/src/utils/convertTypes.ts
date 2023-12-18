import { IUser } from "../interfaces/Interfaces"
import { GenderEnum, genderEnum } from "./constants";

export const returnCorrecTypesForEditUser = (values: IUser) => {
  const {Age, Gender, ...otherValues} = values;

  const userData: IUser = {
    ...otherValues
  }

  if (Age) {
    const age = Number(Age);
    userData.Age = age;
  }

  if (Gender) {
    const gender = genderEnum[Gender as unknown as keyof GenderEnum];
    userData.Gender = gender;
  }

  return userData;
}