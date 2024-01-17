import { useFormik } from 'formik';

import { EditCommentFromKeys } from '../../../../../core/environments/costants';
import * as commentService from '../../../../../core/services/commentService';
import styles from './EditComment.module.css';

export default function EditComment({
    hideEditCommentField,
    editCommentHandler,
    comment,
}) {
    const { values, isSubmitting, handleChange, handleSubmit } = useFormik({
        initialValues: {
            [EditCommentFromKeys.EditCommentArea]: comment.content,
        },
        onSubmit,
        enableReinitialize: true,
    });

    async function onSubmit(values) {
        try {
            const editedComment = await commentService.editComment(
                comment.id,
                values[EditCommentFromKeys.EditCommentArea]
            );

            editCommentHandler(editedComment);

            hideEditCommentField();
        } catch (error) {
            console.log(error);
        }
    }

    return (
        <section className={styles['edit-comment-section']}>
            <form
                onSubmit={handleSubmit}
                className={styles['edit-comment-form']}
            >
                <textarea
                    className={
                        values[EditCommentFromKeys.EditCommentArea].length === 0
                            ? styles['edit-comment-area-error']
                            : styles['edit-comment-area']
                    }
                    name={EditCommentFromKeys.EditCommentArea}
                    id={EditCommentFromKeys.EditCommentArea}
                    placeholder={
                        values[EditCommentFromKeys.EditCommentArea].length === 0
                            ? "You can't send an empty comment"
                            : ''
                    }
                    cols="50"
                    rows="5"
                    onChange={handleChange}
                    value={values[EditCommentFromKeys.EditCommentArea]}
                ></textarea>
                <div className={styles['edit-comment-buttons']}>
                    <button
                        className={styles['cancel-button']}
                        onClick={hideEditCommentField}
                    >
                        Cancel
                    </button>
                    <button
                        disabled={
                            isSubmitting ||
                            values[EditCommentFromKeys.EditCommentArea]
                                .length === 0
                        }
                        className={styles['submit-button']}
                        type="submit"
                    >
                        Save
                    </button>
                </div>
            </form>
        </section>
    );
}
