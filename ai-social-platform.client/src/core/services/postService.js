import { endpoints } from '../environments/costants';
import { ContentType } from '../environments/costants';
import * as api from './api';

export const createPost = async (postData) =>
    await api.post(endpoints.createPost, postData, ContentType.ApplicationJSON);

export const getAllPosts = async () =>
    await api.get(`${endpoints.getAllPosts}?page=1`);
