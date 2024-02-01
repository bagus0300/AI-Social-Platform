import { useContext, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { useFormik } from 'formik';
import EmojiPicker from 'emoji-picker-react';

import * as postService from '../../core/services/postService';
import * as mediaService from '../../core/services/mediaService';

import { CreateFormKeys, FILES, PATH } from '../../core/environments/costants';
import styles from './CreatePost.module.css';
import createPostValidation from './createPostValidation';
import AuthContext from '../../contexts/authContext';

import OpenAIForm from '../OpenAI/OpenAi';
import AiGeneratePhoto from '../AiGeneratePhoto/AiGeneratePhoto';

const initialValues = {
    [CreateFormKeys.PostDescription]: '',
    [CreateFormKeys.PostMedia]: '',
};

export default function CreatePost() {
    const { firstName, lastName, avatar } = useContext(AuthContext);

    const [textareaRows, setTextareaRows] = useState(2);

    const [openAiFormVisible, setOpenAiFormVisible] = useState(false);

    const [showEmojiPicker, setShowEmojiPicker] = useState(false);

    const [isGenerateImageSectionVisible, setIsGenerateImageSectionVisible] =
        useState(false);

    const toggleOpenAiForm = () => setOpenAiFormVisible(!openAiFormVisible);

    const toggleEmojiPicker = () => setShowEmojiPicker(!showEmojiPicker);

    const openGenerateImageSection = () =>
        setIsGenerateImageSectionVisible(true);

    const closeGenerateImageSection = () =>
        setIsGenerateImageSectionVisible(false);

    const navigate = useNavigate();

    const {
        values,
        errors,
        isSubmitting,
        resetForm,
        handleChange,
        handleSubmit,
        setFieldValue,
    } = useFormik({
        initialValues,
        onSubmit,
        validationSchema: createPostValidation,
    });

    const onEmojiClick = (emojiObject) => {
        values[CreateFormKeys.PostDescription] = `${
            values[CreateFormKeys.PostDescription]
        }${emojiObject.emoji}`;

        setShowEmojiPicker(false);
    };

    const incrementTextareaRows = () => setTextareaRows(7);

    const closeCreateForm = () => {
        values[CreateFormKeys.PostDescription] = '';
        navigate(PATH.home);
    };

    const updatePostDescription = (generatedText) =>
        (values[CreateFormKeys.PostDescription] = `${
            values[CreateFormKeys.PostDescription]
        } ${generatedText}`);

    async function onSubmit(values) {
        const formData = new FormData();

        if (values[CreateFormKeys.PostMedia].length > 0) {
            for (const file of values[CreateFormKeys.PostMedia]) {
                formData.append('files', file);
            }
        }

        if (
            values[CreateFormKeys.PostDescription].length > 0 &&
            values[CreateFormKeys.PostMedia].length === 0
        ) {
            postService
                .createPost({ content: values[CreateFormKeys.PostDescription] })
                .then(() => {
                    resetForm();
                    navigate(PATH.home);
                })
                .catch((error) => console.log(error));
        }

        if (
            values[CreateFormKeys.PostDescription].length === 0 &&
            values[CreateFormKeys.PostMedia].length > 0
        ) {
            Promise.all([
                postService.createPost({
                    content: '',
                }),
                mediaService.addMedia(formData),
            ])
                .then(() => {
                    resetForm();
                    navigate(PATH.home);
                })
                .catch((error) => console.log(error));
        }

        if (
            values[CreateFormKeys.PostDescription].length > 0 &&
            values[CreateFormKeys.PostMedia].length > 0
        ) {
            Promise.all([
                postService.createPost({
                    content: values[CreateFormKeys.PostDescription],
                }),
                mediaService.addMedia(formData),
            ])
                .then(() => {
                    resetForm();
                    navigate(PATH.home);
                })
                .catch((error) => console.log(error));
        }
    }

    return (
        <>
            {openAiFormVisible && (
                <OpenAIForm
                    onClose={toggleOpenAiForm}
                    updatePostDescription={updatePostDescription}
                />
            )}

            {isGenerateImageSectionVisible && (
                <AiGeneratePhoto
                    closeGenerateImageSection={closeGenerateImageSection}
                />
            )}

            <div onClick={closeCreateForm} className={styles['backdrop']}></div>
            <section className={styles['create-post-section']}>
                {showEmojiPicker && (
                    <div className={styles['emoji-picker-wrapper']}>
                        <EmojiPicker onEmojiClick={onEmojiClick} />
                    </div>
                )}
                <div className={styles['section-header']}>
                    <h2 className={styles['section-heading']}>Create Post</h2>
                    <p
                        onClick={closeCreateForm}
                        className={styles['close-button']}
                    >
                        X
                    </p>
                </div>
                <div className={styles['user-info']}>
                    <Link className={styles['profile-pic-wrapper']}>
                        <img
                            className={styles['profile-picture']}
                            src={avatar || '/images/default-profile-pic.png'}
                            alt=""
                        />
                    </Link>
                    <p className={styles['user-names']}>
                        {firstName} {lastName}
                    </p>
                </div>
                <div className={styles['openAi']}>
                    <section className={styles['openAi-section']}>
                        <img
                            src={FILES.aiLogo}
                            alt="OpenAI Logo"
                            className={styles['openAi-logo']}
                        />
                        <p className={styles['openAi']}>
                            <strong>Generating text with OpenAI</strong>
                        </p>
                    </section>
                    <button
                        className={styles['openAi-button']}
                        onClick={toggleOpenAiForm}
                    >
                        Try
                    </button>
                </div>
                <div className={styles['openAi']}>
                    <section className={styles['openAi-section']}>
                        <img
                            src={FILES.aiLogo}
                            alt="OpenAI Logo"
                            className={styles['openAi-logo']}
                        />
                        <p className={styles['openAi']}>
                            <strong>Generating image with OpenAI</strong>
                        </p>
                    </section>
                    <button
                        className={styles['openAi-button']}
                        onClick={openGenerateImageSection}
                    >
                        Try
                    </button>
                </div>
                <div className={styles['text-length-error']}>
                    {errors[CreateFormKeys.PostDescription] && (
                        <p>{errors[CreateFormKeys.PostDescription]}</p>
                    )}
                </div>

                <form onSubmit={handleSubmit} className={styles['create-form']}>
                    <label htmlFor={CreateFormKeys.PostDescription}></label>
                    <div className={styles['description-wrapper']}>
                        <textarea
                            className={styles['post-description']}
                            name={CreateFormKeys.PostDescription}
                            id={CreateFormKeys.PostDescription}
                            onFocus={incrementTextareaRows}
                            rows={textareaRows}
                            placeholder={`What's on your mind, ${firstName}?`}
                            onChange={handleChange}
                            value={values[CreateFormKeys.PostDescription]}
                        ></textarea>
                        <div
                            onClick={toggleEmojiPicker}
                            className={styles['emoji-logo-wrapper']}
                        >
                            <i className="fa-solid fa-face-grin"></i>
                        </div>
                    </div>
                    <div className={styles['media-input']}>
                        <label
                            className={styles['media-label']}
                            htmlFor={CreateFormKeys.PostMedia}
                        >
                            Add images{' '}
                            <span className={styles['drag-drop']}>
                                or drag and drop
                            </span>
                        </label>
                        <input
                            className={styles['file-input']}
                            name={CreateFormKeys.PostMedia}
                            id={CreateFormKeys.PostMedia}
                            type="file"
                            multiple
                            onChange={(e) => {
                                setFieldValue(
                                    CreateFormKeys.PostMedia,
                                    e.target.files
                                );
                            }}
                        />
                    </div>
                    <input
                        disabled={
                            errors[CreateFormKeys.PostDescription] ||
                            errors[CreateFormKeys.PostMedia] ||
                            isSubmitting
                        }
                        className={
                            values[CreateFormKeys.PostDescription].length ===
                                0 &&
                            values[CreateFormKeys.PostMedia].length === 0
                                ? styles['submit-button-disabled']
                                : styles['submit-button']
                        }
                        type="submit"
                        value={isSubmitting ? 'Creating...' : 'Create'}
                    />
                </form>
            </section>
        </>
    );
}
