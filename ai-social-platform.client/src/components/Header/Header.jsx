import { useContext, useEffect, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';

import * as notificationsService from '../../core/services/notificationsService';

import { PATH } from '../../core/environments/costants';
import styles from './Header.module.css';
import AuthContext from '../../contexts/authContext';

import Notifications from '../Notifications/Notifications';

import Logo from '/images/Logo-White.svg'

export default function Header() {
    const [showUserMenu, setShowUserMenu] = useState(false);
    const [showNotificationsMenu, setShowNotificationsMenu] = useState(false);

    const { isAuthenticated, firstName, lastName, userId, avatar } =
        useContext(AuthContext);
    const [notificationsCount, setNotificationsCount] = useState(0);

    useEffect(() => {
        if (isAuthenticated) {
            const interval = setInterval(() => {
                notificationsService
                    .getNotificationsCount()
                    .then((result) => setNotificationsCount(result))
                    .catch((error) => console.log(error));
            }, 5000);

            return () => clearInterval(interval);
        }
    }, [isAuthenticated]);

    const navigate = useNavigate();

    const showUserMenuToggle = () => {
        setShowUserMenu(!showUserMenu);
        setShowNotificationsMenu(false);
    };

    const showNotificationsMenuToggle = () => {
        setShowNotificationsMenu(!showNotificationsMenu);
        setShowUserMenu(false);
    };

    const clearNotificationsCount = () =>
        setNotificationsCount((state) =>
            state - 20 < 0 ? state === 0 : state - 20
        );

    const hideMenus = () => {
        setShowUserMenu(false);
        setShowNotificationsMenu(false);
    };

    const onLogoutClickHandler = () => {
        hideMenus();
        setNotificationsCount(0);
    };

    const openSearchPage = () => navigate(PATH.search);

    const formSubmit = (e) => e.preventDefault();

    return (
        <>
            {showNotificationsMenu && (
                <Notifications
                    clearNotificationsCount={clearNotificationsCount}
                />
            )}
            {(showUserMenu || showNotificationsMenu) && (
                <div onClick={hideMenus} className={styles['backdrop']}></div>
            )}
            <header className={styles['app-header']}>
            <img src={Logo} alt="AI-Social-Platform Logo" className={styles['logo-image']} />
                <h1 className={styles['logo-heading']}>
                    <Link className={styles['logo-text']} to={PATH.home}>
                        AI-Social-Platform
                    </Link>
                </h1>
                <section className={styles['profile']}>
                    {isAuthenticated && (
                        <form
                            onClick={openSearchPage}
                            onSubmit={formSubmit}
                            className={styles['search-form']}
                        >
                            <div className={styles['search-input-wrapper']}>
                                <input placeholder="Search..." />
                                <div
                                    className={styles['search-input-cover']}
                                ></div>
                            </div>
                            <button>Search</button>
                        </form>
                    )}
                    {!isAuthenticated ? (
                        <div className={styles['guest']}>
                            <Link to={PATH.login}>Login</Link>
                            <Link to={PATH.register}>Register</Link>
                        </div>
                    ) : (
                        <div>
                            <ul className={styles['user']}>
                                <li
                                    onClick={showNotificationsMenuToggle}
                                    className={styles['menu']}
                                >
                                    <div className={styles['media']}>
                                        <i className="fa-solid fa-bell"></i>
                                    </div>
                                    {notificationsCount > 0 && (
                                        <div
                                            className={
                                                styles[
                                                    'notifications-count-wrapper'
                                                ]
                                            }
                                        >
                                            <p>{notificationsCount}</p>
                                        </div>
                                    )}
                                </li>
                                <li
                                    onClick={showUserMenuToggle}
                                    className={styles['menu']}
                                >
                                    <img
                                        src={
                                            avatar ||
                                            '/images/default-profile-pic.png'
                                        }
                                        alt="profile-pic"
                                    />
                                </li>
                            </ul>
                        </div>
                    )}
                </section>
                {showUserMenu && (
                    <div className={styles['user-menu']}>
                        <Link
                            onClick={hideMenus}
                            to={PATH.userProfile(userId)}
                            className={styles['user-info']}
                        >
                            <img
                                src={
                                    avatar || '/images/default-profile-pic.png'
                                }
                                alt="profile-pic"
                            />
                            <p>
                                {firstName} {lastName}
                            </p>
                        </Link>
                        <Link
                            className={styles['logout']}
                            onClick={onLogoutClickHandler}
                            to={PATH.logout}
                        >
                            <i className="fa-solid fa-right-from-bracket"></i>{' '}
                            <span>Logout</span>
                        </Link>
                    </div>
                )}
            </header>
        </>
    );
}
