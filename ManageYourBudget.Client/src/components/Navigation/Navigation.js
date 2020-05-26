import React, { Component, Fragment } from 'react';
import { Icon, Menu, Responsive, Sidebar } from 'semantic-ui-react';
import SimpleLink from '../common/buttons/simpleLink';
import { routesConstants } from '../../routing';
import './Navigation.css'
import SimpleButton from '../common/buttons/simpleButton';
import UserInfo from '../UserInfo/userInfo';
import { authApi } from '../../api';
import authProvider from '../../authProvider';
import { withRouter } from 'react-router-dom';

const TABLET_WIDTH = 800;

const NavBarMobile = ({onPusherClick, onToggle, visible, onLogout, user, children}) => (
    <Sidebar.Pushable>
        <Sidebar
            as={Menu}
            animation="overlay"
            icon="labeled"
            inverted
            vertical
            visible={visible}
        >
            <div className="custom-sidebar-mobile">
                <div>
                    <LogoPanel isSidebar={true}/>
                    <LeftPanel closePusher={onPusherClick}/>
                </div>
                <div>
                    <RightPanel closePusher={onPusherClick} user={user} onLogout={onLogout}/>
                </div>
            </div>
        </Sidebar>
        <Sidebar.Pusher
            dimmed={visible}
            onClick={onPusherClick}
            style={{minHeight: '100vh'}}
        >
            <Menu className="no-border-radius" inverted>
                <LogoPanel/>
                <Menu.Menu position="right">
                    <Menu.Item link>
                        <Icon name="sidebar" onClick={onToggle}/>
                    </Menu.Item>
                </Menu.Menu>
            </Menu>
            {children}
        </Sidebar.Pusher>
    </Sidebar.Pushable>
);

const NavBarDesktop = ({onLogout, user}) => (
    <Menu className="no-border-radius" inverted>
        <LogoPanel/>
        <LeftPanel/>
        <Menu.Menu position="right">
            <RightPanel onLogout={onLogout} user={user}/>
        </Menu.Menu>
    </Menu>
);

const LeftPanel = ({closePusher}) => {
    closePusher = closePusher ? closePusher : () => {
    };
    return (
        <Fragment>
            <SimpleLink onClick={closePusher} className="item" to={routesConstants.WALLET}>New wallet</SimpleLink>
        </Fragment>
    )
};

const RightPanel = ({onLogout, user, closePusher}) => {
    closePusher = closePusher ? closePusher : () => {
    };
    return (
        <Fragment>
            <div className="item" onClick={closePusher}>
                <UserInfo user={user}/>
            </div>
            <Menu.Item>
                <SimpleButton onClick={onLogout}>Log out</SimpleButton>
            </Menu.Item>
        </Fragment>
    )
};

const LogoPanel = ({isSidebar}) => {
    const iconStyles = {paddingLeft: !isSidebar ? '10px' : '0', paddingTop: isSidebar ? '10px' : '0'};
    return (
        <Menu.Item>
            <SimpleLink to={routesConstants.MAIN}>
                <span className="main-header">Manage Your Budget</span> <Icon style={iconStyles}
                                                                              name="money bill alternate outline"/>
            </SimpleLink>
        </Menu.Item>
    )
};

class NavBar extends Component {
    state = {
        visible: false
    };

    onLogout = async () => {
        const logoutData = await authApi.logout();
        authProvider.removeToken();
        logoutData.externallyLoggedOut ? (window.location.href = logoutData.uri) : this.props.history.push(routesConstants.LOGIN);
    };

    handleToggle = () => this.setState({visible: !this.state.visible});

    handlePusherClick = () => {
        if (this.state.visible) {
            this.setState({visible: false});
        }
    };

    render() {
        const {visible} = this.state;

        return (
            <div className="navigation">
                <Responsive maxWidth={TABLET_WIDTH}>
                    <NavBarMobile
                        onPusherClick={this.handlePusherClick}
                        onToggle={this.handleToggle}
                        visible={visible}
                        onLogout={this.onLogout}
                        user={this.props.user}
                    >
                        <div style={{minHeight: '90vh'}}>
                            {this.props.children}
                        </div>
                    </NavBarMobile>
                </Responsive>
                <Responsive minWidth={TABLET_WIDTH}>
                    <NavBarDesktop user={this.props.user} onLogout={this.onLogout}/>
                    <div style={{minHeight: '90vh'}}>
                        {this.props.children}
                    </div>
                </Responsive>
            </div>
        );
    }
}

export default withRouter(NavBar)
