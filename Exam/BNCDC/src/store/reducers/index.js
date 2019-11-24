import { combineReducers } from 'redux';
import users from './users'
import app from './app';
import course from './course';
import marksheet from './marksheet';
export default combineReducers({
    users,
    course,
    marksheet,
    app
});