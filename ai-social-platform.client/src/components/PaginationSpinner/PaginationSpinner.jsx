import styles from './PaginationSpinner.module.css';

export default function PaginationSpinner() {
    return (
        <div className={styles['load-ellipsis']}>
            <div></div>
            <div></div>
            <div></div>
            <div></div>
        </div>
    );
}
