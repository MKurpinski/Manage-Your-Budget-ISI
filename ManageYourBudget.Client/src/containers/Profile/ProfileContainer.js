import React from 'react';
import profileActions from '../../actions';
import bindActionCreators from 'redux/src/bindActionCreators';
import { connect } from 'react-redux';
import ProfileForm from '../../components/Profile/profileForm';
import { toastrService } from '../../common';
import { startSubmit, stopSubmit } from 'redux-form';
import { profileApi } from '../../api';
import { Card, Grid } from 'semantic-ui-react';
import CustomSpinner from '../../components/common/customSpinner';
import Titled from '../../components/Titled/titled';
import ChangePasswordForm from '../../components/Profile/changePasswordForm';
import AddPasswordForm from '../../components/Profile/addPasswordForm';

const PROFILE_FORM = 'profileForm';
const CHANGE_PASSWORD_FORM = 'changePasswordForm';
const ADD_PASSWORD_FORM = 'addPasswordForm';

class ProfileContainer extends React.Component {

    savePersonalInfo = async (personalInfoData) => {
        this.props.dispatch(startSubmit(PROFILE_FORM));
        try {
            await profileApi.changeProfileInfo(personalInfoData);
            toastrService.success('You have successfully changed the data!');
            this.props.actions.editProfile(personalInfoData);
        }
        catch (error) {
            toastrService.error('Something went wrong. Try again!');
            this.props.dispatch(stopSubmit(PROFILE_FORM));
        }
    };

    changePassword = async (changePasswordData) => {
        this.props.dispatch(startSubmit(CHANGE_PASSWORD_FORM));
        try {
            await profileApi.changePassword(changePasswordData);
            toastrService.success('You have successfully changed password!');
        }
        catch (error) {
            toastrService.error('Your old password is not correct.');
            this.props.dispatch(stopSubmit(PROFILE_FORM));
        }
    };

    addPassword = async (addPasswordData) => {
        this.props.dispatch(startSubmit(ADD_PASSWORD_FORM));
        try {
            await profileApi.addPassword(addPasswordData);
            toastrService.success('You have successfully created local account!');
            this.props.actions.editProfile({hasLocalAccount: true});
        }
        catch (error) {
            toastrService.error('Something went wrong. Try again!');
            this.props.dispatch(stopSubmit(PROFILE_FORM));
        }
    };

    render() {
        return (
            <Titled title="Profile">
                <div>
                    <h1 className="auth-title">Profile</h1>
                    <Grid>
                        <Grid.Column mobile={16} tablet={8} computer={8}>
                            <Card fluid centered className="form-wrapper--card">
                                <h2>Profile Info</h2>
                                <ProfileForm onSubmit={this.savePersonalInfo} initial={this.props.profile}
                                             user={this.props.profile}/>
                            </Card>
                        </Grid.Column>
                        <Grid.Column mobile={16} tablet={8} computer={8}>
                            <Card fluid centered className="form-wrapper--card">
                                {this.props.profile.hasLocalAccount ?
                                    <div>
                                        <h2>Change password</h2>
                                        <ChangePasswordForm onSubmit={this.changePassword}/>
                                    </div>
                                    :
                                    <div>
                                        <h2>Create local account</h2>
                                        <AddPasswordForm onSubmit={this.addPassword}/>
                                    </div>
                                }
                            </Card>
                        </Grid.Column>
                        <CustomSpinner active={!this.props.profile.loaded}/>
                    </Grid>
                </div>
            </Titled>
        )
    }
}


const mapDispatchToProps = dispatch => {
    return {actions: bindActionCreators(profileActions, dispatch), dispatch};
};

const mapStateToProps = state => {
    return {profile: state.profile}
};

export default connect(mapStateToProps, mapDispatchToProps)(ProfileContainer)