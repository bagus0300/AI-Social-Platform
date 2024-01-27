import { endpoints } from '../environments/costants';
import { ContentType } from '../environments/costants';
import * as api from './api';

export const generateTextWhitOpenAi = async (data) =>
    await api.post(endpoints.getTextWhitOpenAi, data, ContentType.ApplicationJSON);