import { endpoints } from '../environments/costants';
import * as api from './api';

export const getNotificationsCount = async () =>
    await api.get(`${endpoints.getNotificationsCount}`);

export const getNotifications = async () =>
    await api.get(`${endpoints.getNotifications}`);

export const readNotification = async (notificationId) =>
    await api.post(endpoints.readNotification(notificationId));
