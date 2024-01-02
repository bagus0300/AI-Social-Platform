import { Link } from 'react-router-dom';

import { PATH } from '../../core/environments/costants';
import styles from './NotFound.module.css';

export default function NotFound() {
    return (
        <section className={styles['not-found-section']}>
            <h2 className={styles['section-heading']}>404</h2>
            <p className={styles['text']}>
                We are sorry, but the page you requested was not found.
            </p>
            <Link to={PATH.home} className={styles['home-button']}>
                Home
            </Link>
        </section>
    );
}
