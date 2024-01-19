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