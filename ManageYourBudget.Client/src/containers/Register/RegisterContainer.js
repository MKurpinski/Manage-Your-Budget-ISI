import React from 'react'
import { connect } from 'react-redux';
import { startSubmit, stopSubmit, SubmissionError } from 'redux-form'
import { authApi } from '../../api';
import { routesConstants } from '../../routing';
import { withRouter } from 'react-router-dom';
import RegisterForm from '../../components/Register/registerForm';
import { toastrService } from '../../common';
import './RegisterContainer.scss';
import { Card } from 'semantic-ui-react';

const REGISTER_FORM = 'registerForm';

class RegisterContainer extends React.Component {

    handleSubmit = async (registerData) => {
        this.props.dispatch(startSubmit(REGISTER_FORM));
        try {
            await authApi.register(registerData);
            toastrService.success('You have successfully registered! Log in to app!');
            this.props.history.push(routesConstants.LOGIN)
        }
        catch (error) {
            const response = error.response;
            this.props.dispatch(stopSubmit(REGISTER_FORM));
            throw new SubmissionError(response.data.errors)
        }
    };

    render() {
        return (
            <div className="page-container auth-container">
                <div className="form-wrapper">
                    <Card centered className="form-wrapper--card" fluid>
                        <h1 className="auth-title">Join us! <i
                            className="wallet-icon money bill alternate outline icon"/></h1>
                        <RegisterForm onSubmit={this.handleSubmit}/>
                    </Card>
                </div>
            </div>
        )
    }
}

export default connect()(withRouter(RegisterContainer))