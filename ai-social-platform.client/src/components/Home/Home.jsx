import { Link } from 'react-router-dom';

import styles from './Home.module.css';

export default function Home() {
    return (
        <section className={styles['home-page']}>
            <Link to={'/login'}>Click to open Login Component</Link>
            <Link to={'/register'}> Click to open Register Component</Link>
        </section>
    );
}