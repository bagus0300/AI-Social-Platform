import React, { useState } from 'react';
import BeatLoader from 'react-spinners/BeatLoader';
import styles from './OpenAi.module.css';
import { generateTextWhitOpenAi } from '../../core/services/openAiService';

export default function OpenAIForm({ onClose, updatePostDescription }) {
    const [theme, setTheme] = useState('');
    const [tone, setTone] = useState('');
    const [audienceType, setAudienceType] = useState('');
    const [textLength, setTextLength] = useState('');
    const [haveError, setHaveError] = useState(false);

    const [loading, setLoading] = useState(false);

    const handleSubmit = async (e) => {
        e.preventDefault();

        const toneValue = convertToNumericValue(tone, [
            'Formal',
            'Informal',
            'Technical',
            'Humorous',
        ]);
        const audienceTypeValue = convertToNumericValue(audienceType, [
            'Business',
            'Marketing',
            'Developers',
            'Student',
        ]);
        const textLengthValue = convertToNumericValue(textLength, [
            'Short',
            'Middle',
            'Long',
        ]);

        const data = {
            subject: theme,
            audience: audienceTypeValue,
            tone: toneValue,
            textLength: textLengthValue,
        };

        try {
            setHaveError(false);
            setLoading(true);
            const response = await generateTextWhitOpenAi(data);
            setLoading(false);
            updatePostDescription(response.generatedText);
            onClose(onClose);
        } catch (error) {
            setLoading(false);
            setHaveError(true);
        }
    };

    const convertToNumericValue = (textValue, options) => {
        const index = options.indexOf(textValue);
        return index !== -1 ? index : null;
    };

    return (
        <>
            <div className={styles['overlay']}>
                <div className={styles['backdrop']}></div>
                <div className={styles['modal']}>
                    <div className={styles['openAI-container']}>
                        <header className={styles['headers']}>
                            <h2>OpenAI</h2>
                            {loading && <BeatLoader color="#2d2ae6" />}
                            {haveError && (
                                <p className={styles['error-message']}>
                                    Something went wrong please try again later.
                                </p>
                            )}
                            <button
                                className={styles['btn btn-close']}
                                onClick={onClose}
                            >
                                <i className="fa-solid fa-xmark"></i>
                            </button>
                        </header>
                        <form
                            onSubmit={(e) => {
                                e.preventDefault();
                                handleSubmit(e);
                            }}
                        >
                            <div className={styles['form-group-theme']}>
                                <label
                                    className={styles['labelTheme']}
                                    htmlFor="theme"
                                >
                                    Theme
                                </label>
                                <div className={styles['input-wrapper']}>
                                    <span>
                                        <i
                                            className={
                                                styles['far fa-pen-to-square']
                                            }
                                        ></i>
                                    </span>
                                    <input
                                        id={styles['openAi-theme']}
                                        name="openAi-theme"
                                        type="text"
                                        value={theme}
                                        onChange={(e) =>
                                            setTheme(e.target.value)
                                        }
                                    />
                                </div>
                                <p className={styles['form-error']}>
                                    Min length 10 characters
                                </p>
                            </div>

                            <div className={styles['form-group-radio']}>
                                <label
                                    className={styles['labelName']}
                                    htmlFor="tone"
                                >
                                    Tone
                                </label>
                                <div className={styles['input-wrapper']}>
                                    <div className={styles['radio-box']}>
                                        <input
                                            className={styles['radio-button']}
                                            type="radio"
                                            id="formal"
                                            name="tone"
                                            value="Formal"
                                            checked={tone === 'Formal'}
                                            onChange={() => setTone('Formal')}
                                        />
                                        <label
                                            className={styles['label']}
                                            htmlFor="formal"
                                        >
                                            Formal
                                        </label>
                                    </div>
                                    <div className={styles['radio-box']}>
                                        <input
                                            className={styles['radio-button']}
                                            type="radio"
                                            id="informal"
                                            name="tone"
                                            value="Informal"
                                            checked={tone === 'Informal'}
                                            onChange={() => setTone('Informal')}
                                        />
                                        <label
                                            className={styles['label']}
                                            htmlFor="informal"
                                        >
                                            Informal
                                        </label>
                                    </div>
                                    <div className={styles['radio-box']}>
                                        <input
                                            className={styles['radio-button']}
                                            type="radio"
                                            id="technical"
                                            name="tone"
                                            value="Technical"
                                            checked={tone === 'Technical'}
                                            onChange={() =>
                                                setTone('Technical')
                                            }
                                        />
                                        <label
                                            className={styles['label']}
                                            htmlFor="technical"
                                        >
                                            Technical
                                        </label>
                                    </div>
                                    <div className={styles['radio-box']}>
                                        <input
                                            className={styles['radio-button']}
                                            type="radio"
                                            id="humorous"
                                            name="tone"
                                            value="Humorous"
                                            checked={tone === 'Humorous'}
                                            onChange={() => setTone('Humorous')}
                                        />
                                        <label
                                            className={styles['label']}
                                            htmlFor="humorous"
                                        >
                                            Humorous
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <p className={styles['form-error']}>*required</p>

                            <div className={styles['form-group-radio']}>
                                <label
                                    className={styles['labelName']}
                                    htmlFor="audienceType"
                                >
                                    Audience Type
                                </label>
                                <div className={styles['input-wrapper']}>
                                    <div className={styles['radio-box']}>
                                        <input
                                            className={styles['radio-button']}
                                            type="radio"
                                            id="business"
                                            name="business"
                                            value="Business"
                                            checked={
                                                audienceType === 'Business'
                                            }
                                            onChange={() =>
                                                setAudienceType('Business')
                                            }
                                        />
                                        <label
                                            className={styles['label']}
                                            htmlFor="business"
                                        >
                                            Business
                                        </label>
                                    </div>
                                    <div className={styles['radio-box']}>
                                        <input
                                            className={styles['radio-button']}
                                            type="radio"
                                            id="marketing"
                                            name="marketing"
                                            value="Marketing"
                                            checked={
                                                audienceType === 'Marketing'
                                            }
                                            onChange={() =>
                                                setAudienceType('Marketing')
                                            }
                                        />
                                        <label
                                            className={styles['label']}
                                            htmlFor="marketing"
                                        >
                                            Marketing
                                        </label>
                                    </div>
                                    <div className={styles['radio-box']}>
                                        <input
                                            className={styles['radio-button']}
                                            type="radio"
                                            id="developers"
                                            name="developers"
                                            value="Developers"
                                            checked={
                                                audienceType === 'Developers'
                                            }
                                            onChange={() =>
                                                setAudienceType('Developers')
                                            }
                                        />
                                        <label
                                            className={styles['label']}
                                            htmlFor="developers"
                                        >
                                            Developers
                                        </label>
                                    </div>
                                    <div className={styles['radio-box']}>
                                        <input
                                            className={styles['radio-button']}
                                            type="radio"
                                            id="student"
                                            name="student"
                                            value="Student"
                                            checked={audienceType === 'Student'}
                                            onChange={() =>
                                                setAudienceType('Student')
                                            }
                                        />
                                        <label
                                            className={styles['label']}
                                            htmlFor="student"
                                        >
                                            Student
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <p className={styles['form-error']}>*required</p>

                            <div className={styles['form-group-radio']}>
                                <label
                                    className={styles['labelName']}
                                    htmlFor="textLength"
                                >
                                    Text Length
                                </label>
                                <div className={styles['input-wrapper']}>
                                    <div className={styles['radio-box']}>
                                        <input
                                            className={styles['radio-button']}
                                            type="radio"
                                            id="short"
                                            name="short"
                                            value="Short"
                                            checked={textLength === 'Short'}
                                            onChange={() =>
                                                setTextLength('Short')
                                            }
                                        />
                                        <label
                                            className={styles['label']}
                                            htmlFor="short"
                                        >
                                            Short
                                        </label>
                                    </div>
                                    <div className={styles['radio-box']}>
                                        <input
                                            className={styles['radio-button']}
                                            type="radio"
                                            id="middle"
                                            name="middle"
                                            value="Middle"
                                            checked={textLength === 'Middle'}
                                            onChange={() =>
                                                setTextLength('Middle')
                                            }
                                        />
                                        <label
                                            className={styles['label']}
                                            htmlFor="middle"
                                        >
                                            Middle
                                        </label>
                                    </div>
                                    <div className={styles['radio-box']}>
                                        <input
                                            className={styles['radio-button']}
                                            type="radio"
                                            id="long"
                                            name="long"
                                            value="Long"
                                            checked={textLength === 'Long'}
                                            onChange={() =>
                                                setTextLength('Long')
                                            }
                                        />
                                        <label
                                            className={styles['label']}
                                            htmlFor="long"
                                        >
                                            Long
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <p className={styles['form-error']}>*required</p>
                            <div id={styles['form-actions']}>
                                <button
                                    id={styles['action-submit']}
                                    className={styles['btn btn-submit']}
                                    type="submit"
                                >
                                    Submit
                                </button>
                                <button
                                    id={styles['action-cancel']}
                                    className={styles['btn btn-cancel']}
                                    type="button"
                                    onClick={onClose}
                                >
                                    Cancel
                                </button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </>
    );
}
