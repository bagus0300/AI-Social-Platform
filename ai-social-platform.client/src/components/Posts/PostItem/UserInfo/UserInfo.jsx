import { Link } from 'react-router-dom';

import { PATH } from '../../../../core/environments/costants';
import dateFormater from '../../../../utils/dateFormatter';
import styles from './UserInfo.module.css';

export default function UserInfo({ post }) {
    return (
        <section className={styles['user-info']}>
            <div className={styles['user-info-wrapper']}>
                <Link to={PATH.userProfile(post.authorId)}>
                    <img
                        className={styles['user-img']}
                        src={
                            post.author?.profilPictureUrl ||
                            '/images/default-profile-pic.png'
                        }
                        alt="User profile pic"
                    />
                </Link>
                <div className={styles['post-info']}>
                    <p className={styles['username-wrapper']}>
                        <Link
                            to={PATH.userProfile(post.authorId)}
                            className={styles['username']}
                        >
                            {post.author?.firstName} {post.author?.lastName}
                        </Link>
                    </p>
                    <p className={styles['posted-on']}>
                        Posted on: {dateFormater(post?.dateCreated)}
                    </p>
                </div>
            </div>
            {/* {post.authorId === userId && ( */}
            {/* <div */}
            {/* onClick={showEditMenuToggle} */}
            {/* className={styles['edit']} */}
            {/* > */}
            {/* <i className="fa-solid fa-pen-to-square"></i> */}
            {/* <span className={styles['dot-menu']}>•</span> */}
            {/* <span className={styles['dot-menu']}>•</span> */}
            {/* <span className={styles['dot-menu']}>•</span> */}
            {/* </div> */}
            {/* )} */}
        </section>
    );
}
