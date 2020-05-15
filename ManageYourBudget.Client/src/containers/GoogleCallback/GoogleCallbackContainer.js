import React from 'react';
import { withRouter } from 'react-router-dom';
import { authApi } from '../../api';
import { toastrService } from '../../common';
import authProvider from '../../authProvider';
import { routesConstants } from '../../routing';
import CustomSpiner from '../../components/common/customSpiner';

class GoogleCallbackContainer extends React.Component {

    async componentDidMount() {
        await this.googleLogin();
    }

    parseQueryUrl = () => {
        const query = new URLSearchParams(this.props.location.search);
        return {state: query.get('state'), code: query.get('code')};
    };

    googleLogin = async () => {
        try {
            const loginResponse = await authApi.loginWithGoogle(this.parseQueryUrl())
            authProvider.saveToken(loginResponse.value.token);
            this.props.history.push(routesConstants.MAIN);
        }
        catch (err) {
            toastrService.error('Login with Google+ has failed');
            this.props.history.push(routesConstants.LOGIN);
        }
    };

    render() {
        return <CustomSpiner active={true}/>
    }
}

export default withRouter(GoogleCallbackContainer)