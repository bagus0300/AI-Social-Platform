export const host = 'https://localhost:7172/api/';

export const PATH = {
    home: '/',
    login: '/users/login',
    register: '/users/register',
    logout: '/users/logout',
    create: '/create-post',
    detailsPost: '/posts/:postId',
    search: '/search',
    postDetails: (postId) => `posts/${postId}`,
    profile: '/profile/:userId',
    profileedit: '/profileedit/:userId',
    successfully: '/successfuly',
    userProfile: (userId) => `/profile/${userId}`,
    postlist: '/postlist',
    postedit: `/postedit/:postId`,
    notFound: '*',
};

export const FILES = {
    aiLogo: '/images/openAi.png',
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
    getAllUsers: 'User/allUsers',
    userDetails: (userId) => `User/userDetails/${userId}`,
    addFriend: (userId) => `User/addFriend/${userId}`,
    removeFriend: (userId) => `User/removeFriend/${userId}`,
    allFriends: (userId) => `User/allFriends?userId=${userId}`,

    // POSTS
    createPost: 'Publication',
    getAllPosts: 'Publication',
    getPostById: (postId) => `Publication/${postId}`,
    deletePost: (postId) => `Publication/${postId}`,
    editPost: (postId) => `Publication/${postId}`,
    // getPostsByUserId: (userId) => `Publication/User/${userId}`,
    getPostsByUserId: (userId, page) => `Publication/User/${userId}`,


    // MEDIA
    addMedia: 'Media/upload?isItPublication=true',
    getPostMedia: (postId) => `Media/${postId}`,
    getMediaById: (mediaId) => `Media/serve/${mediaId}`,
    getMediaByPostId: (postId) => `Media/${postId}`,
    deleteMedia: (mediaId) => `Media/delete/${mediaId}`,
    editMedia: (mediaId) => `Media/edit/${mediaId}`,


    // COMMENTS
    createComment: 'SocialFeature/comment',
    getAllComments: (postId) => `SocialFeature/comment/${postId}`,
    editComment: (commentId) => `SocialFeature/comment/${commentId}`,
    deleteComment: (commentId) => `SocialFeature/comment/${commentId}`,

    // LIKES
    getLikes: (postId) => `SocialFeature/like/${postId}`,
    addLike: (postId) => `SocialFeature/like/${postId}`,
    removeLike: (likeId) => `SocialFeature/like/${likeId}`,

    // NOTIFICATIONS
    getNotificationsCount: 'SocialFeature/notification/count',
    getNotifications: 'SocialFeature/notification',
    readNotification: (notificationId) =>
        `SocialFeature/notification/read?notificationId=${notificationId}`,

    // OpenAI
    getTextWhitOpenAi: 'OpenAi/generateText',

    // SEARCH
    search: (type, query) => `SocialFeature/search?type=${type}&query=${query}`,
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
    FirstName: 'FirstName',
    LastName: 'LastName',
    //Email: 'email',
    PhoneNumber: 'PhoneNumber',
    ProfilePicture: 'ProfilPicture',
    CoverPhoto: 'CoverPhoto',
    Country: 'Country',
    State: 'State',
    Gender: 'Gender',
    School: 'School',
    Birthday: 'Birthday',
    Relationship: 'Relationship',
};

export const EditPostFormKeys = {
    PostDescription: 'content',
    TopicId: 'topicId',
    PostPicture: 'PostPicture',
    ChangePostPicture: 'ChangePostPicture',
};

export const CreateFormKeys = {
    PostDescription: 'postDescription',
    PostMedia: 'postMedia',
};

export const CommentFormKeys = {
    CommentText: 'commentText',
};

export const EditCommentFromKeys = {
    EditCommentArea: 'editCommentArea',
};

export const SearchFormKeys = {
    SearchSelect: 'search-select',
    SearchText: 'search-text',
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

export const PostActions = {
    CreatePost: 'createPost',
    GetAllPosts: 'getAllPosts',
    EditPost: 'editPost',
    DeletePost: 'deletePost',
};

export const LikeActions = {
    GetLikes: 'getLikes',
    AddLike: 'addLike',
    RemoveLike: 'removeLike',
};

export const tokenName = 'accessToken';
