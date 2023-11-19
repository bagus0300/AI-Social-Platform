import PostItem from './PostItem/PostItem';
import styles from './Posts.module.css';

export default function Posts() {
    return (
        <section className={styles['posts-section']}>
            <div className={styles['content-wrapper']}>
                <PostItem />
                <PostItem />
            </div>
        </section>
    );
}
