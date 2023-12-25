import * as api from '../../core/services/api';
import { ContentType, endpoints } from '../environments/costants';

export const createComment = async (commentData) =>
    await api.post(
        endpoints.createComment,
        commentData,
        ContentType.ApplicationJSON
    );

export const getAllComments = async (postId) =>
    await api.get(`${endpoints.getAllComments(postId)}?page=1`);

export const deleteComment = async (commentId) => {
    return await api.remove(endpoints.deleteComment(commentId));
};
