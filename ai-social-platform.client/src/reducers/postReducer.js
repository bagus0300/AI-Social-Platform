import { PostActions } from '../core/environments/costants';

const postReducer = (state, action) => {
    switch (action?.type) {
        case PostActions.GetAllPosts:
            return [...state, ...action.payload];
        case PostActions.CreatePost:
            return [...state, action.payload];
        case PostActions.EditPost:
            return state.map((p) =>
                p.id === action.payload.id
                    ? { ...p, content: action.payload.content }
                    : p
            );
        case PostActions.DeletePost:
            return state.filter((p) => p.id !== action.payload.id);
        default:
            return state;
    }
};

export default postReducer;
