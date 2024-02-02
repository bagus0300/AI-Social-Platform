import { useState } from 'react';
import { useFormik } from 'formik';

import {
    ContentType,
    GeneratePhotoFormKeys,
    GeneratedImageName,
} from '../../core/environments/costants';
import styles from './AiGeneratePhoto.module.css';
import * as openAiService from '../../core/services/openAiService';

import PaginationSpinner from '../PaginationSpinner/PaginationSpinner';
import { convertBase64ToFileObject } from '../../utils/convertBase64ToFileObject';

const initialValues = {
    [GeneratePhotoFormKeys.PhotoDescription]: '',
};

export default function AiGeneratePhoto({
    closeGenerateImageSection,
    addGeneratedPhoto,
}) {
    const [generatedPhoto, setGeneratedPhoto] = useState();

    const [isLoading, setIsLoading] = useState(false);

    const [generatedFile, setGeneratedFile] = useState();

    const [haveError, setHaveError] = useState(false);

    const { values, isSubmitting, handleChange, handleSubmit } = useFormik({
        initialValues,
        onSubmit,
    });

    async function onSubmit(values) {
        try {
            setGeneratedPhoto('');

            setHaveError(false);

            setIsLoading(true);

            const result = await openAiService.generatePhotoBase64(
                values[GeneratePhotoFormKeys.PhotoDescription]
            );

            const file = await convertBase64ToFileObject(
                result.imageBase64,
                GeneratedImageName,
                ContentType.ImagePng
            );

            setGeneratedFile(file);

            const fileUrl = URL.createObjectURL(file);

            setGeneratedPhoto(fileUrl);

            setIsLoading(false);
        } catch (error) {
            setIsLoading(false);
            setHaveError(true);
        }
    }

    return (
        <>
            <div
                onClick={closeGenerateImageSection}
                className={styles['backdrop']}
            ></div>
            <section className={styles['generate-photo-section']}>
                <div className={styles['header-wrapper']}>
                    <h2 className={styles['section-heading']}>
                        Generate Photo
                    </h2>
                    <p
                        onClick={closeGenerateImageSection}
                        className={styles['close-button']}
                    >
                        X
                    </p>
                </div>
                <form
                    className={styles['generate-photo-form']}
                    onSubmit={handleSubmit}
                >
                    <label
                        htmlFor={GeneratePhotoFormKeys.PhotoDescription}
                    ></label>
                    <textarea
                        className={styles['photo-description-area']}
                        name={GeneratePhotoFormKeys.PhotoDescription}
                        id={GeneratePhotoFormKeys.PhotoDescription}
                        cols="60"
                        rows="10"
                        placeholder="Describe me what to generate."
                        value={values[GeneratePhotoFormKeys.PhotoDescription]}
                        onChange={handleChange}
                    ></textarea>
                    <button
                        className={
                            values[GeneratePhotoFormKeys.PhotoDescription]
                                .length < 1
                                ? styles['generate-button-disabled']
                                : styles['generate-button']
                        }
                        type="submit"
                        disabled={
                            values[GeneratePhotoFormKeys.PhotoDescription]
                                .length < 1 || isSubmitting
                        }
                    >
                        Generate
                    </button>
                </form>

                {isLoading && <PaginationSpinner />}

                {haveError && (
                    <p className={styles['error-text']}>
                        Something went wrong please try again later.
                    </p>
                )}

                {generatedPhoto && (
                    <>
                        <div className={styles['generated-photo-wrapper']}>
                            <img
                                className={styles['generated-img']}
                                src={generatedPhoto}
                                alt="generated photo"
                            />
                        </div>
                        <p
                            onClick={() => {
                                addGeneratedPhoto(generatedFile);
                            }}
                            className={styles['add-image-button']}
                        >
                            Add to post
                        </p>
                    </>
                )}
            </section>
        </>
    );
}
