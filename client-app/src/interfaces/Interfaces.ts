export interface IUser {
    Id?: string,
    Name?: string,
    Email: string,
    Password?: string,
    RePassword?: string,
    Description?: string,
    Age?: number,
    Education?: string,
    Photo?: string,
    JobTitle?: string,
    Gender?: string,
    Address?: string,
    City?: string
}

export interface IAnimal {
    AnimalId: string,
    Name: string,
    AnimalCategory?: Categories,
    BreedId: Breeds,
    Description?: string,
    Age: number,
    BirthDate?: Date,
    IsEducated: string | boolean,
    Photo: File[],
    HealthStatus: number,
    Gender: number,
    SocialMedia?: string,
    Weight?: number,
    IsHavingValidDocuments: string | boolean
}

export interface Categories {
    name: string,
    animalCategoryId: number
}

export interface Breeds {
    name: string,
    breedId: number
}
