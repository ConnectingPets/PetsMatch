import { combineValidators, composeValidators, isRequired, hasLengthBetween, hasLengthLessThan, isNumeric, createValidator, hasLengthGreaterThan, matchesField } from 'revalidate';

const isAlphabeticWithSpaces = createValidator(
    message => value => {
        const pattern = /^[A-Za-z\s]+$/;
        if (!pattern.test(value)) {
            return message;
        }
    },

    field => `${field} must be alphabetic`
);

const isValidEmail = createValidator(
    message => value => {
        const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!emailPattern.test(value)) {
            return message;
        }
    },

    field => `Invalid ${field}`
);

const ageRange = createValidator(
    message => value => {
        if (Number(value) < 16 || Number(value) > 90) {
            return message;
        }
    },

    field => `${field} must be between 16 and 90`
);

export const registerFormValidator = combineValidators({
    Name: composeValidators(
        isRequired,
        isAlphabeticWithSpaces,
        hasLengthBetween(2, 100)
    )('Name'),
    Email: composeValidators(
        isRequired,
        isValidEmail
    )('Email'),
    Password: composeValidators(
        isRequired,
        hasLengthGreaterThan(4)
    )('Password'),
    RePassword: composeValidators(
        isRequired,
        matchesField('Password', 'Password')
    )('Retype Password')
});

export const loginFormValidator = combineValidators({
    Email: composeValidators(
        isRequired,
        isValidEmail
    )('Email'),
    Password: composeValidators(
        isRequired,
        hasLengthGreaterThan(4)
    )('Password')
});

export const editUserProfileFormValidator = combineValidators({
    Name: composeValidators(
        isRequired,
        isAlphabeticWithSpaces,
        hasLengthBetween(2, 100)
    )('Name'),
    Email: composeValidators(
        isRequired,
        isValidEmail
    )('Email'),
    Description: hasLengthLessThan(501)('Description'),
    Age: composeValidators(
        isNumeric,
        ageRange
    )('Age'),
    Education: composeValidators(
        isAlphabeticWithSpaces,
        hasLengthBetween(5, 50)
    )('Education'),
    JobTitle: composeValidators(
        isAlphabeticWithSpaces,
        hasLengthBetween(5, 50)
    )('Job Title'),
    Address: hasLengthBetween(10, 150)('Address'),
    City: composeValidators(
        isAlphabeticWithSpaces,
        hasLengthBetween(3, 50)
    )('City')
});