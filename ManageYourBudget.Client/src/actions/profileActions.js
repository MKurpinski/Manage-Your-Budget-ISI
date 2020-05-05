export const GET_PROFILE = '[Profile] get profile';
export const EDIT_PROFILE = '[Profile] edit profile';

export const getProfile = (profile) => {
    return { type: GET_PROFILE, payload: profile }
};
export const editProfile = (profile) => {
    return { type: EDIT_PROFILE, payload: profile };
};