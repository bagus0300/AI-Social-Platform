import { endpoints } from '../environments/costants';
import * as api from './api';

export const search = async (type, query) =>
    await api.get(endpoints.search(type, query));
