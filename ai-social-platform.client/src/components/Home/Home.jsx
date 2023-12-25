import { useContext } from 'react';
import { Link } from 'react-router-dom';

import { PATH } from '../../core/environments/costants';
import styles from './Home.module.css';
import AuthContext from '../../contexts/authContext';

import Posts from '../Posts/Posts';

export default function Home() {
    const { firstName } = useContext(AuthContext);

    return (
        <>
            <section className={styles['home-page']}>
                <div className={styles['create-post-banner']}>
                    <Link className={styles['profile-pic-wrapper']}>
                        <img
                            className={styles['user-profile-pic']}
                            src="/images/default-profile-pic.png"
                            alt=""
                        />
                    </Link>
                    <p className={styles['placeholder']}>
                        <Link to={PATH.create} className={styles['text']}>
                            What's on your mind, {firstName}?
                        </Link>
                    </p>
                </div>
                <Posts />
            </section>
        </>
    );
}
