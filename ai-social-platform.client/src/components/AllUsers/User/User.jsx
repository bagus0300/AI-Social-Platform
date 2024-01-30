import { useNavigate } from 'react-router-dom';

import { PATH } from '../../../core/environments/costants';
import styles from './User.module.css';

export default function User({ user }) {
    const navigate = useNavigate();

    const goToProfilePage = () => navigate(PATH.userProfile(user.id));

    return (
        <li className={styles['user-item']}>
            <div className={styles['user-info-wrapper']}>
                <img
                    onClick={goToProfilePage}
                    className={styles['user-img']}
                    src={
                        user?.profilePictureUrl ||
                        '/images/default-profile-pic.png'
                    }
                    alt=""
                />
                <p
                    className={styles['user-username']}
                    onClick={goToProfilePage}
                >
                    {user.firstName} {user.lastName}
                </p>
            </div>
            <p className={styles['add-friend-btn']}>Follow</p>
        </li>
    );
}
