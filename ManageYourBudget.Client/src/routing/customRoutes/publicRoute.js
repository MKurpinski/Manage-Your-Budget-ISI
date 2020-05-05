import React from 'react';
import { Redirect } from 'react-router-dom';
import Route from 'react-router-dom/es/Route';
import authProvider from '../../authProvider';
import { routesConstants } from '../index'

const PublicRoute = ({ component: Component, ...rest }) => {
    const isNotAuthorized = !authProvider.isAuthenticated();
    return (
        <Route
            {...rest}
            render={props =>
                isNotAuthorized ? (
                    <Component {...props} />
                ) : (
                    <Redirect to={routesConstants.MAIN} />
                )
            }
        />
    );
};

export default PublicRoute;