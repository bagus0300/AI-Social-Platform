import { useContext } from 'react';
import { Navigate, Outlet } from 'react-router-dom';

import { PATH } from '../core/environments/costants';
import AuthContext from '../contexts/authContext';

export default function AuthGuard() {
    const { isAuthenticated } = useContext(AuthContext);

    if (!isAuthenticated) {
        return <Navigate to={PATH.login} />;
    }

    return <Outlet />;
}
