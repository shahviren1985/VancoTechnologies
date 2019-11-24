export const SET_USER = 'APP | SET_USER';
export const SET_IS_LOGGED_IN = 'APP | SET_IS_LOGGED_IN';
export const SET_RESET = 'APP | SET_RESET';
export const SET_PAPERS = 'APP | SET_PAPERS';
export const SET_APP = 'APP | SET_APP';
export const SET_GRADES = 'APP | SET_GRADES';


export function setUser(user) {
    return { type: SET_USER, payload: user };
}

export function setIsLoggedIn(flag) {
    return { type: SET_IS_LOGGED_IN, payload: flag };

}

export function setPapers(papers)
{
    return { type: SET_PAPERS, payload: papers };
}

export function reset()
{
    return { type: SET_RESET };
}

export function setApp(payload)
{
    return { type: SET_APP, payload };
}