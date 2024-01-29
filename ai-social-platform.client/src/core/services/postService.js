import { endpoints } from '../environments/costants';
import { ContentType } from '../environments/costants';
import * as api from './api';

export const createPost = async (postData) =>
    await api.post(endpoints.createPost, postData, ContentType.ApplicationJSON);

export const getAllPosts = async (page) =>
    await api.get(`${endpoints.getAllPosts}?page=${page}`);

export const getPostById = async (postId) =>
    await api.get(`${endpoints.getPostById(postId)}`);

export const deletePost = async (postId) =>
    await api.remove(endpoints.deletePost(postId));

export const editPost = async (postId, requestBody) =>
    await api.put(endpoints.editPost(postId), requestBody, ContentType.ApplicationJSON);

// export const getPostsByUserId = async (userId) =>
//     await api.get(`${endpoints.getPostsByUserId(userId)}`);
    
export const getPostsByUserId = async (userId, page) =>
    await api.get(`${endpoints.getPostsByUserId(userId)}?page=${page}`);
