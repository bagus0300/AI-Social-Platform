import { endpoints } from '../environments/costants';
import * as api from './api';

export const login = async (email, password) => {
    const result = await api.post(endpoints.login, { email, password });

    return result;
};

export const register = async (values) =>
    await api.post(endpoints.register, values);

export const logout = async () => {
    await api.post(endpoints.logout);
};
