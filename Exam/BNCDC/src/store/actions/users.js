import axios from 'axios';
import {getTime} from '../../util';

export const SET_USERS = 'USERS | SET';

export function getUsers()
{
    return (dispatch) => {
        axios.get(`./json/users.json?nocache=${getTime()}`).then((res) => dispatch(setUsers(res.data)) )
    }
}


export function setUsers(data)
{
    return {type: SET_USERS, payload:data}
}