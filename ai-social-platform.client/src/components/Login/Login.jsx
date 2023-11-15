import { Link } from 'react-router-dom';

import styles from './Login.module.css';

export default function Login() {
    return (
        <section className={styles['section-wrapper']}>
            <div className={styles['login-wrapper']}>
                <div className={styles['login-form-wrapper']}>
                    <h2>Log in to your Account</h2>
                    <p>Welcome back!</p>
                    <form className={styles['login-form']}>
                        <section className={styles['email-wrapper']}>
                            <input
                                className={styles['input-field']}
                                type="email"
                                name="email"
                                id="email"
                                placeholder="Email"
                            />
                            <i className="fa-regular fa-envelope"></i>
                        </section>
                        <section className={styles['password-wrapper']}>
                            <input
                                className={styles['input-field']}
                                type="password"
                                name="password"
                                id="password"
                                placeholder="Password"
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
                        <button className={styles['login-button']}>
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
