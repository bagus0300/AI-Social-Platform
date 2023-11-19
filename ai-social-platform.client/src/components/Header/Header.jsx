import { Link } from 'react-router-dom';

import styles from './Header.module.css';

export default function Header() {
    return (
        <header className={styles['app-header']}>
            <Link to={'/'}>
                <h2>AI-Social-Platform</h2>
            </Link>
            <form className={styles['search-form']}>
                <input type="search" placeholder="Search..." />
                <button>Search</button>
            </form>
            <section className={styles['profile']}>
                <Link to={'/login'}>Login</Link>
                <Link to={'/register'}>Register</Link>
            </section>
        </header>
    );
}
