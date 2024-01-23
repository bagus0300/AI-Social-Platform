import { useEffect, useReducer, useRef, useState } from 'react';

import { PostActions } from '../../core/environments/costants';
import * as postService from '../../core/services/postService';
import styles from './Posts.module.css';
import postReducer from '../../reducers/postReducer';

import PostItem from './PostItem/PostItem';
import PaginationSpinner from '../PaginationSpinner/PaginationSpinner';

export default function Posts() {
    const [page, setPage] = useState(1);

    const [posts, dispatchPost] = useReducer(postReducer, []);

    const [hasMore, setHasMore] = useState(true);

    const elementRef = useRef(null);

    function onIntersection(entries) {
        const firstEntry = entries[0];

        if (firstEntry.isIntersecting && hasMore) {
            fetchMoreItems();
        }
    }

    useEffect(() => {
        const observer = new IntersectionObserver(onIntersection);

        if (observer && elementRef.current) {
            observer.observe(elementRef.current);
        }

        return () => {
            if (observer) {
                observer.disconnect();
            }
        };
    }, [posts]);

    async function fetchMoreItems() {
        const data = await postService.getAllPosts(page);

        if (data.publicationsLeft === 0 && data.publications.length === 0) {
            setHasMore(false);
        } else {
            dispatchPost({
                type: PostActions.GetAllPosts,
                payload: data.publications,
            });

            setPage((state) => state + 1);
        }
    }

    return (
        <section className={styles['posts-section']}>
            <div className={styles['content-wrapper']}>
                {posts
                    .sort(
                        (a, b) =>
                            new Date(b.dateCreated) - new Date(a.dateCreated)
                    )
                    .map((post) => (
                        <PostItem key={post.id} post={post} />
                    ))}
                {hasMore && (
                    <div ref={elementRef}>
                        <PaginationSpinner />
                    </div>
                )}
                {posts.length > 0 && !hasMore && (
                    <div>There are no more posts</div>
                )}
                {posts.length === 0 && (
                    <p>
                        Add friends or follow topics of your choice to fill your
                        feed.
                    </p>
                )}
            </div>
        </section>
    );
}
