import React from 'react'
import { connect } from 'react-redux';
import { startSubmit, stopSubmit } from 'redux-form'
import { resetPasswordApi } from '../../api';
import { routesConstants } from '../../routing';
import { withRouter } from 'react-router-dom';
import { toastrService } from '../../common';
import authProvider from '../../authProvider';
import ResetForm from '../../components/Reset/resetForm';
import CustomSpiner from '../../components/common/customSpiner';
import { Card } from 'semantic-ui-react';

const RESET_FORM = 'resetForm';

class RegisterContainer extends React.Component {
    state = {
        hash: '',
        hashValidated: false
    };

    componentWillMount() {
        authProvider.removeToken();
    }

    async componentDidMount() {
        const hash = this.props.match.params.hash;
        try {
            await resetPasswordApi.validateHash(hash);
            this.setState({hash, hashValidated: true});
        }
        catch (err) {
            toastrService.error('Your password reset link is not valid!');
            this.props.history.push(routesConstants.LOGIN);
        }
    }

    handleSubmit = async (resetData) => {
        resetData = {...resetData, hash: this.state.hash};
        this.props.dispatch(startSubmit(RESET_FORM));
        try {
            await resetPasswordApi.reset(resetData);
            toastrService.success('You have successfully changed the password! Log in to app!');
            this.props.history.push(routesConstants.LOGIN)
        }
        catch (error) {
            toastrService.error('Something went wrong! Try again!');
            this.props.dispatch(stopSubmit(RESET_FORM));
        }
    };

    render() {
        return (
            <div className="page-container auth-container">
                <div className="form-wrapper">
                    <Card centered className="form-wrapper--card" fluid>
                        <h1 className="auth-title">Reset password</h1>
                        <ResetForm onSubmit={this.handleSubmit}/>
                    </Card>
                </div>
                <CustomSpiner active={!this.state.hashValidated}/>
            </div>
        )
    }
}

export default connect()(withRouter(RegisterContainer))