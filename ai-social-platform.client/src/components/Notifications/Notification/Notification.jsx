import { useEffect, useState } from 'react';
import styles from './Notification.module.css';

import * as userService from '../../../core/services/userService';
import * as notificationsService from '../../../core/services/notificationsService';

import { ContentType } from '../../../core/environments/costants';
import dateFormater from '../../../utils/dateFormatter';

export default function Notification({
    notification,
    clearNotificationsCount,
}) {
    const [userData, setUserData] = useState({});

    useEffect(() => {
        userService
            .getUserDetails(notification.creatingUserId)
            .then((user) => {
                setUserData(user);
            })
            .catch((error) => console.log(error));

        notificationsService
            .readNotification(notification.id, ContentType.ApplicationJSON)
            .then(clearNotificationsCount())
            .catch((error) => console.log(error));
    }, [notification]);

    return (
        <>
            <div className={styles['notification-wrapper']}>
                <div>
                    <img
                        className={styles['notification-media']}
                        src={
                            userData.profilePictureUrl ||
                            '/images/default-profile-pic.png'
                        }
                        alt="profile-pic"
                    />
                </div>
                <div className={styles['notification-content']}>
                    <p>{notification.content}</p>
                    <p className={styles['notification-created']}>
                        on {dateFormater(notification.dateCreated)}
                    </p>
                </div>
            </div>
        </>
    );
}
