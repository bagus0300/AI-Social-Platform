import { useContext, useEffect, useReducer, useRef, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useFormik } from 'formik';

import * as mediaService from '../../../core/services/mediaService';
import * as commentService from '../../../core/services/commentService';
import * as likeService from '../../../core/services/likeService';

import { CommentFormKeys, PATH } from '../../../core/environments/costants';
import { CommentActions } from '../../../core/environments/costants';
import styles from './PostItem.module.css';
import likeReducer from '../../../reducers/likeReducer';
import commentReducer from '../../../reducers/commentReducer';
import AuthContext from '../../../contexts/authContext';

// import DeletePost from '../../DeletePost/DeletePost';
// import EditComment from './Comment/EditComment/EditComment';
import Comment from './Comment/Comment';
import UserInfo from './UserInfo/UserInfo';
import Like from './Like/Like';

const initialValues = {
    [CommentFormKeys.CommentText]: '',
};

export default function PostItem({ post }) {
    const [media, setMedia] = useState([]);

    const [commentsCount, setCommentsCount] = useState(post.commentsCount);

    const [editMenu, setEditMenu] = useState(false);

    const [isPostLiked, setIsPostLiked] = useState(post.isLiked);

    const [likes, dispatchLike] = useReducer(likeReducer, []);

    // const [deleteModal, setDeleteModal] = useState(false);

    const [comments, dispatchComment] = useReducer(commentReducer, []);

    const inputRef = useRef(null);

    const mediaSectionRef = useRef(null);

    const { avatar } = useContext(AuthContext);

    const navigate = useNavigate();

    const { values, isSubmitting, handleChange, resetForm, handleSubmit } =
        useFormik({
            initialValues,
            onSubmit,
        });

    useEffect(() => {
        mediaService
            .getPostMedia(post.id)
            .then((result) => {
                setMedia(result);
            })
            .catch((error) => console.log(error));

        commentService
            .getAllComments(post.id, 0)
            .then((result) => {
                dispatchComment({
                    type: CommentActions.GetAllComments,
                    payload: result.comments,
                });
            })
            .catch((error) => console.log(error));
    }, []);

    useEffect(() => {
        likeService
            .getLikes(post.id)
            .then((result) =>
                dispatchLike({
                    type: 'getLikes',
                    payload: result,
                })
            )
            .catch((error) => console.log(error));
    }, [likes.length]);

    const focusInput = () => {
        if (inputRef.current && mediaSectionRef.current) {
            mediaSectionRef.current.scrollIntoView();
            inputRef.current.focus();
        }
    };

    const openPostDetails = () => navigate(PATH.postDetails(post.id));
    // const showEditMenuToggle = () => setEditMenu(!editMenu);

    const closeEditMenu = () => setEditMenu(false);

    const onLikeButtonClickHandler = async () => {
        if (!isPostLiked) {
            const newLike = await likeService.addLike(post.id);

            dispatchLike({
                type: 'addLike',
                payload: newLike,
            });

            setIsPostLiked(true);
        }
    };

    // const showDeleteModal = () => {
    //     setEditMenu(false);
    //     setDeleteModal(true);
    // };

    // const closeDeleteModal = () => setDeleteModal(false);

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

            setCommentsCount((state) => state + 1);

            resetForm();
        } catch (error) {
            console.log(error);
            resetForm();
        }
    }

    function editCommentHandler(editedComment) {
        dispatchComment({
            type: CommentActions.EditComment,
            payload: editedComment,
        });
    }

    function deleteCommentHandler(comment) {
        dispatchComment({
            type: CommentActions.DeleteComment,
            payload: comment,
        });

        setCommentsCount((state) => state - 1);
    }

    return (
        <article className={styles['post-item']}>
            {/* {editMenu && (
                <div
                    onClick={closeEditMenu}
                    className={styles['backdrop']}
                ></div>
            )} */}

            {/* {deleteModal && (
                <DeletePost
                    closeDeleteModal={closeDeleteModal}
                    postId={post.id}
                />
            )} */}

            <UserInfo post={post} />

            {/* {editMenu && ( */}
            {/* <section className={styles['edit-menu']}> */}
            {/* <div className={styles['edit-post']}> */}
            {/* <i className="fa-solid fa-pen-to-square"></i> */}
            {/* <p>Edit Post</p> */}
            {/* </div> */}
            {/* <div */}
            {/* onClick={showDeleteModal} */}
            {/* className={styles['delete-post']} */}
            {/* > */}
            {/* <i className="fa-solid fa-trash-can"></i> */}
            {/* <p>Delete Post</p> */}
            {/* </div> */}
            {/* </section> */}
            {/* // )} */}
            <section
                ref={mediaSectionRef}
                className={styles['content-description']}
            >
                <p>{post.content}</p>
            </section>
            {media.length === 1 && (
                <section
                    onClick={openPostDetails}
                    ref={mediaSectionRef}
                    className={styles['media']}
                >
                    <img src={media[0].url} alt="img" />
                </section>
            )}

            {media.length === 2 && (
                <section
                    onClick={openPostDetails}
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
                    onClick={openPostDetails}
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
                    onClick={openPostDetails}
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
                    <div className={styles['media-backdrop']}>
                        <p>View all {media.length} photos</p>
                    </div>
                </section>
            )}
            <section className={styles['likes']}>
                <div className={styles['likes-count']}>
                    <i className="fa-solid fa-thumbs-up"></i>
                    <p>{likes.length}</p>
                </div>
                <p
                    onClick={openPostDetails}
                    className={styles['comments-count']}
                >
                    {commentsCount} comments
                </p>
            </section>
            <section className={styles['buttons']}>
                <Like
                    onLikeButtonClickHandler={onLikeButtonClickHandler}
                    isPostLiked={isPostLiked}
                />
                <div
                    onClick={focusInput}
                    className={styles['comment-button-wrapper']}
                >
                    <i className="fa-solid fa-comment"></i>
                    <p className={styles['comment-button']}>Comment</p>
                </div>
            </section>
            <section className={styles['comments']}>
                {comments
                    .sort(
                        (a, b) =>
                            new Date(a.dateCreated) - new Date(b.dateCreated)
                    )
                    .map((comment) => (
                        <li className={styles['comment']} key={comment.id}>
                            <Comment
                                comment={comment}
                                deleteCommentHandler={deleteCommentHandler}
                                editCommentHandler={editCommentHandler}
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
