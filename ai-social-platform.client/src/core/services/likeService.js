import { endpoints } from '../environments/costants';
import * as api from './api';

export const getLikes = async (postId) =>
    await api.get(`${endpoints.getLikes(postId)}`);

export const addLike = async (postId) =>
    await api.post(`${endpoints.addLike(postId)}`);

export const removeLike = async (likeId) =>
    await api.remove(`${endpoints.removeLike(likeId)}`);