import initialState from './initialState';
import profileActions from '../actions';

export default function profileReducer(state = initialState.profile, action) {
    switch (action.type) {
        case profileActions.GET_PROFILE:
            return { ...action.payload, loaded: true };
        case profileActions.EDIT_PROFILE:
            return { ...state, ...action.payload };
        default:
            return state;
    }
}