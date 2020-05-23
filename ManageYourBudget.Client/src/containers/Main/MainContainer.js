import React from 'react'
import { connect } from 'react-redux';
import profileActions from '../../actions';
import bindActionCreators from 'redux/src/bindActionCreators';
import { profileApi } from '../../api';
import Navbar from '../../components/Navigation/Navigation'
import { Route } from 'react-router-dom';
import { routesConstants } from '../../routing';
import ProfileContainer from '../Profile/ProfileContainer';
import { Grid } from 'semantic-ui-react';
import NewWalletContainer from '../NewWallet/NewWalletContainer';

class MainContainer extends React.Component {
    componentDidMount() {
        profileApi.getProfile().then(profile => {
            this.props.actions.getProfile(profile);
        });
    }

    render() {
        return (
            <div>
                <Navbar user={this.props.profile}>
                    <Grid className="full-height">
                        <Grid.Column width={2}>
                        </Grid.Column>
                        <Grid.Column width={12}>
                            <Route
                                path={routesConstants.PROFILE}
                                component={ProfileContainer}
                            />
                            <Route
                                exact
                                path={routesConstants.WALLET}
                                component={NewWalletContainer}
                            />
                        </Grid.Column>
                        <Grid.Column width={2}>
                        </Grid.Column>
                    </Grid>
                </Navbar>
            </div>
        )
    }
}

const mapDispatchToProps = dispatch => {
    return {actions: bindActionCreators(profileActions, dispatch)};
};

const mapStateToProps = state => {
    return {profile: state.profile}
};

export default connect(mapStateToProps, mapDispatchToProps)(MainContainer);