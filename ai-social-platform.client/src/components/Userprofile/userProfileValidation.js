import * as Yup from 'yup';

import { ProfileFormKeys } from '../../core/environments/costants';

const passwordRulse = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{5,}/;
// min 5 characters, 1 upper case letter, 1 lower case letter, 1 numeric digit.

const userProfileValidation = Yup.object({
        
    [ProfileFormKeys.FirstName]: Yup.string()
    .required('First name is required.')
    .min(1, 'First name must be at least 1 characters.')
    .max(15, 'First name must be at most 15 characters.'),
    
    [ProfileFormKeys.LastName]: Yup.string()
    .required('Last name is required.')
    .min(1, 'Last name must be at least 1 characters.')
    .max(15, 'Last name must be at most 15 characters.'),
    
    [ProfileFormKeys.PhoneNumber]: Yup.string()
    .min(8, 'Phone Number must be at least 8 characters.')
    .max(10, 'Phone Number must be at most 10 characters.'),
    
    [ProfileFormKeys.Country]: Yup.string()
    .min(3, 'Country must be at least 3 characters.')
    .max(50, 'Country must be at most 50 characters.'),
    
    [ProfileFormKeys.State]: Yup.string()
    .min(3, 'State must be at least 3 characters.')
    .max(50, 'State must be at most 50 characters.'),
    
    [ProfileFormKeys.School]: Yup.string()
    .min(3, 'Last name must be at least 3 characters.')
    .max(50, 'Last name must be at most 50 characters.'),
});

export default userProfileValidation;
