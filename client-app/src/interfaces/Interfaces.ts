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
    AnimalId: string | undefined,
    Name: string | undefined,
    AnimalCategory?: Categories,
    Breed: Breeds,
    Description?: string | undefined,
    Age: string | undefined,
    BirthDate?: string | undefined,
    IsEducated: string | undefined,
    Photo: [] | undefined,
    HealthStatus: string | undefined,
    Gender: string | undefined,
    SocialMedia?: string | undefined,
    Weight?: string | undefined,
    IsHavingValidDocuments: string | undefined
}

export interface Categories {
    name: string,
    animalCategoryId: number
}

export interface Breeds {
    name: string,
    breedId: number
}
