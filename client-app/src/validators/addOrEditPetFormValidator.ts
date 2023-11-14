import { combineValidators, composeValidators, isRequired, isAlphabetic, hasLengthBetween, hasLengthLessThan, isNumeric, createValidator, hasLengthGreaterThan } from 'revalidate';

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

export const addOrEditPetFormValidator = combineValidators({
    name: composeValidators(
        isRequired,
        isAlphabetic,
        hasLengthBetween(2, 50)('This field')
    )('Name'),
    description: hasLengthLessThan(151)('This field'),
    age: composeValidators(
        isRequired,
        isNumeric('This field'),
        ageRange
    )('Age'),
    isEducated: isRequired('This field'),
    photo: isRequired('Photo'),
    healthStatus: isRequired('This field'),
    gender: isRequired('Gender'),
    socialMedia: hasLengthGreaterThan(3)('This field'),
    weight: composeValidators(
        isNumeric,
        weightRange
    )('This field'),
    isHavingValidDocuments: isRequired('This field')
});