import { useContext, useState } from 'react';
import { Link } from 'react-router-dom';

import { PATH } from '../../../../core/environments/costants';
import styles from './Comment.module.css';
import AuthContext from '../../../../contexts/authContext';
import dateFormater from '../../../../utils/dateFormatter';

import DeleteComment from './DeleteComment/DeleteComment';
import EditComment from './EditComment/EditComment';

export default function Comment({
    comment,
    deleteCommentHandler,
    editCommentHandler,
}) {
    const [showDeleteModal, setShowDeleteModal] = useState(false);

    const [editCommentField, setEditCommentField] = useState(false);

    const { userId } = useContext(AuthContext);

    const onDeleteButtonClick = () => setShowDeleteModal(true);

    const closeDeleteModal = () => setShowDeleteModal(false);

    const showEditCommentField = () => setEditCommentField(true);

    const hideEditCommentField = () => setEditCommentField(false);

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
            <Link
                className={styles['image-wrapper']}
                to={PATH.userProfile(comment.user.id)}
            >
                <img
                    className={styles['comment-user-img']}
                    src={
                        comment.user?.avatar ||
                        '/images/default-profile-pic.png'
                    }
                    alt="user"
                />
            </Link>
            <div
                className={
                    userId === comment.user.id
                        ? styles['comment-info-owner']
                        : styles['comment-info']
                }
            >
                <div className={styles['comment-header']}>
                    <div className={styles['owner']}>
                        <Link
                            to={PATH.userProfile(comment.user.id)}
                            className={styles['username']}
                        >
                            {comment.user.firstName} {comment.user.lastName}
                        </Link>

                        {userId === comment.user.id && (
                            <div className={styles['buttons']}>
                                <p
                                    onClick={showEditCommentField}
                                    className={styles['edit-button']}
                                >
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
                        {editCommentField ? (
                            <EditComment
                                hideEditCommentField={hideEditCommentField}
                                editCommentHandler={editCommentHandler}
                                comment={comment}
                            />
                        ) : (
                            comment.content
                        )}
                    </div>
                </div>
            </div>
        </>
    );
}
