import User from '../AllUsers/User/User';
import styles from './SuccessfullyFollow.module.css';

export default function SuccessfullyFollow({ user }) {
    return (
        <section className={styles['successfully-follow-section']}>
            <div className={styles['successfully-content']}>
                <p className={styles['successfully-text']}>
                    You successfully followed
                </p>
                <User user={user} />
            </div>
        </section>
    );
}
