import { ContentType, tokenName, host } from '../environments/costants';

const buildOptions = (data, requestType) => {
    const options = {};

    if (requestType === ContentType.ApplicationJSON) {
        options.body = JSON.stringify(data);
        options.headers = {
            'Content-Type': ContentType.ApplicationJSON,
        };
    } else if (requestType === ContentType.MulitpartFormData) {
        options.body = data;
    }

    const token = localStorage.getItem(tokenName);

    if (token) {
        options.headers = {
            ...options.headers,
            Authorization: `Bearer ${token}`,
        };
    }

    return options;
};

const api = async (method, url, data, requestType) => {
    const response = await fetch(host + url, {
        ...buildOptions(data, requestType),
        method,
    });

    if (response.status === 204) {
        return {};
    }

    const result = await response.json();

    if (!response.ok) {
        if (response.status === 403) {
            localStorage.removeItem(tokenName);
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