import { useContext } from 'react';

import styles from './LeftAside.module.css';
import AuthContext from '../../contexts/authContext';

import Translator from '../Translator/Translator';

export default function LeftAside() {
    const { isAuthenticated } = useContext(AuthContext);

    return isAuthenticated ? (
        <aside className={styles['left-aside']}>
            <h3 className={styles['topic-heading']}>Topics</h3>
            <section className={styles['topics']}>
                <p className={styles['topics-text']}>Coming Soon</p>
            </section>
            <Translator />
        </aside>
    ) : null;
}
