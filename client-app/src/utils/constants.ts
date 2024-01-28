export type GenderEnum = {
    'Male': number,
    'Female': number,
    'Other': number,
    'Default': number
}

export type HealthStatusEnum = {
    'Vaccinated': number,
    'Dewormed': number
};

export type AnimalCategoryEnum = {
    'Cat': number,
    'Dog': number,
    'Bunny': number
};

export type AnimalBreedEnum = {
    'Pitbull': number,
    'Husky': number,
    'American rabbit': number,
    'Persian cat': number,
    'Bengal cat': number
};

export type AnimalStatusEnum = {
    'For adoption': number,
    'For sale': number,
    'For swiping': number
}

export const genderEnum: GenderEnum = {
    'Male': 1,
    'Female': 2,
    'Other': 3,
    'Default': 4
};

export const healthStatusEnum: HealthStatusEnum = {
    'Vaccinated': 1,
    'Dewormed': 2
};

export const animalStatusEnum: AnimalStatusEnum = {
    'For adoption': 1,
    'For sale': 2,
    'For swiping': 3
};

export const animalCategoryEnum: AnimalCategoryEnum = {
    'Cat': 1,
    'Dog': 2,
    'Bunny': 3
};

export const animalBreedEnum: AnimalBreedEnum = {
    'Pitbull': 1,
    'Husky': 2,
    'American rabbit': 3,
    'Persian cat': 4,
    'Bengal cat': 5
};