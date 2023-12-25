import { endpoints } from '../environments/costants';
import { ContentType } from '../environments/costants';
import * as api from './api';

export const login = async (email, password) => {
    const result = await api.post(
        endpoints.login,
        { email, password },
        ContentType.ApplicationJSON
    );

    return result;
};

export const register = async (values) =>
    await api.post(endpoints.register, values, ContentType.ApplicationJSON);

export const logout = async () => {
    await api.post(endpoints.logout, null, ContentType.ApplicationJSON);
};
