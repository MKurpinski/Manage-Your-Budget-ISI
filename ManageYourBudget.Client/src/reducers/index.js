import { combineReducers } from 'redux';
import { routerReducer } from 'react-router-redux';
import { reducer as formReducer } from 'redux-form';
import profileReducer from './profileReducer';

const rootReducer = combineReducers({
    router: routerReducer,
    form: formReducer,
    profile: profileReducer
});

export default rootReducer;
