import { Component } from 'react';

import './styles.css';
import { PATH } from '../../core/environments/costants';

export default class ErrorBoundary extends Component {
    constructor() {
        super();

        this.state = { hasError: false };
    }

    static getDerivedStateFromError(err) {
        return { hasError: true };
    }

    componentDidCatch(error, errorInfo) {
        console.log(error);
        console.error(errorInfo);
    }

    render() {
        if (this.state.hasError) {
            return (
                <section className="error-boundary">
                    <h1 className="section-heading">404</h1>
                    <p className="section-text">
                        Oops... Something went wrong.
                    </p>
                    <a href={PATH.home} className="home-button">
                        Home
                    </a>
                </section>
            );
        }

        return this.props.children;
    }
}
