import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';

import * as notificationsService from '../../core/services/notificationsService';
import styles from './Notifications.module.css';

import Notification from './Notification/Notification';
import PaginationSpinner from '../PaginationSpinner/PaginationSpinner';

export default function Notifications({ clearNotificationsCount }) {
    const [notifications, setNotifications] = useState([]);

    const [isLoading, setIsLoading] = useState(true);

    const navigate = useNavigate();

    useEffect(() => {
        notificationsService
            .getNotifications()
            .then((result) => {
                setNotifications(result);
            })
            .catch((error) => console.log(error))
            .finally(() => setIsLoading(false));
    }, []);

    return (
        <section className={styles['notifications-section']}>
            <div className={styles['notification-spinner']}>
                {isLoading && <PaginationSpinner />}
            </div>
            {!isLoading &&
                notifications.length > 0 &&
                notifications.map((notification) => (
                    <li
                        onClick={() => navigate(notification.redirectUrl)}
                        className={styles['notification']}
                        key={notification.id}
                    >
                        <Notification
                            notification={notification}
                            clearNotificationsCount={clearNotificationsCount}
                        />
                    </li>
                ))}
            {!isLoading && notifications.length === 0 && (
                <p>No notifications yet.</p>
            )}
        </section>
    );
}
