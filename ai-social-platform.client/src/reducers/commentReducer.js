import { CommentActions } from '../core/environments/costants';

const commentReducer = (state, action) => {
    switch (action?.type) {
        case CommentActions.GetAllComments:
            return [...action.payload];
        case CommentActions.CreateComment:
            return [...state, action.payload.comment];
        case CommentActions.EditComment:
            return state.map((c) =>
                c.id === action.payload.id
                    ? { ...c, content: action.payload.content }
                    : c
            );
        case CommentActions.DeleteComment:
            return state.filter((c) => c.id !== action.payload.id);
        default:
            return state;
    }
};

export default commentReducer;
