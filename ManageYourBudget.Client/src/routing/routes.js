import { Switch } from 'react-router';
import React from 'react';
import routesConstants from './routesConstants';
import { PublicRoute, PrivateRoute, MainRoute } from './customRoutes';
import App from '../App';
import LoginContainer from '../containers/Login/LoginContainer';
import RegisterContainer from '../containers/Register/RegisterContainer';
import MainContainer from '../containers/Main/MainContainer';
import StartResetContainer from '../containers/StartReset/StartResetContainer';
import { Route } from 'react-router-dom';
import ResetContainer from '../containers/Reset/ResetContainer';
import GoogleCallbackContainer from '../containers/GoogleCallback/GoogleCallbackContainer';

export default (
    <App>
        <Switch>
            <MainRoute exact path="/"/>
            <PublicRoute path={routesConstants.LOGIN} component={LoginContainer}/>
            <PublicRoute exact path={routesConstants.START_RESET} component={StartResetContainer}/>
            <PublicRoute path={routesConstants.REGISTER} component={RegisterContainer}/>
            <PublicRoute exact path={routesConstants.GOOGLE} component={GoogleCallbackContainer}/>
            <PrivateRoute path={routesConstants.MAIN} component={MainContainer}/>
            <Route path={routesConstants.RESET} component={ResetContainer}/>
        </Switch>
    </App>
);