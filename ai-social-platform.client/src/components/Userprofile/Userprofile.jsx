import { useState, useEffect, useContext } from 'react';
import styles from "./Userprofile.css?inline";
import * as userService from '../../core/services/userService';
import { Link, useParams } from 'react-router-dom';
import { PATH } from '../../core/environments/costants';
import { pathToUrl } from '../Userprofile/pathUtils';
import AuthContext from '../../contexts/authContext';

export default function Userprofile() {
    const { userId } = useParams();

    const [userData, setUserData] = useState(null);
    const [authUserData, setAuthUserData] = useState(null);
    // friendsData - the logged-in user's friends
    const [friendsData, setFriendsData] = useState(null);
    // friendsDataFriend - the friends of a non-logged-in user
    const [friendsDataFriend, setFriendsDataFriend] = useState(null);
    const [error, setError] = useState(null);
    const authContext = useContext(AuthContext);
    let isUserFriend;

    useEffect(() => {
        // Използваме Promise.all за изчакване на завършването на двете заявки
        Promise.all([
            userService.getUserDetails(userId),
            userService.getUserDetails(authContext.userId),
            userService.getFriendsData(authContext.userId),
            userService.getFriendsData(userId),
        ])
            .then(
                ([
                    userResult,
                    authUserResult,
                    friendsResult,
                    friendsResultFriend,
                ]) => {
                    setUserData(userResult);
                    setAuthUserData(authUserResult);
                    setFriendsData(friendsResult);
                    setFriendsDataFriend(friendsResultFriend);
                }
            )
            .catch((error) => setError(error));
    }, [userId, authContext.userId, isUserFriend]);

    if (error) {
        return <div>{error}</div>;
    }

    if (!userData) {
        return <div>Loading...</div>;
    }

    let formattedBirthday = '';
    if (userData.birthday) {
        const date = new Date(userData.birthday);
        const day = date.getDate();
        const month = date.getMonth() + 1;
        const year = date.getFullYear();
        formattedBirthday = `${day < 10 ? '0' : ''}${day}/${
            month < 10 ? '0' : ''
        }${month}/${year}`;
    }
    const isCurrentUserProfile = userId === authContext.userId;
    isUserFriend = friendsData.some((friend) => friend.id === userData.id);

    let imageUrl;

    if (userData.profilePictureUrl) {
        imageUrl = `${userData.profilePictureUrl}?${Math.random()}`;
    } else {
        imageUrl = '../../../public/images/default-profile-pic.png';
    }

    let imageUrlCover;
    if (userData.coverPhotoUrl) {
        imageUrlCover = `${userData.coverPhotoUrl}?${Math.random()}`;
    } else {
        imageUrlCover = '../../../public/images/Logo.png';
    }

    const handleAddFriend = async () => {
        try {
            await userService.addFriend(userId);
            // Промени състоянието, че сега потребителят е приятел
            isUserFriend = true;
            //Извикване на нова заявка, за да актуализира информацията за приятелите
            const friendsResult = await userService.getFriendsData(
                authContext.userId
            );
            setFriendsData(friendsResult);
            const friendsResultFriend = await userService.getFriendsData(
                userId
            );
            setFriendsDataFriend(friendsResultFriend);
        } catch (error) {
            setError(error);
        }
    };

    const handleRemoveFriend = async () => {
        try {
            await userService.removeFriend(userId);
            // Промени състоянието, че сега потребителят не е приятел
            isUserFriend = false;
            // Извикване на нова заявка, за да актуализира информацията за приятелите
            const friendsResult = await userService.getFriendsData(
                authContext.userId
            );
            setFriendsData(friendsResult);
            const friendsResultFriend = await userService.getFriendsData(
                userId
            );
            setFriendsDataFriend(friendsResultFriend);
        } catch (error) {
            setError(error.message);
        }
    };

    return (
        <div className="user-profile">
            <article className="post-item">
                <img
                    className="user-cover"
                    src={imageUrlCover || '../../../public/images/Logo.png'}
                    alt="User cover photo"
                />
                <div className="user-info-wrapper">
                    <img
                        className="user-img"
                        src={
                            imageUrl ||
                            '../../../public/images/default-profile-pic.png'
                        }
                        alt="User profile pic"
                    />

                    <fieldset className="userprofile-fieldset">
                        <legend>Contact information</legend>
                        <p className="username-profile">
                            {userData.firstName} {'  '} {userData.lastName}
                        </p>

                        <p className="posted-user">
                            E-mail:
                            <a href="mailto: {userData?.userName}">
                                {' '}
                                {userData?.userName}
                            </a>
                        </p>
                        <p className="posted-user">
                            Phone Number: {userData.phoneNumber}
                        </p>
                    </fieldset>
                    <div className="edit">
                        {isCurrentUserProfile && (
                            <Link
                                to={pathToUrl(PATH.profileedit, {
                                    userId: userId,
                                })}
                            >
                                <img
                                    className="edit-img"
                                    src="../../../public/images/edit-pen-icon-6.jpg"
                                    alt="edit button"
                                />
                            </Link>
                        )}
                        {!isCurrentUserProfile && (
                            <>
                                {isUserFriend ? (
                                    <button
                                        className="profile-button"
                                        onClick={handleRemoveFriend}
                                    >
                                        Remove from Friends
                                    </button>
                                ) : (
                                    <button
                                        className="profile-button"
                                        onClick={handleAddFriend}
                                    >
                                        Add as Friend
                                    </button>
                                )}
                            </>
                        )}
                    </div>
                </div>

                <fieldset className="userprofile-fieldset">
                    <legend>Personal data</legend>

                    <p className="posted-user">Country: {userData.country}</p>
                    <p className="posted-user">State: {userData.state}</p>
                    <p className="posted-user">Gender: {userData.gender}</p>
                    <p className="posted-user">School: {userData.school}</p>
                    <p className="posted-user">
                        Birthday (dd-mm-yyyy): {formattedBirthday}
                    </p>
                    <p className="posted-user">
                        Relationship Status: {userData.relationship}
                    </p>
                </fieldset>
                <fieldset className="userprofile-fieldset">
                    <legend>Friends</legend>
                    <div>
                        <ul>
                            {isCurrentUserProfile
                                ? friendsData.map((friend) => (
                                      <li
                                          className="userprofile-li"
                                          key={friend.id}
                                      >
                                          <Link
                                              to={PATH.userProfile(friend.id)}
                                          >
                                              <img
                                                  className="friend-img"
                                                  src={
                                                      friend.profilePictureUrl ||
                                                      '../../../public/images/default-profile-pic.png'
                                                  }
                                                  alt="User profile pic"
                                              />{' '}
                                              {friend.firstName}{' '}
                                              {friend.lastName}
                                          </Link>
                                      </li>
                                  ))
                                : friendsDataFriend.map((friend) => (
                                      <li
                                          className="userprofile-li"
                                          key={friend.id}
                                      >
                                          <Link
                                              to={PATH.userProfile(friend.id)}
                                          >
                                              <img
                                                  className="friend-img"
                                                  src={
                                                      friend.profilePictureUrl ||
                                                      '../../../public/images/default-profile-pic.png'
                                                  }
                                                  alt="User profile pic"
                                              />{' '}
                                              {friend.firstName}{' '}
                                              {friend.lastName}
                                          </Link>
                                      </li>
                                  ))}
                        </ul>
                    </div>
                </fieldset>
            </article>
        </div>
    );
}
