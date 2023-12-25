
// ON CREATE POST WE NEED RESPONSE TO BE POST DATA

const postReducer = (state, action) => {
    switch (action?.type) {
        case 'GET_ALL_POSTS':
            return [...action.payload];
        case 'CREATE_POST':
            return [...state, action.payload.content];
        case 'EDIT_POST':
            return state.map((p) =>
                p.id === action.payload.id
                    ? { ...p, content: action.payload.content }
                    : p
            );
        case 'DELETE_POST':
            return state.filter((p) => p.id !== action.payload.id);
        default:
            return state;
    }
};

export default postReducer;
