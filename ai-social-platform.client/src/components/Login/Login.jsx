import { useContext, useState } from 'react';
import { Link } from 'react-router-dom';
import { useFormik } from 'formik';

import { LoginFormKeys, PATH } from '../../core/environments/costants';
import styles from './Login.module.css';
import AuthContext from '../../contexts/authContext';
import loginValidation from './loginValidation';

const initialValues = {
    [LoginFormKeys.Email]: '',
    [LoginFormKeys.Password]: '',
};

export default function Login() {
    const [serverError, setServerError] = useState({});
    const {
        values,
        errors,
        touched,
        isSubmitting,
        handleSubmit,
        handleChange,
        handleBlur,
    } = useFormik({
        initialValues,
        onSubmit,
        validationSchema: loginValidation,
    });

    const { loginSubmitHandler } = useContext(AuthContext);

    async function onSubmit(values) {
        try {
            await loginSubmitHandler(values);
        } catch (error) {
            setServerError(error);
        }
    }

    return (
        <section className={styles['section-wrapper']}>
            <div className={styles['login-wrapper']}>
                <div className={styles['login-form-wrapper']}>
                    <h2>Log in to your Account</h2>
                    <p>Welcome back!</p>
                    {serverError ? (
                        <p className={styles['error-message']}>
                            {serverError.message}
                        </p>
                    ) : (
                        ''
                    )}
                    <form
                        onSubmit={handleSubmit}
                        className={styles['login-form']}
                    >
                        <section className={styles['email-wrapper']}>
                            <label htmlFor={LoginFormKeys.Email}></label>
                            <input
                                className={
                                    errors[LoginFormKeys.Email] &&
                                    touched[LoginFormKeys.Email]
                                        ? styles['input-field-error']
                                        : styles['input-field']
                                }
                                type="email"
                                name={LoginFormKeys.Email}
                                id={LoginFormKeys.Email}
                                placeholder="Email"
                                onChange={handleChange}
                                onBlur={handleBlur}
                                value={values[LoginFormKeys.Email]}
                            />
                            <i className="fa-regular fa-envelope"></i>
                        </section>
                        <section className={styles['password-wrapper']}>
                            <label htmlFor={LoginFormKeys.Password}></label>
                            <input
                                className={
                                    errors[LoginFormKeys.Password] &&
                                    touched[LoginFormKeys.Password]
                                        ? styles['input-field-error']
                                        : styles['input-field']
                                }
                                type="password"
                                name={LoginFormKeys.Password}
                                id={LoginFormKeys.Password}
                                placeholder="Password"
                                onChange={handleChange}
                                onBlur={handleBlur}
                                value={values[LoginFormKeys.Password]}
                            />
                            <i className="fa-solid fa-key"></i>
                        </section>
                        <section className={styles['forgot-password']}>
                            {/* <div className={styles['remember-me']}>
                                <input className={styles['input-checkbox']} type="checkbox" name="checkbox" id="checkbox" />
                                <p className={styles['text']}>Remember me</p>
                            </div> */}
                            <Link to={'/forgot-password'}>
                                Forgot Password?
                            </Link>
                        </section>
                        <button
                            type="submit"
                            className={styles['login-button']}
                            disabled={isSubmitting}
                        >
                            {isSubmitting ? 'Logging in...' : 'Log in'}
                        </button>
                    </form>
                    <section className={styles['create-account']}>
                        <p>Don't have an account?</p>
                        <Link to={PATH.register}>Create an account</Link>
                    </section>
                </div>
                <div className={styles['media-content']}>
                    <img src="/images/Logo.png" alt="Logo" />
                </div>
            </div>
        </section>
    );
}
