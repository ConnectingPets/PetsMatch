export interface IUser {
    Id: string,
    Name: string,
    Email: string,
    Password: string,
    RePassword: string,
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
    AnimalCategory: string | undefined,
    Breed: string | undefined,
    Description?: string | undefined,
    Age: string | undefined,
    BirthDate?: string | undefined,
    IsEducated: string | undefined,
    Photo: string | undefined,
    HealthStatus: string | undefined,
    Gender: string | undefined,
    SocialMedia?: string | undefined,
    Weight?: string | undefined,
    IsHavingValidDocuments: string | undefined
}
