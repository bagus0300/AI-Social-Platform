import { useContext } from 'react';
import { Link } from 'react-router-dom';
import { useFormik } from 'formik';

import { PATH, RegisterFormKeys } from '../../core/environments/costants';
import styles from './Register.module.css';
import registerValidation from './registerValidation';
import AuthContext from '../../contexts/authContext';

const initialValues = {
    [RegisterFormKeys.FirstName]: '',
    [RegisterFormKeys.LastName]: '',
    [RegisterFormKeys.Email]: '',
    [RegisterFormKeys.PhoneNumber]: '',
    [RegisterFormKeys.Password]: '',
    [RegisterFormKeys.ConfirmPassword]: '',
};

export default function Register() {
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
        onSubmit,
        validationSchema: registerValidation,
    });

    const { registerSubmitHandler } = useContext(AuthContext);

    async function onSubmit(values) {
        try {
            await registerSubmitHandler(values);
        } catch (error) {
            console.log('Error:', error);
        }
    }

    return (
        <section className={styles['section-wrapper']}>
            <div className={styles['register-wrapper']}>
                <div className={styles['register-form-wrapper']}>
                    <h2>Create Account</h2>
                    <p>It's quick and easy</p>
                    <form
                        onSubmit={handleSubmit}
                        className={styles['register-form']}
                    >
                        <section className={styles['names-wrapper']}>
                            <div className={styles['first-name']}>
                                <label
                                    htmlFor={RegisterFormKeys.FirstName}
                                ></label>
                                <input
                                    className={
                                        errors[RegisterFormKeys.FirstName] &&
                                        touched[RegisterFormKeys.FirstName]
                                            ? styles['name-input-error']
                                            : styles['name-input']
                                    }
                                    type="text"
                                    name={RegisterFormKeys.FirstName}
                                    id={RegisterFormKeys.FirstName}
                                    placeholder="First name"
                                    onChange={handleChange}
                                    onBlur={handleBlur}
                                    value={values[RegisterFormKeys.FirstName]}
                                />
                                <i className="fa-solid fa-user"></i>
                                {errors[RegisterFormKeys.FirstName] &&
                                    touched[RegisterFormKeys.FirstName] && (
                                        <p className={styles['error-message']}>
                                            {errors[RegisterFormKeys.FirstName]}
                                        </p>
                                    )}
                            </div>
                            <div className={styles['last-name']}>
                                <label
                                    htmlFor={RegisterFormKeys.LastName}
                                ></label>
                                <input
                                    className={
                                        errors[RegisterFormKeys.LastName] &&
                                        touched[RegisterFormKeys.LastName]
                                            ? styles['name-input-error']
                                            : styles['name-input']
                                    }
                                    type="text"
                                    name={RegisterFormKeys.LastName}
                                    id={RegisterFormKeys.LastName}
                                    placeholder="Last name"
                                    onChange={handleChange}
                                    onBlur={handleBlur}
                                    value={values[RegisterFormKeys.LastName]}
                                />
                                <i className="fa-solid fa-user"></i>
                                {errors[RegisterFormKeys.LastName] &&
                                    touched[RegisterFormKeys.LastName] && (
                                        <p className={styles['error-message']}>
                                            {errors[RegisterFormKeys.LastName]}
                                        </p>
                                    )}
                            </div>
                        </section>
                        <section className={styles['email-wrapper']}>
                            <label htmlFor={RegisterFormKeys.Email}></label>
                            <input
                                className={
                                    errors[RegisterFormKeys.Email] &&
                                    touched[RegisterFormKeys.Email]
                                        ? styles['input-field-error']
                                        : styles['input-field']
                                }
                                type="email"
                                name={RegisterFormKeys.Email}
                                id={RegisterFormKeys.Email}
                                placeholder="Email"
                                onChange={handleChange}
                                onBlur={handleBlur}
                                value={values[RegisterFormKeys.Email]}
                            />
                            <i className="fa-regular fa-envelope"></i>
                            {errors[RegisterFormKeys.Email] &&
                                touched[RegisterFormKeys.Email] && (
                                    <p className={styles['error-message']}>
                                        {errors[RegisterFormKeys.Email]}
                                    </p>
                                )}
                        </section>

                        <section className={styles['phone-number-wrapper']}>
                            <label htmlFor={RegisterFormKeys.Email}></label>
                            <input
                                className={
                                    errors[RegisterFormKeys.Email] &&
                                    touched[RegisterFormKeys.Email]
                                        ? styles['input-field-error']
                                        : styles['input-field']
                                }
                                type="text"
                                name={RegisterFormKeys.PhoneNumber}
                                id={RegisterFormKeys.PhoneNumber}
                                placeholder="Phone number"
                                onChange={handleChange}
                                onBlur={handleBlur}
                                value={values[RegisterFormKeys.PhoneNumber]}
                            />
                            <i className="fa-solid fa-phone"></i>
                            {errors[RegisterFormKeys.Email] &&
                                touched[RegisterFormKeys.Email] && (
                                    <p className={styles['error-message']}>
                                        {errors[RegisterFormKeys.Email]}
                                    </p>
                                )}
                        </section>

                        <section className={styles['password-wrapper']}>
                            <label htmlFor={RegisterFormKeys.Password}></label>
                            <input
                                className={
                                    errors[RegisterFormKeys.Password] &&
                                    touched[RegisterFormKeys.Password]
                                        ? styles['input-field-error']
                                        : styles['input-field']
                                }
                                type="password"
                                name={RegisterFormKeys.Password}
                                id={RegisterFormKeys.Password}
                                placeholder="Password"
                                onChange={handleChange}
                                onBlur={handleBlur}
                                value={values[RegisterFormKeys.Password]}
                            />
                            <i className="fa-solid fa-key"></i>
                            {errors[RegisterFormKeys.Password] &&
                                touched[RegisterFormKeys.Password] && (
                                    <p className={styles['error-message']}>
                                        {errors[RegisterFormKeys.Password]}
                                    </p>
                                )}
                        </section>
                        <section className={styles['confirm-password-wrapper']}>
                            <label
                                htmlFor={RegisterFormKeys.ConfirmPassword}
                            ></label>
                            <input
                                className={
                                    errors[RegisterFormKeys.ConfirmPassword] &&
                                    touched[RegisterFormKeys.ConfirmPassword]
                                        ? styles['input-field-error']
                                        : styles['input-field']
                                }
                                type="password"
                                name={RegisterFormKeys.ConfirmPassword}
                                id={RegisterFormKeys.ConfirmPassword}
                                placeholder="Confrim Password"
                                onChange={handleChange}
                                onBlur={handleBlur}
                                value={values[RegisterFormKeys.ConfirmPassword]}
                            />
                            <i className="fa-solid fa-key"></i>
                            {errors[RegisterFormKeys.ConfirmPassword] &&
                                touched[RegisterFormKeys.ConfirmPassword] && (
                                    <p className={styles['error-message']}>
                                        {
                                            errors[
                                                RegisterFormKeys.ConfirmPassword
                                            ]
                                        }
                                    </p>
                                )}
                        </section>

                        <button
                            type="submit"
                            className={styles['register-button']}
                            disabled={isSubmitting}
                        >
                            Register
                        </button>
                    </form>
                    <section className={styles['sign-in']}>
                        <p>Already have an account?</p>
                        <Link to={PATH.login}>Sign In</Link>
                    </section>
                </div>
                <div className={styles['media-content']}>
                    <img src="/images/Logo.png" alt="Logo" />
                </div>
            </div>
        </section>
    );
}
