import User from '../AllUsers/User/User';
import styles from './ConfrimUnfollow.module.css';

export default function ConfrimUnfollow({
    unfollowPerson,
    closeConfirmUnfollowModal,
    user
}) {
    return (
        <section className={styles['confrim-unfollow-section']}>
            <p className={styles['unfollow-text']}>Are you sure you want to unfollow</p>
            <User user={user} />
            <div className={styles['unfollow-section-buttons']}>
                <p
                    onClick={unfollowPerson}
                    className={styles['confirm-button']}
                >
                    Yes
                </p>
                <p
                    onClick={closeConfirmUnfollowModal}
                    className={styles['cancel-button']}
                >
                    Cancel
                </p>
            </div>
        </section>
    );
}
