import { createContext } from 'react';
import { useNavigate } from 'react-router-dom';

import { PATH } from '../core/environments/costants';
import * as authService from '../core/services/authService';
import * as userService from '../core/services/userService';
import usePersistedState from '../hooks/usePersistedState';

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [auth, setAuth] = usePersistedState('auth', {});
    const navigate = useNavigate();

    const loginSubmitHandler = async (values) => {
        const result = await authService.login(values.email, values.password);

        localStorage.setItem('accessToken', result.token);

        setAuth(result);

        navigate(PATH.home);
    };

    const registerSubmitHandler = async ({
        firstName,
        lastName,
        email,
        phoneNumber,
        password,
        confirmPassword,
    }) => {
        const result = await authService.register({
            firstName,
            lastName,
            email,
            phoneNumber,
            password,
            confirmPassword,
        });

        setAuth(result);

        localStorage.setItem('accessToken', result.token);

        navigate(PATH.home);
    };

    const logoutHandler = () => {
        setAuth({});

        localStorage.removeItem('accessToken');

        navigate(PATH.home);
    };

    const values = {
        loginSubmitHandler,
        registerSubmitHandler,
        logoutHandler,
        firstName: auth?.firstName,
        lastName: auth?.lastName,
        profilePicture: auth?.profilePicture,
        userId: auth?.userId,
        isAuthenticated: !!auth.token,
    };

    return (
        <AuthContext.Provider value={values}>{children}</AuthContext.Provider>
    );
};

export default AuthContext;
