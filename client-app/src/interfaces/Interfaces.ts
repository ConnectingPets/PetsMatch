import React from 'react';

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
    PhotoUrl?: string
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
    isModifiedName?: boolean
}

export interface Categories {
    name: string,
    animalCategoryId: number
}

export interface Breeds {
    name: string,
    breedId: number
}

export interface IPossibleSwipes {
    map(arg0: (pet: { name: string; photo: string; }, arg1: number) => import('react/jsx-runtime').JSX.Element): React.ReactNode;
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
    photo: string,
    socialMedia?: string,
    weight?: number
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
