import { useFormik } from 'formik';

import * as openAiService from '../../core/services/openAiService';

import {
    TranslatorFormKeys,
    languages,
} from '../../core/environments/costants';
import styles from './Translator.module.css';
import { useState } from 'react';

const initialValues = {
    [TranslatorFormKeys.SelectInputLanguage]: 'English',
    [TranslatorFormKeys.SelectTargetLanguage]: 'Bulgarian',
    [TranslatorFormKeys.InputLanguageArea]: '',
    [TranslatorFormKeys.TargetLanguageArea]: '',
};

export default function Translator() {
    const [isLoading, setIsLoading] = useState(false);

    const [haveError, setHaveError] = useState(false);

    const { values, handleChange, handleSubmit, setValues } = useFormik({
        initialValues,
        onSubmit,
    });

    async function onSubmit(values) {
        try {
            setHaveError(false);

            setIsLoading(true);

            const data = {
                inputLanguage: values[TranslatorFormKeys.SelectInputLanguage],
                inputToTranslate: values[TranslatorFormKeys.InputLanguageArea],
                targetLanguage: values[TranslatorFormKeys.SelectTargetLanguage],
            };

            const result = await openAiService.translateWithAi(data);

            setIsLoading(false);

            values[TranslatorFormKeys.TargetLanguageArea] =
                result.translatedText;
        } catch (error) {
            setHaveError(true);
            setIsLoading(false);
        }
    }

    const switchLanguages = () => {
        setValues((val) => ({
            [TranslatorFormKeys.SelectInputLanguage]:
                val[TranslatorFormKeys.SelectTargetLanguage],
            [TranslatorFormKeys.SelectTargetLanguage]:
                val[TranslatorFormKeys.SelectInputLanguage],
            [TranslatorFormKeys.InputLanguageArea]:
                val[TranslatorFormKeys.InputLanguageArea],
            [TranslatorFormKeys.TargetLanguageArea]:
                val[TranslatorFormKeys.TargetLanguageArea],
        }));
    };

    return (
        <section className={styles['translator-section']}>
            <h3 className={styles['translator-section-heading']}>
                Translate with AI
            </h3>
            <form className={styles['translator-form']} onSubmit={handleSubmit}>
                <div className={styles['select-options']}>
                    <select
                        className={styles['select-input-language']}
                        name={TranslatorFormKeys.SelectInputLanguage}
                        id={TranslatorFormKeys.SelectInputLanguage}
                        onChange={handleChange}
                        value={values[TranslatorFormKeys.SelectInputLanguage]}
                    >
                        {languages
                            .sort((a, b) => a.localeCompare(b))
                            .map((language) => (
                                <option key={language}>{language}</option>
                            ))}
                    </select>

                    <p
                        onClick={switchLanguages}
                        className={styles['switch-button']}
                    >
                        <i className="fa-solid fa-repeat"></i>
                    </p>

                    <select
                        className={styles['select-target-language']}
                        name={TranslatorFormKeys.SelectTargetLanguage}
                        id={TranslatorFormKeys.SelectTargetLanguage}
                        onChange={handleChange}
                        value={values[TranslatorFormKeys.SelectTargetLanguage]}
                    >
                        {languages
                            .sort((a, b) => a.localeCompare(b))
                            .map((language) => (
                                <option key={language}>{language}</option>
                            ))}
                    </select>
                </div>
                <div className={styles['areas-and-button-wrapper']}>
                    <div className={styles['input-language-wrapper']}>
                        <label
                            htmlFor={TranslatorFormKeys.InputLanguageArea}
                        ></label>
                        <textarea
                            className={styles['input-language-area']}
                            cols="40"
                            rows="10"
                            type="text"
                            name={TranslatorFormKeys.InputLanguageArea}
                            id={TranslatorFormKeys.InputLanguageArea}
                            onChange={handleChange}
                            value={values[TranslatorFormKeys.InputLanguageArea]}
                        ></textarea>
                    </div>
                    <div className={styles['target-language-wrapper']}>
                        <label
                            htmlFor={TranslatorFormKeys.TargetLanguageArea}
                        ></label>
                        <textarea
                            className={styles['target-language-area']}
                            cols="40"
                            rows="10"
                            type="text"
                            disabled
                            name={TranslatorFormKeys.TargetLanguageArea}
                            id={TranslatorFormKeys.TargetLanguageArea}
                            onChange={handleChange}
                            value={
                                values[TranslatorFormKeys.TargetLanguageArea]
                            }
                        ></textarea>
                    </div>

                    <button
                        disabled={
                            values[TranslatorFormKeys.InputLanguageArea]
                                .length < 1
                        }
                        className={
                            values[TranslatorFormKeys.InputLanguageArea]
                                .length < 1
                                ? styles['translate-button-disabled']
                                : styles['translate-button']
                        }
                        type="submit"
                    >
                        {!isLoading ? 'Translate' : 'In process...'}
                    </button>
                    {haveError && (
                        <p className={styles['error-text']}>
                            Something went wrong please try agin later.
                        </p>
                    )}
                </div>
            </form>
        </section>
    );
}
