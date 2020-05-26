import React from 'react';
import CustomSpiner from '../../components/common/customSpinner';
import { walletApi } from '../../api';
import { toastrService, walletHelper } from '../../common';
import Titled from '../../components/Titled/titled';
import ConfirmationModal from '../../components/common/confirmationModal';
import WalletList from '../../components/Wallet/walletList';
import { withRouter } from 'react-router-dom';
import { routesConstants } from '../../routing';

class MyWalletsContainer extends React.Component {
    state = {
        wallets: [],
        deleteModalData: {
            content: '',
            header: '',
            isOpened: false
        },
        loaded: false
    };
    TOP_WALLETS = 3;

    async componentDidMount() {
        try {
            const wallets = await walletApi.getAll();
            await this.setState({wallets, loaded: !!wallets.length});
            if(!this.state.loaded){
                this.props.history.push(routesConstants.FIRST_WALLET);
            }
        }
        catch (err) {
            toastrService.error('Something went wrong!');
        }
    }

    onReject = () => {
        this.setState(
            {deleteModalData: {...this.state.deleteModalData, isOpened: false}}
        );
    };

    deleteWallet = async (wallet) => {
        const content = walletHelper.hasAllPrivileges(wallet.role) ?
            `Are you sure you want to archive '${wallet.name}' wallet?`
            :
            `Are you sure you want to unassign '${wallet.name}' from wallet?`;
        const header = walletHelper.hasAllPrivileges(wallet.role) ? 'Archive wallet' : 'Unassign from wallet';
        const onConfirm = this.confirmDelete(wallet);
        const onReject = this.onReject;
        this.setState(
            {deleteModalData: {...this.state.deleteModalData, isOpened: true, onReject, content, header, onConfirm}}
        );
    };

    confirmDelete = (wallet) => {
        const message = walletHelper.hasAllPrivileges(wallet.role) ? 'archived' : 'unassigned from';
        const id = wallet.id;
        return async () => {
            try {
                let redirect = false;
                await walletApi.archive(id);
                this.setState(prevState => {
                    const wallets = prevState.wallets.filter(x => x.id !== id);
                    redirect = !wallets.length;
                    return {wallets, deleteModalData: {...prevState.deleteModalData, isOpened: false}}
                });
                if (redirect) {
                    this.props.history.push(routesConstants.FIRST_WALLET);
                }
                toastrService.success(`Successfully ${message} ${wallet.name}`);
            }
            catch (err) {
                toastrService.error('Something went wrong!');
            }
        }
    };

    changeFavorite = async (wallet) => {
        try {
            await walletApi.starWallet(wallet.id);
            this.setState(prevState => {
                const wallets = prevState.wallets.map((x) => {
                    if (x.id === wallet.id) {
                        x.favorite = !x.favorite;
                    }
                    return x;
                });
                return {wallets};
            });
        }
        catch (err) {
            toastrService.error('Something went wrong');
        }
    };

    render() {
        if(!this.state.loaded){
            return <CustomSpiner active={true}/>
        }
        return (
            <Titled title="My wallets">
                <div>
                    <h1>Wallets</h1>
                    <ConfirmationModal {...this.state.deleteModalData}/>
                    <WalletList title="Last opened" onDelete={this.deleteWallet} changeFavorite={this.changeFavorite}
                                wallets={this.state.wallets.slice(0, this.TOP_WALLETS)}/>
                    <WalletList title="Favorites" onDelete={this.deleteWallet} changeFavorite={this.changeFavorite}
                                wallets={this.state.wallets.filter(x => x.favorite)}/>
                    <WalletList title="Created by me" onDelete={this.deleteWallet} changeFavorite={this.changeFavorite}
                                wallets={this.state.wallets.filter(x => walletHelper.createdByMe(x.role))}/>
                    <WalletList title="I'm admin" onDelete={this.deleteWallet} changeFavorite={this.changeFavorite}
                                wallets={this.state.wallets.filter(x => walletHelper.adminRole(x.role))}/>
                    <WalletList title="Assigned to" onDelete={this.deleteWallet} changeFavorite={this.changeFavorite}
                                wallets={this.state.wallets.filter(x => !walletHelper.hasAllPrivileges(x.role))}/>
                </div>
            </Titled>
        )
    }
}

export default withRouter(MyWalletsContainer);