import { endpoints } from '../environments/costants';
import * as api from './api';

export const login = async (email, password) => {
    const result = await api.post(endpoints.login, { email, password });

    return result;
};
