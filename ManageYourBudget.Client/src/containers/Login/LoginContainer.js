import React from 'react'
import LoginForm from '../../components/Login/loginForm';
import { connect } from 'react-redux';
import { startSubmit, stopSubmit, SubmissionError } from 'redux-form'
import { authApi } from '../../api';
import authProvider from '../../authProvider';
import { routesConstants } from '../../routing';
import { withRouter } from 'react-router-dom';
import { toastrService } from '../../common';
import FacebookLogin from 'react-facebook-login/dist/facebook-login-render-props'
import { constants } from '../../common';
import { IconButton, SimpleLink } from '../../components/common/buttons';
import { Button, Card, Responsive } from 'semantic-ui-react';
import { Divider } from 'semantic-ui-react';
import './LoginContainer.css'
import CustomSpiner from '../../components/common/customSpinner';
import Titled from '../../components/Titled/titled';
import { FORMS } from '../../common/constants';

class LoginContainer extends React.Component {
    state = {
        isLoading: false
    };

    handleSubmit = async (loginData) => {
        this.startLoading();
        this.props.dispatch(startSubmit(FORMS.LOGIN_FORM));
        try {
            const response = await authApi.login(loginData);
            this.handleSuccessLogin(response);
        }
        catch (error) {
            this.stopLoading();
            const response = error.response;
            this.props.dispatch(stopSubmit(FORMS.LOGIN_FORM));
            throw new SubmissionError(response.data.errors)
        }
    };

    handleFacebookLogin = async (response) => {
        try {
            this.startLoading();
            const loginResponse = await authApi.loginWithFacebook(response);
            this.handleSuccessLogin(loginResponse);
        }
        catch (err) {
            this.handleExternalLoginError();
        }
    };

    handleGoogleLogin = async () => {
        try {
            this.startLoading();
            window.location.href = await authApi.getGoogleRedirectUri();
        }
        catch (err) {
            this.handleExternalLoginError();
        }
    };

    handleExternalLoginError = () => {
        this.stopLoading();
        toastrService.error('Login with chosen social media has failed! Try again!');
    };

    handleSuccessLogin = loginResponse => {
        authProvider.saveToken(loginResponse.value.token);
        let returnUrl = new URLSearchParams(this.props.location.search).get('returnUrl');
        returnUrl = returnUrl ? returnUrl : routesConstants.MAIN;
        this.props.history.push(returnUrl);
    };

    render() {
        return (
            <Titled title='Login'>
                <div className="page-container auth-container login-container">
                    <div className="form-wrapper">
                        <Card centered className="form-wrapper--card" fluid>
                            <h1 className="auth-title">Login <i
                                className="wallet-icon money bill alternate outline icon"/></h1>
                            <LoginForm onSubmit={this.handleSubmit}/>
                            <div className="link-section">
                                <SimpleLink to={routesConstants.START_RESET}>Forgot password?</SimpleLink>
                                <SimpleLink to={routesConstants.REGISTER}>New account?</SimpleLink>
                            </div>
                            <Divider horizontal>Or</Divider>
                            <Button.Group vertical>

                                <Responsive as={FacebookLogin} {...Responsive.onlyComputer}
                                    appId={constants.FACEBOOK_APP_ID}
                                    callback={this.handleFacebookLogin}
                                    isMobile={false}
                                    render={renderProps => (
                                        <IconButton
                                            iconName="facebook"
                                            className="auth-button"
                                            onClick={renderProps.onClick}>
                                            Facebook
                                        </IconButton>
                                    )}
                                />
                                <IconButton className="auth-button" iconName="google plus"
                                            onClick={this.handleGoogleLogin}>Google</IconButton>
                            </Button.Group>
                        </Card>
                    </div>
                    <CustomSpiner active={this.state.isLoading}/>
                </div>
            </Titled>
        )
    }

    startLoading() {
        this.setState({isLoading: true})
    }

    stopLoading() {
        this.setState({isLoading: false})
    }
}

export default connect()(withRouter(LoginContainer))
