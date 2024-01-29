import { endpoints } from '../environments/costants';
import { ContentType } from '../environments/costants';
import * as api from './api';

export const addMedia = async (media) =>
    await api.post(endpoints.addMedia, media, ContentType.MulitpartFormData);

export const getPostMedia = async (postId) =>
    await api.get(endpoints.getPostMedia(postId));

export const getMediaById = async (mediaId) =>
    await api.get(endpoints.getMediaById(mediaId));

export const getMediaByPostId = async (postId) =>
    await api.get(endpoints.getMediaByPostId(postId));

export const deleteMedia = async (mediaId) =>
    await api.post(endpoints.deleteMedia(mediaId));

export const editMedia = async (mediaId, media) =>
    await api.put(endpoints.editMedia(mediaId),media,ContentType.MulitpartFormData);
