export const SET_MARKSHEET = 'MARKSHEET | SET_MARKSHEET';
export const RESET_MARKSHEET = 'MARKSHEET | RESET_MARKSHEET';
export const SET_ROWS = 'ROWS | SET_ROWS';

export function setMarksheet(data) {
    return { type: SET_MARKSHEET, payload: data };
}

export function setRows(data) {
    return { type: SET_ROWS, payload: data };
}

export function resetMarksheet()
{
    return { type: RESET_MARKSHEET };

}
