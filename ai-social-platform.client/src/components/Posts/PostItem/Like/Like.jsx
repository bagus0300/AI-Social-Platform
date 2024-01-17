import styles from './Like.module.css';

export default function Like({ isPostLiked, onLikeButtonClickHandler }) {
    return (
        <div
            onClick={onLikeButtonClickHandler}
            className={
                isPostLiked
                    ? styles['like-button-liked']
                    : styles['like-button']
            }
        >
            <i className="fa-solid fa-thumbs-up"></i>
            <p>Like</p>
        </div>
    );
}
