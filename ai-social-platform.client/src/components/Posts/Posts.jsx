import { useEffect, useReducer } from 'react';

import * as postService from '../../core/services/postService';
import styles from './Posts.module.css';
import postReducer from '../../reducers/postReducer';

import PostItem from './PostItem/PostItem';

export default function Posts() {
    const [posts, dispatchPost] = useReducer(postReducer, []);

    useEffect(() => {
        postService
            .getAllPosts()
            .then((res) => {
                dispatchPost({
                    type: 'GET_ALL_POSTS',
                    payload: res.publications,
                });
            })
            .catch((error) => console.log(error));
    }, []);

    return (
        <section className={styles['posts-section']}>
            <div className={styles['content-wrapper']}>
                {posts.map((post) => (
                    <PostItem key={post.id} post={post} />
                ))}
            </div>
        </section>
    );
}
