import React from 'react'
import { connect } from 'react-redux';
import { startSubmit, stopSubmit } from 'redux-form'
import { routesConstants } from '../../routing';
import { withRouter } from 'react-router-dom';
import { resetPasswordApi } from '../../api';
import { toastrService } from '../../common';
import StartResetForm from '../../components/StartReset/startResetForm';
import { Card, Divider } from 'semantic-ui-react';
import { LinkAsButton } from '../../components/common/buttons';
import Titled from '../../components/Titled/titled';

const START_RESET_FROM = 'start_reset_form';

class StartResetContainer extends React.Component {

    handleSubmit = async (resetData) => {
        this.props.dispatch(startSubmit(START_RESET_FROM));
        try {
            await resetPasswordApi.startFlow(resetData);
            toastrService.success('We\'ve send you password reset email. Check the inbox! ');
            this.props.history.push(routesConstants.LOGIN)
        }
        catch (error) {
            toastrService.error('Something went wrong, try again!');
            this.props.dispatch(stopSubmit(START_RESET_FROM));
        }
    };

    render() {
        return (
            <Titled title='Forgot password'>
                <div className="page-container auth-container">
                    <div className="form-wrapper">
                        <Card centered className="form-wrapper--card" fluid>
                            <h1 className="auth-title">Forgot password?</h1>
                            <StartResetForm onSubmit={this.handleSubmit}/>
                            <Divider horizontal>Or</Divider>
                            <LinkAsButton to={routesConstants.LOGIN} className="fluid secondary">
                                Cancel and return to login!
                            </LinkAsButton>
                        </Card>
                    </div>
                </div>
            </Titled>
        )
    }
}

export default connect()(withRouter(StartResetContainer))
