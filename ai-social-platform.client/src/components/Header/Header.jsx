import { Link } from 'react-router-dom';

import { PATH } from '../../core/environments/costants';
import styles from './Header.module.css';
import { useContext } from 'react';
import AuthContext from '../../contexts/authContext';

export default function Header() {
    const { isAuthenticated } = useContext(AuthContext);

    return (
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
                    <div className={styles['user']}>
                        <p>Profile</p>
                        <p>Logout</p>
                    </div>
                )}
            </section>
        </header>
    );
}
