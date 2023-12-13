import { useContext, useState } from 'react';
import { Link } from 'react-router-dom';

import { PATH } from '../../core/environments/costants';
import styles from './Header.module.css';
import AuthContext from '../../contexts/authContext';

export default function Header() {
    const [showUserMenu, setShowUserMenu] = useState(false);
    const { isAuthenticated, firstName, lastName } = useContext(AuthContext);

    const showUserMenuToggle = () => setShowUserMenu(!showUserMenu);

    const hideMenus = () => {
        setShowUserMenu(false);
    };

    return (
        <>
            {showUserMenu && (
                <div onClick={hideMenus} className={styles['backdrop']}></div>
            )}
            <header className={styles['app-header']}>
                <h1 className={styles['logo-heading']}>
                    <Link className={styles['logo-text']} to={PATH.home}>
                        AI-Social-Platform
                    </Link>
                </h1>
                <form className={styles['search-form']}>
                    <input type="search" placeholder="Search..." />
                    <button>Search</button>
                </form>
                <section className={styles['profile']}>
                    {!isAuthenticated ? (
                        <div className={styles['guest']}>
                            <Link to={PATH.login}>Login</Link>
                            <Link to={PATH.register}>Register</Link>
                        </div>
                    ) : (
                        <div>
                            <ul className={styles['user']}>
                                <li className={styles['menu']}>
                                    <div className={styles['media']}>
                                        <i class="fa-solid fa-bell"></i>
                                    </div>
                                </li>
                                <li
                                    onClick={showUserMenuToggle}
                                    className={styles['menu']}
                                >
                                    <img
                                        src="/images/default-profile-pic.png"
                                        alt=""
                                    />
                                </li>
                            </ul>
                        </div>
                    )}
                </section>
                {showUserMenu && (
                    <div className={styles['user-menu']}>
                        <Link className={styles['user-info']}>
                            <img src="/images/default-profile-pic.png" alt="" />
                            <p>
                                {firstName} {lastName}
                            </p>
                        </Link>
                        <Link
                            className={styles['logout']}
                            onClick={hideMenus}
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
