import { SET_USERS} from '../actions/users';

const initialState = [];

const users = (state = initialState, action) => {
    switch(action.type)
    {
        case SET_USERS: 
        return [...action.payload];
        default:
        return state
    }
}

export default users;

