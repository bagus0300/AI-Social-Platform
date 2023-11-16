import { Link } from 'react-router-dom';

import styles from './Register.module.css';

export default function Register() {
    return (
        <section className={styles['section-wrapper']}>
            <div className={styles['register-wrapper']}>
                <div className={styles['register-form-wrapper']}>
                    <h2>Create Account</h2>
                    <p>It's quick and easy</p>
                    <form className={styles['register-form']}>
                        <section className={styles['names-wrapper']}>
                            <div className={styles['first-name']}>
                                <input
                                    className={styles['name-input']}
                                    type="text"
                                    name="firstName"
                                    id="firstName"
                                    placeholder="First name"
                                />
                                <i className="fa-solid fa-user"></i>
                            </div>
                            <div className={styles['last-name']}>
                                <input
                                    className={styles['name-input']}
                                    type="text"
                                    name="lastName"
                                    id="lastName"
                                    placeholder="Last name"
                                />
                                <i className="fa-solid fa-user"></i>
                            </div>
                        </section>
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
                        <section className={styles['confirm-password-wrapper']}>
                            <input
                                className={styles['input-field']}
                                type="password"
                                name="confirmPassword"
                                id="confirmPassword"
                                placeholder="Confrim Password"
                            />
                            <i className="fa-solid fa-key"></i>
                        </section>

                        <button className={styles['register-button']}>
                            Register
                        </button>
                    </form>
                    <section className={styles['sign-in']}>
                        <p>Already have an account?</p>
                        <Link to={'/login'}>Sign In</Link>
                    </section>
                </div>
                <div className={styles['media-content']}>
                    <img src="/images/Logo.png" alt="Logo" />
                </div>
            </div>
        </section>
    );
}
