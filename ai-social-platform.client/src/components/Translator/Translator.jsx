import { useFormik } from 'formik';

import * as openAiService from '../../core/services/openAiService';

import {
    TranslatorFormKeys,
    languages,
} from '../../core/environments/costants';
import styles from './Translator.module.css';

const initialValues = {
    [TranslatorFormKeys.selectInputLanguage]: 'English',
    [TranslatorFormKeys.selectTargetLanguage]: 'Bulgarian',
    [TranslatorFormKeys.inputLanguageArea]: '',
    [TranslatorFormKeys.targetLanguageArea]: '',
};

export default function Translator() {
    const { values, handleChange, handleSubmit, setValues } = useFormik({
        initialValues,
        onSubmit,
    });

    async function onSubmit(values) {
        const data = {
            inputLanguage: values[TranslatorFormKeys.selectInputLanguage],
            inputToTranslate: values[TranslatorFormKeys.inputLanguageArea],
            targetLanguage: values[TranslatorFormKeys.selectTargetLanguage],
        };

        const result = await openAiService.translateWithAi(data);

        values[TranslatorFormKeys.targetLanguageArea] = result.translatedText;
    }

    const switchLanguages = () => {
        setValues((val) => ({
            [TranslatorFormKeys.selectInputLanguage]:
                val[TranslatorFormKeys.selectTargetLanguage],
            [TranslatorFormKeys.selectTargetLanguage]:
                val[TranslatorFormKeys.selectInputLanguage],
            [TranslatorFormKeys.inputLanguageArea]:
                val[TranslatorFormKeys.inputLanguageArea],
            [TranslatorFormKeys.targetLanguageArea]:
                val[TranslatorFormKeys.targetLanguageArea],
        }));
    };

    return (
        <section className={styles['translator-section']}>
            <h4 className={styles['translator-section-heading']}>
                Translate with AI
            </h4>
            <form className={styles['translator-form']} onSubmit={handleSubmit}>
                <div className={styles['select-options']}>
                    <select
                        className={styles['select-input-language']}
                        name={TranslatorFormKeys.selectInputLanguage}
                        id={TranslatorFormKeys.selectInputLanguage}
                        onChange={handleChange}
                        value={values[TranslatorFormKeys.selectInputLanguage]}
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
                        name={TranslatorFormKeys.selectTargetLanguage}
                        id={TranslatorFormKeys.selectTargetLanguage}
                        onChange={handleChange}
                        value={values[TranslatorFormKeys.selectTargetLanguage]}
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
                            htmlFor={TranslatorFormKeys.inputLanguageArea}
                        ></label>
                        <textarea
                            className={styles['input-language-area']}
                            cols="40"
                            rows="10"
                            type="text"
                            name={TranslatorFormKeys.inputLanguageArea}
                            id={TranslatorFormKeys.inputLanguageArea}
                            onChange={handleChange}
                            value={values[TranslatorFormKeys.inputLanguageArea]}
                        ></textarea>
                    </div>
                    <div className={styles['target-language-wrapper']}>
                        <label
                            htmlFor={TranslatorFormKeys.targetLanguageArea}
                        ></label>
                        <textarea
                            className={styles['target-language-area']}
                            cols="40"
                            rows="10"
                            type="text"
                            disabled
                            name={TranslatorFormKeys.targetLanguageArea}
                            id={TranslatorFormKeys.targetLanguageArea}
                            onChange={handleChange}
                            value={
                                values[TranslatorFormKeys.targetLanguageArea]
                            }
                        ></textarea>
                    </div>

                    <button
                        disabled={
                            values[TranslatorFormKeys.inputLanguageArea]
                                .length < 1
                        }
                        className={
                            values[TranslatorFormKeys.inputLanguageArea]
                                .length < 1
                                ? styles['translate-button-disabled']
                                : styles['translate-button']
                        }
                        type="submit"
                    >
                        Translate
                    </button>
                </div>
            </form>
        </section>
    );
}
