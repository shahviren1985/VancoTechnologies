import { SET_COURSE} from '../actions/course';

const initialState = {
    title: '',
    years : []
};

const course = (state = initialState, action) => {
    switch(action.type)
    {
        case SET_COURSE: 
        return {...action.payload};
        default:
        return state
    }
}

export default course;

