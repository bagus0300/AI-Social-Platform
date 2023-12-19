export const host = 'https://localhost:7172/api/';

export const PATH = {
    home: '/',
    login: '/users/login',
    register: '/users/register',
    logout: '/users/logout',
    profile: '/profile/:userId',
    profileedit: '/profileedit/:userId',
};

export const endpoints = {
    // AUTH
    login: 'User/login',
    register: 'User/register',
    logout: 'User/logout',

    // USER
    getLoggedUserDetails: 'User/userDetails',
    userDetails: 'User/userDetails',
    updateUser: 'User/updateUser',
};

export const LoginFormKeys = {
    Email: 'email',
    Password: 'password',
};

export const RegisterFormKeys = {
    FirstName: 'firstName',
    LastName: 'lastName',
    Email: 'email',
    PhoneNumber: 'phoneNumber',
    Password: 'password',
    ConfirmPassword: 'confirmPassword',
};

export const ProfileFormKeys = {
    FirstName: 'firstName',
    LastName: 'lastName',
    Email: 'email',
    PhoneNumber: 'phoneNumber',
    Password: 'password',
    ConfirmPassword: 'confirmPassword',
    Country: 'country',
    State: 'state',
    Gender: 0,
    School: "school",
    Birthday: 'birthday',
    Relationship: 0,
    Schools: [{}],
  };