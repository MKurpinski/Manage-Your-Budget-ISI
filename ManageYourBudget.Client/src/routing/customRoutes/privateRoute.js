import { Redirect, Route, withRouter } from 'react-router-dom';
import authProvider from '../../authProvider';
import React from 'react';
import { routesConstants } from '../index'

const privateRoute = ({
                          component: Component,
                          computedMatch,
                          ...rest
                      }) => {

    const isAuthorized = authProvider.isAuthenticated();
    const getRedirectUrl = () =>
        computedMatch.url ? `${routesConstants.LOGIN}?returnUrl=${computedMatch.url}` : routesConstants.LOGIN;
    return (
        <Route
            {...rest}
            render={props =>
                isAuthorized ? (
                    <Component {...props} />
                ) : (
                    <Redirect to={getRedirectUrl()}/>
                )
            }
        />
    );
};
const PrivateRoute = withRouter(privateRoute);
export default PrivateRoute;