import React, { ReactElement } from 'react';

export interface IUser {
    Id?: string,
    Name: string,
    Email: string,
    Password?: string,
    ConfirmPassword?: string,
    Description?: string,
    Age?: number,
    Education?: string
    JobTitle?: string,
    Gender?: number,
    Address?: string,
    City?: string,
    PhotoUrl?: string,
    Roles: string[],
    RememberMe?: boolean
}

export interface IUserProfile {
    address?: string
    age?: number
    city?: string
    description?: string
    education?: string
    email: string
    gender?: string
    jobTitle?: string
    name: string
    photo?: string
    roles?: string[]
}

export interface IUserPasswordData {
    OldPassword: string
    NewPassword: string
    ConfirmPassword: string
}

export interface IUserAnimals {
    id: string,
    name: string,
    mainPhoto: string,
    price?: number | null,
    breed?: string,
    category?: string,
    city?: string,
    gender?: string
}

export interface IAnimal {
    AnimalId?: string,
    Name: string,
    AnimalCategory?: Categories,
    BreedId: Breeds,
    Description?: string,
    Age: number,
    BirthDate?: Date,
    IsEducated: string | boolean,
    Photos: Array<{
        id: string,
        isMain: boolean,
        url: string
    }>,
    HealthStatus: number,
    Gender: number,
    SocialMedia?: string,
    Weight?: number,
    IsHavingValidDocuments: string | boolean,
    isModifiedBreed?: boolean,
    isModifiedGender?: boolean,
    isModifiedName?: boolean,
    IsForSale?: string,
    Price?: string
}

export interface Categories {
    name: string,
    animalCategoryId: number
}

export interface Breeds {
    name: string,
    breedId: number
}

export interface IAnimalWithUserInfo {
    address: string
    age: number
    birthDate: string
    breedName: string
    city: string
    description: string
    gender: number
    healthStatus: number
    isEducated: boolean
    isHavingValidDocuments: boolean
    name: string
    photos: IPhoto[]
    price: number
    socialMedia: string
    userEmail: string
    userName: string
    weight: number
}

export interface IPossibleSwipes {
    map(arg0: (pet: { name: string; animalId: string; photos: IPhoto[]; }, arg1: number) => import('react/jsx-runtime').JSX.Element): React.ReactNode;
    animalId: string,
    age: number,
    birthDate?: Date,
    breed: string,
    description?: string,
    gender: string,
    healthStatus: string,
    isEducated: false,
    isHavingValidDocuments: true,
    name: string,
    photos: IPhoto[],
    socialMedia?: string,
    weight?: number,
    unmatchId?: string
}

export interface IPhoto {
    id: string,
    isMain: boolean,
    url: string
}

export interface ITinderCard {
    swipe: (dir: string) => Promise<void>;
    restoreCard: () => void;
    age: number,
    birthDate?: Date,
    breed: string,
    description?: string,
    gender: string,
    healthStatus: string,
    isEducated: false,
    isHavingValidDocuments: true,
    name: string,
    photo: string,
    socialMedia?: string,
    weight?: number
}

export interface IMessage {
    content: string;
    animalId: string;
    sentOn: string;
}

export interface IAdoptionArticle {
    title: string
    image: string
    content: string
    extendedContent?: ReactElement
}