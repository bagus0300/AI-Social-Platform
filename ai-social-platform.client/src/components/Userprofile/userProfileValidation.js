import * as Yup from 'yup';

import { RegisterFormKeys } from '../../core/environments/costants';

const passwordRulse = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{5,}/;
// min 5 characters, 1 upper case letter, 1 lower case letter, 1 numeric digit.

const userProfileValidation = Yup.object({
    [RegisterFormKeys.FirstName]: Yup.string().required(
        'First name is required.'
    ),
    [RegisterFormKeys.LastName]: Yup.string().required(
        'Last name is required.'
    ),
});

export default userProfileValidation;
