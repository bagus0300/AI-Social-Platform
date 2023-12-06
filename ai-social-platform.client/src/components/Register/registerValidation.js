import * as Yup from 'yup';

import { RegisterFormKeys } from '../../core/environments/costants';

const passwordRulse = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{5,}/;
// min 5 characters, 1 upper case letter, 1 lower case letter, 1 numeric digit.

const registerValidation = Yup.object({
    [RegisterFormKeys.FirstName]: Yup.string().required(
        'First name is required.'
    ),
    [RegisterFormKeys.LastName]: Yup.string().required(
        'Last name is required.'
    ),
    [RegisterFormKeys.Email]: Yup.string()
        .required('Email is required.')
        .email('Invalid email format'),
    [RegisterFormKeys.Password]: Yup.string()
        .min(5, 'Password must be at least 5 characters long')
        .required('Password is required')
        .matches(
            passwordRulse,
            'Password must have at least 1 upper case letter, 1 lower case letter and 1 number.'
        ),
    [RegisterFormKeys.ConfirmPassword]: Yup.string()
        .required('Confirm password is required.')
        .oneOf(
            [Yup.ref(RegisterFormKeys.Password), null],
            'Passwords miss match.'
        ),
});

export default registerValidation;
