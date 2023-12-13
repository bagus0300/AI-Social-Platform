import { endpoints } from '../environments/costants';
import * as api from './api';

export const getLoggedUserDetails = async () => {
    const result = await api.get(endpoints.getLoggedUserDetails);

    return result;
};
