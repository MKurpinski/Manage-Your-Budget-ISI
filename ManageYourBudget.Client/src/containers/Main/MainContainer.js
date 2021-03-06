import React from 'react'
import { connect } from 'react-redux';
import profileActions from '../../actions';
import { bindActionCreators } from 'redux';
import { profileApi } from '../../api';
import Navbar from '../../components/Navigation/Navigation'
import { Route, withRouter } from 'react-router-dom';
import { routesConstants } from '../../routing';
import ProfileContainer from '../Profile/ProfileContainer';
import { Grid } from 'semantic-ui-react';
import NewWalletContainer from '../NewWallet/NewWalletContainer';
import FirstWalletContainer from '../NewWallet/FirstWalletContainer';
import MyWalletsContainer from '../MyWallets/MyWalletsContainer';
import WalletContainer from '../Wallet/WalletContainer';

class MainContainer extends React.Component {
    componentDidMount() {
        profileApi.getProfile().then(profile => {
            const {hasAnyWallet, ...rest} = profile;
            this.props.actions.getProfile(rest);
            if (!hasAnyWallet) {
                this.props.history.push(routesConstants.FIRST_WALLET);
            }
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
                            <Route
                                exact
                                path={routesConstants.FIRST_WALLET}
                                component={FirstWalletContainer}
                            />
                            <Route
                                exact
                                path={routesConstants.MAIN}
                                component={MyWalletsContainer}
                            />
                            <Route
                                path={`${routesConstants.WALLET}/:id`}
                                component={WalletContainer}
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

export default connect(mapStateToProps, mapDispatchToProps)(withRouter(MainContainer))