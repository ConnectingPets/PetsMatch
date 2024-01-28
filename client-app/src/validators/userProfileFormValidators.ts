import { combineValidators, composeValidators, isRequired, hasLengthBetween, hasLengthLessThan, isNumeric, createValidator, hasLengthGreaterThan, matchesField } from 'revalidate';

const isAlphabeticWithSpaces = createValidator(
    message => value => {
        const pattern = /^[A-Za-z\s]+$/;
        if (value !== '' && !pattern.test(value)) {
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
        if (value !== null && value != 0 && (Number(value) < 16 || Number(value) > 90)) {
            return message;
        }
    },

    field => `${field} must be between 16 and 90`
);

const isHaveRole = createValidator(
    message => value => {
        if (value == undefined || value.length == 0) {
            return message;
        }
    },

    field => `At least one ${field} must be selected`
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
    Roles: isHaveRole('Role'),
    Password: composeValidators(
        isRequired,
        hasLengthGreaterThan(4)
    )('Password'),
    ConfirmPassword: composeValidators(
        isRequired,
        matchesField('Password', 'Password')
    )('Confirm Password')
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
    Roles: isHaveRole('Role'),
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

export const changePasswordFormValidator = combineValidators({
    OldPassword: composeValidators(
        isRequired,
        hasLengthGreaterThan(4)
    )('Old password'),
    NewPassword: composeValidators(
        isRequired,
        hasLengthGreaterThan(4)
    )('New password'),
    ConfirmPassword: composeValidators(
        isRequired,
        matchesField('NewPassword', 'NewPassword')
    )('Confirm password'),
});