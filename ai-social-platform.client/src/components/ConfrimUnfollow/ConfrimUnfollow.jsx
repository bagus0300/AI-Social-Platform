import styles from './ConfrimUnfollow.module.css';

export default function ConfrimUnfollow({
    unfollowPerson,
    closeConfirmUnfollowModal,
}) {
    return (
        <section className={styles['confrim-unfollow-section']}>
            <p>Are you sure you want to unfollow Pesho?</p>
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
