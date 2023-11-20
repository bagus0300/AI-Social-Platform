import { Link } from 'react-router-dom';

import styles from './Home.module.css';
import Posts from '../Posts/Posts';

export default function Home() {
    return (
        <section className={styles['home-page']}>
            <Posts />
        </section>
    );
}