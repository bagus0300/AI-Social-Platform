import { useState } from 'react';
import { Link } from 'react-router-dom';
import { useFormik } from 'formik';

import { GeneratePhotoFormKeys } from '../../core/environments/costants';
import styles from './AiGeneratePhoto.module.css';
import * as openAiService from '../../core/services/openAiService';

import PaginationSpinner from '../PaginationSpinner/PaginationSpinner';

const initialValues = {
    [GeneratePhotoFormKeys.PhotoDescription]: '',
};

export default function AiGeneratePhoto({ closeGenerateImageSection }) {
    const [generatedPhoto, setGeneratedPhoto] = useState();

    const [isLoading, setIsLoading] = useState(false);

    const { values, isSubmitting, handleChange, handleSubmit } = useFormik({
        initialValues,
        onSubmit,
    });

    async function onSubmit(values) {
        try {
            setGeneratedPhoto('');

            setIsLoading(true);

            const result = await openAiService.generatePhoto(
                values[GeneratePhotoFormKeys.PhotoDescription]
            );

            setGeneratedPhoto(result.imageUrl);

            console.log(result.imageUrl);

            setIsLoading(false);
        } catch (error) {
            console.log(error);
            setIsLoading(false);
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

                {generatedPhoto && (
                    <>
                        <div className={styles['generated-photo-wrapper']}>
                            <img
                                className={styles['generated-img']}
                                src={generatedPhoto}
                                alt="generated photo"
                            />
                        </div>
                        <Link
                            className={styles['view-button']}
                            to={generatedPhoto}
                            target="_blank"
                        >
                            View in full size
                        </Link>
                    </>
                )}
            </section>
        </>
    );
}
