import { useContext } from 'react';

import styles from './RightAside.module.css';
import AuthContext from '../../contexts/authContext';

import AllUsers from '../AllUsers/AllUsers';

export default function RightAside() {
    const { isAuthenticated } = useContext(AuthContext);

    return isAuthenticated ? (
        <aside className={styles['right-aside']}>
            <AllUsers />
        </aside>
    ) : null;
}
