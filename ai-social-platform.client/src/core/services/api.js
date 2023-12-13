import { host } from '../environments/costants';

const buildOptions = (data) => {
    const options = {};

    if (data) {
        options.body = JSON.stringify(data);
        options.headers = {
            'Content-Type': 'application/json',
        };
    }

    const token = localStorage.getItem('accessToken');

    if (token) {
        options.headers = {
            ...options.headers,
            Authorization: `Bearer ${token}`,
        };
    }

    return options;
};

const api = async (method, url, data) => {
    const response = await fetch(host + url, {
        ...buildOptions(data),
        method,
    });

    if (response.status === 204) {
        return {};
    }

    const result = await response.json();

    if (!response.ok) {
        if (response.status === 403) {
            localStorage.removeItem('accessToken');
        }

        throw result;
    }

    return result;
};

export const get = api.bind(null, 'GET');
export const post = api.bind(null, 'POST');
export const put = api.bind(null, 'PUT');
export const patch = api.bind(null, 'PATCH');
export const remove = api.bind(null, 'DELETE');
