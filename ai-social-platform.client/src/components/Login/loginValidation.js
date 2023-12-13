import * as Yup from 'yup';

import { LoginFormKeys } from '../../core/environments/costants';

const loginValidation = Yup.object({
    [LoginFormKeys.Email]: Yup.string().required(),
    [LoginFormKeys.Password]: Yup.string().required(),
});

export default loginValidation;
