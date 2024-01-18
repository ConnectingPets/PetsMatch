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
    'cat': number,
    'dog': number,
    'bunny': number
};

export type AnimalBreedEnum = {
    'pitbull': number,
    'husky': number,
    'American rabbit': number,
    'Persian Cat': number,
    'Bengal Cat.': number
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
    'cat': 1,
    'dog': 2,
    'bunny': 3
};

export const animalBreedEnum: AnimalBreedEnum = {
    'pitbull': 1,
    'husky': 2,
    'American rabbit': 3,
    'Persian Cat': 4,
    'Bengal Cat.': 5
};