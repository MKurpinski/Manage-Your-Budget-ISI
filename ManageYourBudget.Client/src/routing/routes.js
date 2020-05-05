import { Switch } from 'react-router';
import React from 'react';
import routesConstants from './routesConstants';
import { PublicRoute, PrivateRoute, MainRoute } from './customRoutes';
import App from '../App';
import LoginContainer from '../containers/Login/LoginContainer';
import RegisterContainer from '../containers/Register/RegisterContainer';
import MainContainer from '../containers/Main/MainContainer';

export default (
    <App>
        <Switch>
            <MainRoute exact path="/" />
            <PublicRoute path={routesConstants.LOGIN} component={LoginContainer}/>
            <PublicRoute path={routesConstants.REGISTER} component={RegisterContainer}/>
            <PrivateRoute path={routesConstants.MAIN} component={MainContainer}/>
        </Switch>
    </App>
);