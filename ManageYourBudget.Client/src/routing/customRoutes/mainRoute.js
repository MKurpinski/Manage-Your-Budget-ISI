import authProvider from '../../authProvider';
import React from 'react';
import LoginContainer from '../../containers/Login/LoginContainer';
import { Redirect } from 'react-router-dom';


const MainRoute = () => {
    const isAuthorized = authProvider.isAuthenticated();
    return (
        isAuthorized ? <Redirect to="/main"/> : <LoginContainer/>
    );
};

export default MainRoute;