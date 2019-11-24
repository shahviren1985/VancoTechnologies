import { SET_USER, SET_IS_LOGGED_IN, SET_RESET, SET_PAPERS, SET_APP, SET_GRADES} from '../actions/app';

const initialState = {
    isLoggedIn: false,
    user: {},
    papers: [],
    maxGrace: ''
}

const app = (state = initialState, action) => {
    switch (action.type) {
        case SET_USER:
            return { ...state, user: action.payload };
        case SET_IS_LOGGED_IN:
            return { ...state, isLoggedIn: action.payload };
        case SET_PAPERS:
            return { ...state, papers: [...action.payload] };
        case SET_APP:
            return { ...state, ...action.payload };
        case SET_GRADES:
            return { ...state, grades : [...action.payload] };
        case SET_RESET:
            return initialState;
        default:
            return state
    }
}

export default app;