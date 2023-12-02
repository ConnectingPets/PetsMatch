import { combineValidators, composeValidators, isRequired, hasLengthBetween, hasLengthLessThan, isNumeric, createValidator, hasLengthGreaterThan, isRequiredIf } from 'revalidate';

const isAlphabeticWithSpaces = createValidator(
    message => value => {
        const pattern = /^[A-Za-z\s]+$/;
        if (!pattern.test(value)) {
            return message;
        }
    },

    field => `${field} must be alphabetic`
);

const ageRange = createValidator(
    message => value => {
        if (Number(value) < 1 || Number(value) > 20) {
            return message;
        }
    },

    field => `${field} must be between 1 and 20`
);

const weightRange = createValidator(
    message => value => {
        if (Number(value) < 1 || Number(value) > 100) {
            return message;
        }
    },

    field => `${field} must be between 1 and 100`
);

const photoRequired = createValidator(
    message => images => {
        if (!images || images.length === 0) {
            return message;
        }
    },

    field => `${field} is required`
);

export const addOrEditPetFormValidator = combineValidators({
    Name: composeValidators(
        isRequired,
        isAlphabeticWithSpaces,
        hasLengthBetween(2, 50)('This field')
    )('Name'),
    AnimalCategory: isRequiredIf(
        // eslint-disable-next-line @typescript-eslint/ban-ts-comment
        // @ts-ignore
        values => !values.BreedId
    )('Category'),
    BreedId: composeValidators(
        isRequired,
    )('Breed'),
    Description: hasLengthLessThan(501)('This field'),
    Age: composeValidators(
        isRequired,
        isNumeric('This field'),
        ageRange
    )('Age'),
    IsEducated: isRequired('This field'),
    Photo: composeValidators(
        isRequired,
        photoRequired
    )('Photo'),
    HealthStatus: isRequired('This field'),
    Gender: isRequired('Gender'),
    SocialMedia: hasLengthGreaterThan(3)('This field'),
    Weight: composeValidators(
        isNumeric,
        weightRange
    )('This field'),
    IsHavingValidDocuments: isRequired('This field')
});