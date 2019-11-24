import axios from 'axios';
import {getTime} from '../../util';

export const SET_COURSE = 'COURSE | SET';

export function getCourse(code)
{
    return (dispatch) => {
        axios.get(`./json/courses.json?nocache=${getTime()}`).then((res) => {
            const courses = res.data || [];
            dispatch(setCourse(courses[code] || {}));
        })
    }
}


export function setCourse(data)
{
    return {type: SET_COURSE, payload:data}
}