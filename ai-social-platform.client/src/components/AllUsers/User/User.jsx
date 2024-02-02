import { useContext, useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';

import * as userService from '../../../core/services/userService';

import { PATH } from '../../../core/environments/costants';
import styles from './User.module.css';
import SuccessfullyFollow from '../../SuccessfullyFollow/SuccessfullyFollow';
import AuthContext from '../../../contexts/authContext';
import ConfrimUnfollow from '../../ConfrimUnfollow/ConfrimUnfollow';

export default function User({ user }) {
    const [isFriend, setIsFriend] = useState(false);

    const [successfullyFollow, setSuccessfullyFollow] = useState(false);

    const [unfollow, setUnfollow] = useState(false);

    const { userId } = useContext(AuthContext);

    useEffect(() => {
        userService
            .isFriend(user.id)
            .then((result) => setIsFriend(result))
            .catch((error) => console.log(error));
    }, []);

    const navigate = useNavigate();

    const goToProfilePage = () => navigate(PATH.userProfile(user.id));

    const onFollowButtonClickHandler = async () => {
        try {
            setSuccessfullyFollow(true);

            await userService.addFriend(user.id);
            setIsFriend(true);

            const timeout = setTimeout(() => {
                setSuccessfullyFollow(false);
            }, 2000);

            return () => clearTimeout(timeout);
        } catch (error) {
            console.log(error);
        }
    };

    const onUnfollowButtonClickHandler = async () => setUnfollow(true);

    const unfollowPerson = async () => {
        try {
            await userService.removeFriend(user.id);
            setIsFriend(false);
            setUnfollow(false);
        } catch (error) {
            console.log(error);
        }
    };

    const closeConfirmUnfollowModal = () => setUnfollow(false);

    return (
        <>
            <li className={styles['user-item']}>
                {successfullyFollow && <SuccessfullyFollow user={user} />}
                {unfollow && (
                    <ConfrimUnfollow
                        unfollowPerson={unfollowPerson}
                        closeConfirmUnfollowModal={closeConfirmUnfollowModal}
                        user={user}
                    />
                )}
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
                {!isFriend && userId !== user.id && (
                    <span
                        onClick={onFollowButtonClickHandler}
                        className={styles['add-friend-btn']}
                    >
                        Follow
                    </span>
                )}
                {isFriend && userId !== user.id && (
                    <span
                        onClick={onUnfollowButtonClickHandler}
                        className={styles['remove-friend-btn']}
                    >
                        Unfollow
                    </span>
                )}
            </li>
        </>
    );
}
