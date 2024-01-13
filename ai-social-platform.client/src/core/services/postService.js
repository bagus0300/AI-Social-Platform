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
