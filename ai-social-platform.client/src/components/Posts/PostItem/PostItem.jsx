import { useContext, useEffect, useReducer, useRef, useState } from 'react';
import { Link } from 'react-router-dom';
import { useFormik } from 'formik';

import * as mediaService from '../../../core/services/mediaService';
import * as commentService from '../../../core/services/commentService';

import { CommentFormKeys, PATH } from '../../../core/environments/costants';
import { CommentActions } from '../../../core/environments/costants';
import styles from './PostItem.module.css';
import commentReducer from '../../../reducers/commentReducer';
import dateFormater from '../../../utils/dateFormatter';
import AuthContext from '../../../contexts/authContext';

import Comment from './Comment/Comment';

const initialValues = {
    [CommentFormKeys.CommentText]: '',
};

export default function PostItem({ post }) {
    const [media, setMedia] = useState([]);

    const [comments, dispatchComment] = useReducer(commentReducer, []);

    const inputRef = useRef(null);

    const mediaSectionRef = useRef(null);

    const { avatar, userId } = useContext(AuthContext);

    const { values, isSubmitting, handleChange, resetForm, handleSubmit } =
        useFormik({
            initialValues,
            onSubmit,
        });

    useEffect(() => {
        commentService.getAllComments(post.id).then((result) => {
            dispatchComment({
                type: CommentActions.GetAllComments,
                payload: result.comments,
            });
        });
    }, []);

    useEffect(() => {
        mediaService
            .getPostMedia(post.id)
            .then((result) => {
                setMedia(result);
            })
            .catch((error) => console.log(error));
    }, []);

    const focusInput = () => {
        if (inputRef.current && mediaSectionRef.current) {
            mediaSectionRef.current.scrollIntoView();
            inputRef.current.focus();
        }
    };

    async function onSubmit(values) {
        try {
            const newComment = await commentService.createComment({
                content: values[CommentFormKeys.CommentText],
                publicationId: post.id,
            });

            dispatchComment({
                type: CommentActions.CreateComment,
                payload: newComment,
            });

            resetForm();
        } catch (error) {
            console.log(error);
            resetForm();
        }
    }

    function deleteCommentHandler(comment) {
        dispatchComment({
            type: CommentActions.DeleteComment,
            payload: comment,
        });
    }

    return (
        <article className={styles['post-item']}>
            <section className={styles['user-info']}>
                <div className={styles['user-info-wrapper']}>
                    <Link to={PATH.userProfile(userId)}>
                        <img
                            className={styles['user-img']}
                            src={
                                post.author.profilePictureBase64 ||
                                '/images/default-profile-pic.png'
                            }
                            alt="User profile pic"
                        />
                    </Link>
                    <div className={styles['post-info']}>
                        <p className={styles['username-wrapper']}>
                            <Link
                                to={PATH.userProfile(userId)}
                                className={styles['username']}
                            >
                                {post.author.firstName} {post.author.lastName}
                            </Link>
                        </p>
                        <p className={styles['posted-on']}>
                            Posted on: {dateFormater(post.dateCreated)}
                        </p>
                    </div>
                </div>
                {post.authorId === userId && (
                    <div className={styles['edit']}>
                        <i className="fa-solid fa-pen-to-square"></i>
                    </div>
                )}
            </section>
            <section className={styles['content-description']}>
                <p>{post.content}</p>
            </section>
            {media.length === 1 && (
                <section ref={mediaSectionRef} className={styles['media']}>
                    <img src={media[0].url} alt="img" />
                </section>
            )}

            {media.length === 2 && (
                <section
                    ref={mediaSectionRef}
                    className={styles['with-two-files']}
                >
                    <ul>
                        {media.slice(0, 3).map((file) => (
                            <li key={file.fileId}>
                                <img src={file.url} alt="img" />
                            </li>
                        ))}
                    </ul>
                </section>
            )}

            {media.length === 3 && (
                <section
                    ref={mediaSectionRef}
                    className={styles['multi-media']}
                >
                    <ul className={styles['files']}>
                        {media.slice(0, 3).map((file) => (
                            <li key={file.fileId} className={styles['file']}>
                                <img src={file.url} alt="img" />
                            </li>
                        ))}
                    </ul>
                </section>
            )}

            {media.length > 3 && (
                <section
                    ref={mediaSectionRef}
                    className={styles['multi-media']}
                >
                    <ul className={styles['files']}>
                        {media.slice(0, 3).map((file) => (
                            <li key={file.fileId} className={styles['file']}>
                                <img src={file.url} alt="img" />
                            </li>
                        ))}
                    </ul>
                    <div className={styles['backdrop']}>
                        <p>View all {media.length} phots</p>
                    </div>
                </section>
            )}
            <section className={styles['likes']}>
                <div className={styles['likes-count']}>
                    <i className="fa-solid fa-thumbs-up"></i>
                    <p>0</p>
                </div>
                <p className={styles['comments-count']}>
                    {comments.length} comments
                </p>
            </section>
            <section className={styles['buttons']}>
                <div className={styles['like-button']}>
                    <i className="fa-solid fa-thumbs-up"></i>
                    <p>Like</p>
                </div>
                <div
                    onClick={focusInput}
                    className={styles['comment-button-wrapper']}
                >
                    <i className="fa-solid fa-comment"></i>
                    <p className={styles['comment-button']}>Comment</p>
                </div>
            </section>
            <section className={styles['comments']}>
                {comments.map((comment) => (
                    <li className={styles['comment']} key={comment.id}>
                        <Comment
                            comment={comment}
                            deleteCommentHandler={deleteCommentHandler}
                        />
                    </li>
                ))}
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
                            ref={inputRef}
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
        </article>
    );
}
