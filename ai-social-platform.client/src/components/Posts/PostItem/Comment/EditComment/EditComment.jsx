import { useFormik } from 'formik';

import { EditCommentFromKeys } from '../../../../../core/environments/costants';
import * as commentService from '../../../../../core/services/commentService';
import styles from './EditComment.module.css';

// TODO: Add validation

export default function EditComment({
    hideEditCommentField,
    editCommentHandler,
    comment,
}) {
    const { values, errors, isSubmitting, handleChange, handleSubmit } =
        useFormik({
            initialValues: {
                [EditCommentFromKeys.EditCommentArea]: comment.content,
            },
            onSubmit,
            enableReinitialize: true,
        });

    async function onSubmit(values) {
        // TODO: use editCommentHandler with editedComment
        try {
            const editedComment = await commentService.editComment(
                comment.id,
                values[EditCommentFromKeys.EditCommentArea]
            );

            console.log(editedComment);
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
                    className={styles['edit-comment-area']}
                    name={EditCommentFromKeys.EditCommentArea}
                    id={EditCommentFromKeys.EditCommentArea}
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
                        disabled={isSubmitting}
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
