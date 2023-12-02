import { Link } from 'react-router-dom';
import { useFormik } from 'formik';

import { LoginFormKeys } from '../../core/environments/costants';
import styles from './Login.module.css';
import { useContext } from 'react';
import AuthContext from '../../contexts/authContext';

const initialValues = {
    [LoginFormKeys.Email]: '',
    [LoginFormKeys.Password]: '',
};

export default function Login() {
    const { values, handleSubmit, handleChange, handleBlur } = useFormik({
        initialValues,
        onSubmit,
    });

    const { loginSubmitHandler } = useContext(AuthContext);

    async function onSubmit(values) {
        try {
            await loginSubmitHandler(values);
        } catch (error) {
            console.log('Error is', error);
        }
    }

    return (
        <section className={styles['section-wrapper']}>
            <div className={styles['login-wrapper']}>
                <div className={styles['login-form-wrapper']}>
                    <h2>Log in to your Account</h2>
                    <p>Welcome back!</p>
                    <form
                        onSubmit={handleSubmit}
                        className={styles['login-form']}
                    >
                        <section className={styles['email-wrapper']}>
                            <label htmlFor={LoginFormKeys.Email}></label>
                            <input
                                className={styles['input-field']}
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
                                className={styles['input-field']}
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
                        >
                            Log in
                        </button>
                    </form>
                    <section className={styles['create-account']}>
                        <p>Don't have an account?</p>
                        <Link to={'/register'}>Create an account</Link>
                    </section>
                </div>
                <div className={styles['media-content']}>
                    <img src="/images/Logo.png" alt="Logo" />
                </div>
            </div>
        </section>
    );
}
