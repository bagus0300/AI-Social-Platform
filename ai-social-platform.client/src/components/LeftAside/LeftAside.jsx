import { useContext } from 'react';

import styles from './LeftAside.module.css';
import AuthContext from '../../contexts/authContext';

export default function LeftAside() {
    const { isAuthenticated } = useContext(AuthContext);

    return isAuthenticated ? (
        <aside className={styles['left-aside']}>
            <h3 className={styles['topic-heading']}>Topics</h3>
        </aside>
    ) : null;
}
