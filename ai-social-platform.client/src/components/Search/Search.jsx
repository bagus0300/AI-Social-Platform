import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useFormik } from 'formik';

import { PATH, SearchFormKeys } from '../../core/environments/costants';
import * as searchService from '../../core/services/searchService';
import searchValidation from './searchValidation';
import styles from './Search.module.css';

import PostItem from '../Posts/PostItem/PostItem';

const initialValues = {
    [SearchFormKeys.SearchSelect]: 'User',
    [SearchFormKeys.SearchText]: '',
};

export default function Search() {
    const [users, setUsers] = useState([]);

    const [posts, setPosts] = useState([]);

    const [topics, setTopics] = useState([]);

    const [isMatch, setIsMatch] = useState(true);

    const {
        values,
        errors,
        isSubmitting,
        touched,
        handleSubmit,
        handleChange,
    } = useFormik({
        initialValues,
        onSubmit,
        validationSchema: searchValidation,
    });

    const navigate = useNavigate();

    async function onSubmit(values) {
        try {
            const result = await searchService.search(
                values[SearchFormKeys.SearchSelect],
                values[SearchFormKeys.SearchText]
            );

            if (result.length === 0) {
                setIsMatch(false);
            } else {
                setIsMatch(true);
            }

            if (values[SearchFormKeys.SearchSelect].toLowerCase() === 'user') {
                setPosts([]);
                setTopics([]);
                setUsers(result);
            } else if (
                values[SearchFormKeys.SearchSelect].toLowerCase() ===
                'publication'
            ) {
                setUsers([]);
                setTopics([]);
                setPosts(result);
            } else if (
                values[SearchFormKeys.SearchSelect].toLowerCase() === 'topic'
            ) {
                setUsers([]);
                setPosts([]);
                setTopics(result);
            }
        } catch (error) {
            console.log(error);
        }
    }

    return (
        <section className={styles['search-section']}>
            <form onSubmit={handleSubmit} className={styles['search-form']}>
                {errors[SearchFormKeys.SearchText] &&
                    touched[SearchFormKeys.SearchText] && (
                        <p className={styles['search-error']}>
                            {errors[SearchFormKeys.SearchText]}
                        </p>
                    )}

                <div className={styles['form-content-wrapper']}>
                    <div className={styles['search-select-wrapper']}>
                        <label htmlFor={SearchFormKeys.SearchSelect}></label>
                        <select
                            className={styles['search-select-element']}
                            name={SearchFormKeys.SearchSelect}
                            id={SearchFormKeys.SearchSelect}
                            onChange={handleChange}
                            value={values[SearchFormKeys.SearchSelect]}
                        >
                            <option>User</option>
                            <option>Publication</option>
                            <option>Topic</option>
                        </select>
                    </div>
                    <div className={styles['search-text-wrapper']}>
                        <label htmlFor={SearchFormKeys.SearchText}></label>
                        <input
                            className={styles['search-text-element']}
                            type="text"
                            name={SearchFormKeys.SearchText}
                            id={SearchFormKeys.SearchText}
                            placeholder="Search..."
                            onChange={handleChange}
                            value={values[SearchFormKeys.SearchText]}
                        />
                    </div>
                    <div>
                        <button
                            className={styles['search-button']}
                            type="submit"
                            disabled={isSubmitting}
                        >
                            Search
                        </button>
                    </div>
                </div>
            </form>

            <section className={styles['search-result']}>
                {!isMatch && (
                    <p className={styles['no-match-message']}>No match.</p>
                )}
                <div className={styles['posts-wrapper']}>
                    {posts.length > 0 &&
                        posts.map((post) => (
                            <PostItem key={post.id} post={post} />
                        ))}
                </div>

                <div className={styles['users-wrapper']}>
                    {users.length > 0 &&
                        users.map((user) => (
                            <div
                                onClick={() =>
                                    navigate(PATH.userProfile(user.id))
                                }
                                className={styles['user-info-wrapper']}
                                key={user.id}
                            >
                                <div className={styles['media']}>
                                    <img
                                        src="/images/default-profile-pic.png"
                                        alt=""
                                    />
                                </div>
                                <div className={styles['username']}>
                                    <p>
                                        {user.firstName} {user.lastName}
                                    </p>
                                </div>
                            </div>
                        ))}
                </div>
            </section>
        </section>
    );
}
