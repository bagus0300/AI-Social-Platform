import { useContext, useState } from 'react';

import styles from './Comment.module.css';
import AuthContext from '../../../../contexts/authContext';
import dateFormater from '../../../../utils/dateFormatter';

import DeleteComment from './DeleteComment/DeleteComment';

export default function Comment({ comment, deleteCommentHandler }) {
    const [showDeleteModal, setShowDeleteModal] = useState(false);

    const { userId } = useContext(AuthContext);

    const onDeleteButtonClick = () => setShowDeleteModal(true);

    const closeDeleteModal = () => setShowDeleteModal(false);

    return (
        <>
            {showDeleteModal && (
                <DeleteComment
                    onClose={closeDeleteModal}
                    deleteCommentHandler={deleteCommentHandler}
                    comment={comment}
                    closeDeleteModal={closeDeleteModal}
                />
            )}
            <img
                className={styles['comment-user-img']}
                src={comment.user?.avatar || '/images/default-profile-pic.png'}
                alt="user"
            />
            <div
                className={
                    userId === comment.user.id
                        ? styles['comment-info-owner']
                        : styles['comment-info']
                }
            >
                <div className={styles['comment-header']}>
                    <div className={styles['owner']}>
                        <p className={styles['username']}>
                            {comment.user.firstName} {comment.user.lastName}
                        </p>

                        {userId === comment.user.id && (
                            <div className={styles['buttons']}>
                                <p className={styles['edit-button']}>
                                    <i className="fa-solid fa-pen-to-square"></i>
                                </p>
                                <p className={styles['delete-button']}>
                                    <i
                                        onClick={onDeleteButtonClick}
                                        className="fa-solid fa-trash-can"
                                    ></i>
                                </p>
                            </div>
                        )}
                    </div>
                    <p className={styles['posted-on']}>
                        Posted on: {dateFormater(comment.dateCreated)}
                    </p>
                    <div className={styles['description']}>
                        {comment.content}
                    </div>
                </div>
            </div>
        </>
    );
}
