import { useState } from 'react';

import styles from './DeleteComment.module.css';
import * as commentService from '../../../../../core/services/commentService';

export default function DeleteComment({
    closeDeleteModal,
    comment,
    deleteCommentHandler,
}) {
    const [isDeleting, setIsDeleting] = useState(false);

    async function deleteComment() {
        try {
            setIsDeleting(true);

            await commentService.deleteComment(comment.id);

            deleteCommentHandler(comment);

            closeDeleteModal();
        } catch (error) {
            console.log(error);
        }
    }

    return (
        <>
            <div
                onClick={closeDeleteModal}
                className={styles['backdrop']}
            ></div>
            <section className={styles['delete-comment']}>
                <p
                    onClick={closeDeleteModal}
                    className={styles['cancel-button']}
                >
                    Cancel
                </p>
                <p onClick={deleteComment} className={styles['delete-button']}>
                    {isDeleting ? 'Deleting...' : 'Delete'}
                </p>
            </section>
        </>
    );
}
