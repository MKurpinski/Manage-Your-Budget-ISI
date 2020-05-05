import authProvider from '../../authProvider';
import React from 'react';
import MainContainer from '../../containers/Main/MainContainer';
import LoginContainer from '../../containers/Login/LoginContainer';


const MainRoute = () => {
    const isAuthorized = authProvider.isAuthenticated();
    return (
        isAuthorized ? <MainContainer/> : <LoginContainer/>
    );
};

export default MainRoute;