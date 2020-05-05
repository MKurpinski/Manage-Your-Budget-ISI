import { createStore, applyMiddleware, compose } from 'redux';
import { routerMiddleware } from 'react-router-redux';
import reduxImmutableStateInvariant from 'redux-immutable-state-invariant';
import createHistory from 'history/createBrowserHistory';
import rootReducer from '../reducers';
import { IS_PRODUCTION } from '../constants';

export const history = createHistory();

const enhancers = [];
const middleware = [routerMiddleware(history)];

if (!IS_PRODUCTION) {
    middleware.push(reduxImmutableStateInvariant());
    const devToolsExtension = window.devToolsExtension;
    if (typeof devToolsExtension === 'function') {
        enhancers.push(devToolsExtension());
    }
}

const composedEnhancers = compose(applyMiddleware(...middleware), ...enhancers);

export default function configureStore(initialState) {
    return createStore(rootReducer, initialState, composedEnhancers);
}