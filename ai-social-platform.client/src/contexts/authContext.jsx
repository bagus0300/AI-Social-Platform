import { createContext } from 'react';
import { useNavigate } from 'react-router-dom';

import { PATH } from '../core/environments/costants';
import * as authService from '../core/services/authService';
import usePersistedState from '../hooks/usePersistedState';

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [auth, setAuth] = usePersistedState('auth', {});
    const navigate = useNavigate();

    const loginSubmitHandler = async (values) => {
        const result = await authService.login(values.email, values.password);

        setAuth(result);

        localStorage.setItem('accessToken', result.token);

        navigate(PATH.home);
    };

    const values = {
        loginSubmitHandler,
        email: auth.email,
        isAuthenticated: !!auth.token,
    };

    return (
        <AuthContext.Provider value={values}>{children}</AuthContext.Provider>
    );
};

export default AuthContext;
