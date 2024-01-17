import styles from './PostTemplate.module.css';

export default function PostTemplate() {
    return (
        <article className={styles['post-item']}>
            <section className={styles['user-info']}>
                <div className={styles['user-info-wrapper']}>
                    <div className={styles['user-img']}></div>
                    <div className={styles['post-info']}>
                        <div className={styles['names-wrapper']}>
                            <p className={styles['first-name']}></p>
                            <p className={styles['last-name']}></p>
                        </div>
                        <p className={styles['posted-on']}>Posted on:</p>
                    </div>
                </div>
            </section>
            <section className={styles['content-description']}>
                <p>Loading...</p>
            </section>
            <section className={styles['media']}></section>
            <section className={styles['likes']}>
                <div className={styles['likes-count']}>
                    <i className="fa-solid fa-thumbs-up"></i>
                    <p>Loading...</p>
                </div>
                <p className={styles['comments-count']}>Loading...</p>
            </section>
            <section className={styles['buttons']}>
                <div className={styles['like-button']}>
                    <i className="fa-solid fa-thumbs-up"></i>
                    <p>Like</p>
                </div>
                <div className={styles['comment-button-wrapper']}>
                    <i className="fa-solid fa-comment"></i>
                    <p className={styles['comment-button']}>Comment</p>
                </div>
            </section>
            <section className={styles['add-comment']}>
                <div className={styles['comment-user-img']}></div>
                <div className={styles['comment-area']}></div>
            </section>
        </article>
    );
}
