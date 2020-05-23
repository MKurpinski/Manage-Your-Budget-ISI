import React from 'react';
import { toastrService } from '../../common';
import { startSubmit, stopSubmit } from 'redux-form';
import { walletApi } from '../../api';
import Titled from '../../components/Titled/titled';
import { withRouter } from 'react-router-dom';
import NewWalletForm from '../../components/Wallet/newWalletForm';
import { connect } from 'react-redux';
import routesConstants from '../../routing/routesConstants';

const NEW_WALLET_FORM = 'newWalletForm';

class NewWalletContainer extends React.Component {

    createWallet = async (newWalletInfo) => {
        this.props.dispatch(startSubmit(NEW_WALLET_FORM));
        try {
            const newWalletId = await walletApi.createNew(newWalletInfo);
            toastrService.success('You have successfully added new wallet!');
            this.props.history.push(`${routesConstants.WALLET}/${newWalletId}`)
        }
        catch (error) {
            toastrService.error('Something went wrong. Try again!');
            this.props.dispatch(stopSubmit(NEW_WALLET_FORM));
        }
    };

    render() {
        return (
            <Titled title="New Wallet">
                <div className="centered-column full-height">
                    <h1 className="auth-title">Create new wallet</h1>
                    <NewWalletForm onSubmit={this.createWallet}/>
                </div>
            </Titled>
        )
    }
}


export default connect()(withRouter(NewWalletContainer));