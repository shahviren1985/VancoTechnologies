import { SET_MARKSHEET, RESET_MARKSHEET, SET_ROWS} from '../actions/marksheet';

const initialState = {
   examName: '',
   examYear: '',
   date: null,
   year : '',
   rows: []
}

const marksheet = (state = initialState, action) => {
    switch (action.type) {
        case SET_MARKSHEET:
            return { ...state, ...action.payload };
        case SET_ROWS:
            return { ...state, rows: [...action.payload] };
        case RESET_MARKSHEET:
            return initialState;
        default:
            return state
    }
}

export default marksheet;