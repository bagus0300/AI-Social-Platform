import { useContext, useEffect, useReducer, useState } from 'react';
import { useParams } from 'react-router-dom';
import { useFormik } from 'formik';

import * as postService from '../../core/services/postService';
import * as mediaService from '../../core/services/mediaService';
import * as commentService from '../../core/services/commentService';

import { CommentFormKeys } from '../../core/environments/costants';
import { CommentActions } from '../../core/environments/costants';
import AuthContext from '../../contexts/authContext';
import commentReducer from '../../reducers/commentReducer';
import styles from './PostDetails.module.css';

import Comment from '../Posts/PostItem/Comment/Comment';
import UserInfo from '../Posts/PostItem/UserInfo/UserInfo';

const initialValues = {
    [CommentFormKeys.CommentText]: '',
};

export default function PostDetails() {
    const [postData, setPostData] = useState({});

    const [commentsCount, setCommentsCount] = useState(0);

    const [commentsLeft, setCommentsLeft] = useState(0);

    const [commentsPage, setCommentsPage] = useState(0);

    const [postMedia, setPostMedia] = useState([]);

    const [comments, dispatchComment] = useReducer(
        commentReducer,
        postData.comments
    );

    const { avatar } = useContext(AuthContext);

    const { postId } = useParams();

    useEffect(() => {
        postService
            .getPostById(postId)
            .then((result) => {
                setPostData(result);
                setCommentsCount(result.commentsCount);
            })
            .catch((error) => console.log(error));

        mediaService
            .getMediaByPostId(postId)
            .then((result) => setPostMedia(result))
            .catch((error) => console.log(error));

        commentService
            .getAllComments(postId, commentsPage)
            .then((result) => {
                dispatchComment({
                    type: CommentActions.GetAllComments,
                    payload: result.comments,
                });
            })
            .catch((error) => console.log(error));
    }, [postId]);

    useEffect(() => {
        commentService
            .getAllComments(postId, commentsPage)
            .then((result) => {
                dispatchComment({
                    type: CommentActions.GetAllComments,
                    payload: result.comments,
                });
                setCommentsLeft(result.commentsLeft);
            })
            .catch((error) => console.log(error));
    }, [commentsPage]);

    const { values, isSubmitting, handleChange, resetForm, handleSubmit } =
        useFormik({
            initialValues,
            onSubmit: addComment,
        });

    async function addComment(values) {
        try {
            const newComment = await commentService.createComment({
                content: values[CommentFormKeys.CommentText],
                publicationId: postId,
            });

            dispatchComment({
                type: CommentActions.CreateComment,
                payload: newComment,
            });

            setCommentsCount((state) => state + 1);

            resetForm();
        } catch (error) {
            console.log(error);
            resetForm();
        }
    }

    const editCommentHandler = (editedComment) => {
        dispatchComment({
            type: CommentActions.EditComment,
            payload: editedComment,
        });
    };

    const deleteCommentHandler = (comment) => {
        dispatchComment({
            type: CommentActions.DeleteComment,
            payload: comment,
        });

        setCommentsCount((state) => state - 1);
    };

    const loadMoreComments = () => setCommentsPage((state) => state + 1);

    const backPage = () => setCommentsPage((state) => state - 1);

    return (
        <section className={styles['post-details-section']}>
            <UserInfo post={postData} />
            <section className={styles['post-description']}>
                {postData.content}
            </section>
            <section className={styles['media']}>
                {postMedia.map((media) => (
                    <ul key={media.fileId}>
                        <li className={styles['media']}>
                            <img src={media.url} alt="" />
                        </li>
                    </ul>
                ))}
            </section>
            <section className={styles['likes']}>
                <div className={styles['likes-count']}>
                    <i className="fa-solid fa-thumbs-up"></i>
                    <p>0</p>
                </div>
                <p className={styles['comments-count']}>
                    {commentsCount} comments
                </p>
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
            <section className={styles['comments']}>
                {comments
                    ?.sort(
                        (a, b) =>
                            new Date(a.dateCreated) - new Date(b.dateCreated)
                    )
                    ?.map((comment) => (
                        <li className={styles['comment']} key={comment.id}>
                            <Comment
                                comment={comment}
                                deleteCommentHandler={deleteCommentHandler}
                                editCommentHandler={editCommentHandler}
                            />
                        </li>
                    ))}
                <div className={styles['load-more-comments']}>
                    {commentsPage > 0 && <p onClick={backPage}>Back</p>}
                    {commentsLeft !== 0 && (
                        <p onClick={loadMoreComments}>Next</p>
                    )}
                </div>
            </section>
            <section className={styles['add-comment']}>
                <img
                    className={styles['comment-user-img']}
                    src={avatar || '/images/default-profile-pic.png'}
                    alt="user"
                />
                <div className={styles['comment-area']}>
                    <form
                        className={styles['comment-form']}
                        onSubmit={handleSubmit}
                    >
                        <label htmlFor={CommentFormKeys.CommentText}></label>
                        <textarea
                            className={styles['comment-area']}
                            type="text"
                            placeholder="Write a comment..."
                            name={CommentFormKeys.CommentText}
                            id={CommentFormKeys.CommentText}
                            onChange={handleChange}
                            value={values[CommentFormKeys.CommentText]}
                        ></textarea>
                        <button
                            className={
                                values[CommentFormKeys.CommentText].length > 0
                                    ? styles['submit-button']
                                    : styles['submit-button-error']
                            }
                            type="submit"
                            disabled={
                                isSubmitting ||
                                values[CommentFormKeys.CommentText].length === 0
                            }
                        >
                            <i className="fa-solid fa-paper-plane"></i>
                        </button>
                    </form>
                </div>
            </section>
        </section>
    );
}
