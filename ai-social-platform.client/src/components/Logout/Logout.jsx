import { useContext, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

import { PATH } from '../../core/environments/costants';
import * as authService from '../../core/services/authService';
import AuthContext from '../../contexts/authContext';

export default function Logout() {
    const navigate = useNavigate();
    const { logoutHandler } = useContext(AuthContext);

    useEffect(() => {
        authService
            .logout()
            .then(() => {
                logoutHandler();
                navigate(PATH.home);
            })
            .catch(() => {
                logoutHandler();
                navigate(PATH.login);
            });
    }, []);

    return null;
}
