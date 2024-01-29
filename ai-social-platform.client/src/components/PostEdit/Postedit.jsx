import { useContext, useEffect, useState } from 'react';

import './Postedit.css';
import * as postService from '../../core/services/postService';
import * as mediaService from '../../core/services/mediaService';
import { Link, useNavigate, useParams } from 'react-router-dom';
import { useFormik, Field } from 'formik';
import { EditPostFormKeys, PATH } from '../../core/environments/costants';

export default function Postedit() {
    const { postId } = useParams();
    const [postData, setPostData] = useState({});
    const [mediaData, setMediaData] = useState([]);
    const [error, setError] = useState(null);
    const [textareaRows, setTextareaRows] = useState(2);
    const [showConfirmation, setShowConfirmation] = useState(false);
    const [mediaIdToDelete, setMediaIdToDelete] = useState(null);
    const navigate = useNavigate();
    useEffect(() => {
        Promise.all([
            postService.getPostById(postId),
            mediaService.getMediaByPostId(postId),
        ])
            .then(([postResult, mediaResult]) => {
                setPostData(postResult);
                setMediaData(mediaResult);
            })
            .catch((error) => setError(error));
    }, []);

    if (!postData) {
        return <div>Loading...</div>;
    }

    const initialValues = {
        [EditPostFormKeys.PostDescription]: postData.content,
        [EditPostFormKeys.TopicId]: postData.topic,
        [EditPostFormKeys.PostPicture]: '',
        [EditPostFormKeys.ChangePostPicture]: '',
    };

    const {
        values,
        errors,
        isSubmitting,
        touched,
        handleChange,
        handleBlur,
        handleSubmit,
    } = useFormik({
        initialValues,
        enableReinitialize: true,
        onSubmit,
    });
    const incrementTextareaRows = () => setTextareaRows(7);

    async function onSubmit(values) {
        const requestBody = {
            content: values[EditPostFormKeys.PostDescription],
            // topicId: values[EditPostFormKeys.TopicId],
        };

        try {
            await postService.editPost(postId, requestBody);
        } catch (error) {
            console.log('Error:', error);
        }
        // debugger;
        // const formData = new FormData();
        // const fileInput = document.getElementById(EditPostFormKeys.PostPicture);

        // const selectedFile = fileInput.files[0];
        // formData.append('files', selectedFile);
        // try {
        //     await mediaService.addMedia(formData);
        // } catch (error) {
        //     console.log('Error:', error);
        // }

        navigate(PATH.postlist);
    }

    async function handleSubmitText(values) {
        const requestBody = {
            content: values[EditPostFormKeys.PostDescription],
            //topicId: values[EditPostFormKeys.TopicId],
        };
        console.log('requestBody', requestBody);
        try {
            await postService.editPost(postId, requestBody);
        } catch (error) {
            console.log('Error:', error);
        }
    }
    const handleRemoveImage = async (mediaId) => {
        const shouldDelete = window.confirm(
            'Are you sure you want to delete this image?'
        );
        if (shouldDelete) {
            try {
                await mediaService.deleteMedia(mediaId);
            } catch (error) {
                setError(error.message);
            }
        }
    };

    const handleChangeImage = async (mediaId) => {
        const formData = new FormData();
        const fileInput = document.getElementById(
            EditPostFormKeys.ChangePostPicture + mediaId
        );
        console.log(fileInput);
        const selectedFile = fileInput.files[0];
        formData.append('DataFile', selectedFile);
        try {
            await mediaService.editMedia(mediaId, formData);
        } catch (error) {
            console.log('Error:', error);
        }
    };

    return (
        <div className="user-profile">
            <article className="post-item-list">
                <h2 className="section-heading">Edit Post</h2>
                <form className="create-form">
                    <textarea
                        className="post-description"
                        name={EditPostFormKeys.PostDescription}
                        id={EditPostFormKeys.PostDescription}
                        onFocus={incrementTextareaRows}
                        rows={textareaRows}
                        onChange={handleChange}
                        value={values[EditPostFormKeys.PostDescription]}
                    ></textarea>
                    <div className="parent-button">
                        <button
                            className="profile-button"
                            disabled={isSubmitting}
                            onClick={() => handleSubmitText(values)}
                        >
                            Save text
                        </button>
                    </div>

                    {/* <label
                        htmlFor={EditPostFormKeys.PostPicture}
                        className="section-heading"
                    >
                        Add image{' '}
                    </label> */}

                    {/* <input
                        type="file"
                        className="userprofile-input"
                        id={EditPostFormKeys.PostPicture}
                        name={EditPostFormKeys.PostPicture}
                        placeholder="Upload a photo..."
                        onChange={handleChange}
                        onBlur={handleBlur}
                    /> */}
                    {mediaData.map((media) => (
                        <>
                            <li className="userprofile-li" key={media.fileId}>
                                <img
                                    className="media-img"
                                    src={media.url}
                                    alt="Post pic"
                                />
                            </li>
                            <div className="user-info-wrapper">
                                <label
                                    // htmlFor={`${EditPostFormKeys.ChangePostPicture}${media.fileId}`}
                                    htmlFor={EditPostFormKeys.ChangePostPicture}
                                    className="change-image"
                                >
                                    Change image
                                </label>

                                <input
                                    type="file"
                                    className="userprofile-input"
                                    id={`${EditPostFormKeys.ChangePostPicture}${media.fileId}`}
                                    name={EditPostFormKeys.ChangePostPicture}
                                    placeholder="Upload a photo..."
                                    onBlur={handleBlur}
                                />
                                <button
                                    onClick={() =>
                                        handleChangeImage(media.fileId)
                                    }
                                >
                                    Change
                                </button>
                            </div>
                            <div className="parent-button">
                                <button
                                    onClick={() =>
                                        handleRemoveImage(media.fileId)
                                    }
                                >
                                    Remove image
                                </button>
                            </div>
                        </>
                    ))}
                    <div className="parent-button">
                        <button
                            className="profile-button"
                            type="submit"
                            disabled={isSubmitting}
                            onClick={handleSubmit}
                        >
                            Submit
                        </button>
                    </div>
                </form>
            </article>
        </div>
    );
}
