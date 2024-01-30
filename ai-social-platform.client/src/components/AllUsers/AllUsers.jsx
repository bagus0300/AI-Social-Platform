import { useEffect, useState } from 'react';

import * as userService from '../../core/services/userService';
import styles from './AllUsers.module.css';

import User from './User/User';

export default function AllUsers() {
    const [users, setUsers] = useState([]);

    useEffect(() => {
        userService
            .getAllUsers()
            .then((result) => setUsers(result))
            .catch((error) => console.log(error));
    }, []);

    return (
        <section className={styles['users-section']}>
            <h3 className={styles['users-section-heading']}>Users</h3>
            <ul className={styles['users-list']}>
                {users.map((user) => (
                    <User key={user.id} user={user} />
                ))}
            </ul>
        </section>
    );
}
