import { endpoints } from '../environments/costants';
import * as api from './api';
import { ContentType } from '../environments/costants';

export const getLoggedUserDetails = async () => {
    const result = await api.get(endpoints.getLoggedUserDetails);

    return result;
};

export const getUserData = async (userId) => {
    const result = await api.get(`${endpoints.userDetails}/${userId}`);

    return result;
};

export const update = async (values) =>
    await api.put(endpoints.updateUser, values, ContentType.MulitpartFormData);

export const getUserDetails = async (userId) =>
    await api.get(endpoints.userDetails(userId));

export const addFriend = async (userId) =>
    await api.post(endpoints.addFriend(userId));

export const removeFriend = async (userId) =>
    await api.post(endpoints.removeFriend(userId));

export const getFriendsData = async (userId) =>
    await api.get(endpoints.allFriends(userId));

export const getAllUsers = async () => await api.get(endpoints.getAllUsers);

export const isFriend = async (friendId) =>
    await api.get(`${endpoints.isFriend(friendId)}`);
