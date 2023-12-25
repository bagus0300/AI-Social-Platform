import * as Yup from 'yup';

import { CreateFormKeys } from '../../core/environments/costants';

const createPostValidation = Yup.object({
    [CreateFormKeys.PostDescription]: Yup.string().min(1),
    [CreateFormKeys.PostMedia]: Yup.string().min(1),
});

export default createPostValidation;
