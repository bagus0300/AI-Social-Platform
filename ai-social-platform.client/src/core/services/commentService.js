import { ContentType, endpoints } from '../environments/costants';
import * as api from '../../core/services/api';

export const createComment = async (commentData) =>
    await api.post(
        endpoints.createComment,
        commentData,
        ContentType.ApplicationJSON
    );

export const getAllComments = async (postId, page) =>
    await api.get(`${endpoints.getAllComments(postId)}?page=${page}`);

export const editComment = async (commentId, commentData) =>
    await api.put(
        `${endpoints.editComment(commentId)}`,
        { content: commentData },
        ContentType.ApplicationJSON
    );

export const deleteComment = async (commentId) =>
    await api.remove(endpoints.deleteComment(commentId));
