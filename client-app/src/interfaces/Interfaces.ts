export interface IUser {
    id?: string,
    name: string,
    email?: string,
    age:number,
    education:string,
    photo:string,
    jobTitle:string,
    gender:string,
    address:string,
    city:string
}

export interface Animal {
    name: string | undefined,
    animalCategory: string | undefined,
    breed: string | undefined,
    description?: string | undefined,
    age: string | undefined,
    birthDate?: string | undefined,
    isEducated: string | undefined,
    photo: string | undefined,
    healthStatus: string | undefined,
    gender: string | undefined,
    socialMedia?: string | undefined,
    weight?: string | undefined,
    isHavingValidDocuments: string | undefined
}
