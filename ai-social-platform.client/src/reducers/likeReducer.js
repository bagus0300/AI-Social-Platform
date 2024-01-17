import { LikeActions } from '../core/environments/costants';

const likeReducer = (state, action) => {
    switch (action?.type) {
        case LikeActions.GetLikes:
            return [...action.payload];
        case LikeActions.AddLike:
            return [...state, action.payload];
        case LikeActions.RemoveLike:
            return state.filter((like) => like.id !== action.payload.id);
        default:
            return state;
    }
};

export default likeReducer;
