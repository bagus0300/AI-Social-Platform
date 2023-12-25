import { endpoints } from '../environments/costants';
import { ContentType } from '../environments/costants';
import * as api from './api';

export const addMedia = async (media) =>
    await api.post(endpoints.addMedia, media, ContentType.MulitpartFormData);

export const getPostMedia = async (postId) =>
    await api.get(endpoints.getPostMedia(postId));

export const getMediaById = async (mediaId) =>
    await api.get(endpoints.getMediaById(mediaId));
