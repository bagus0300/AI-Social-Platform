export const host = 'https://localhost:7172/api/';

export const PATH = {
    home: '/',
    login: '/users/login',
    register: '/users/register',
    logout: '/users/logout',
    create: '/create-post',
    profile: '/profile/:userId',
    profileedit: '/profileedit/:userId',
    userProfile: (userId) => `/profile/${userId}`,
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
    userDetails: (userId) => `User/userDetails/${userId}`,

    // POSTS
    createPost: 'Publication',
    getAllPosts: 'Publication',

    // MEDIA
    addMedia: 'Media/upload?isItPublication=true',
    getPostMedia: (postId) => `Media/${postId}`,
    getMediaById: (mediaId) => `Media/serve/${mediaId}`,

    // COMMENTS
    createComment: 'SocialFeature/comment',
    getAllComments: (postId) => `SocialFeature/comment/${postId}`,
    deleteComment: (commentId) => `SocialFeature/comment/${commentId}`,
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
    School: 'school',
    Birthday: 'birthday',
    Relationship: 0,
    Schools: [{}],
};

export const CreateFormKeys = {
    PostDescription: 'postDescription',
    PostMedia: 'postMedia',
};

export const CommentFormKeys = {
    CommentText: 'commentText',
};

export const ContentType = {
    ApplicationJSON: 'application/json',
    MulitpartFormData: 'multipart/form-data',
};

export const CommentActions = {
    CreateComment: 'createComment',
    GetAllComments: 'getAllComments',
    EditComment: 'editComment',
    DeleteComment: 'deleteComment',
};

export const tokenName = 'accessToken';
