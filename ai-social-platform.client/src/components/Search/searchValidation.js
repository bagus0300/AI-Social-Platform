import { SearchFormKeys } from '../../core/environments/costants';
import * as Yup from 'yup';

const searchValidation = Yup.object({
    [SearchFormKeys.SearchText]: Yup.string().required(
        'Search field cannot be empty.'
    ),
});

export default searchValidation;
