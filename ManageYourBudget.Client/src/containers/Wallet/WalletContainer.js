import Titled from '../../components/Titled/titled';
import React from 'react';
import CustomSpiner from '../../components/common/customSpinner';
import { searchApi, walletApi } from '../../api';
import { withRouter } from 'react-router';
import { Icon, Popup } from 'semantic-ui-react';
import DropdownMenu from '../../components/common/buttons/dropdownMenu';
import EditWalletModal from '../../components/Wallet/editWalletModal';
import bindActionCreators from 'redux/src/bindActionCreators';
import profileActions from '../../actions';
import { connect } from 'react-redux';
import { startSubmit, stopSubmit } from 'redux-form';
import { toastrService } from '../../common';
import walletHelper from '../../common/walletHelper';
import WalletParticipantsModal from '../../components/Wallet/walletPartipantsModal';
import { assignmentToWalletApi } from '../../api';
import ExpenseWrapper from '../../components/Expense/ExpenseWrapper';

const EDIT_WALLET_FORM = 'newWalletForm';

class WalletContainer extends React.Component {
    state = {
        wallet: {},
        loaded: false,
        menuOpened: false,
        isEditModalOpened: false,
        isPartipantsModalOpened: false,
        searchResults: {
            results: [],
            startedSearch: false
        }
    };

    async componentDidMount() {
        try {
            const wallet = await walletApi.get(this.props.match.params.id);
            wallet.participants = wallet.participants.map(participant => flatten(participant));
            this.setState({wallet, loaded: true})
        }
        catch (err) {
        }
    }

    toggleEditModal = () => {
        this.setState({isEditModalOpened: !this.state.isEditModalOpened});
    };

    toggleParticipantsModal = () => {
        this.setState({isPartipantsModalOpened: !this.state.isPartipantsModalOpened});
    };

    searchPeople = async (searchTerm) => {
        const searchOptions = {
            searchTerm
        };

        const searchResults = await searchApi.searchUsers(searchOptions);

        searchResults.results = searchResults.results.map(member => {
            member.added = this.state.wallet.participants.some(x => x.id === member.id);
            return member;
        });

        this.setState({
            searchResults: {...searchResults, startedSearch: true}
        });
    };

    handleEdit = async (editWalletData) => {
        this.props.dispatch(startSubmit(EDIT_WALLET_FORM));
        try {
            await walletApi.update(editWalletData, this.state.wallet.id);
            toastrService.success('Wallet modified successfully!');
            this.setState(prevState => {
                const wallet = {
                    ...prevState.wallet,
                    ...editWalletData,
                    defaultCurrency: walletHelper.mapValueCurrencyToString(editWalletData.currency),
                    category: walletHelper.mapValueCategoryToString(editWalletData.category),
                };
                return {
                    wallet,
                    isEditModalOpened: false
                }
            });
        }
        catch (error) {
            toastrService.error('Something went wrong. Try again!');
            this.props.dispatch(stopSubmit(EDIT_WALLET_FORM));
        }
    };

    onAdded = async (participant) => {
        await this.setState(prevState => {
            const results = prevState.searchResults.results.map(res => {
                if (res.id === participant.id) {
                    res.added = !res.added;
                    res.loading = true;
                }
                return res;
            });
            return {searchResults: {...prevState.searchResults, results}}
        });

        const data = {userId: participant.id, walletId: this.state.wallet.id};

        participant.added ? await assignmentToWalletApi.assign(data) : await assignmentToWalletApi.unassign(data);

        await this.setState(prevState => {
            participant.role = walletHelper.defaultRole;
            const participants = participant.added ?
                [...prevState.wallet.participants, participant]
                : prevState.wallet.participants.filter(x => x.id !== participant.id);

            const results = prevState.searchResults.results.map(res => {
                if (res.id === participant.id) {
                    res.loading = false;
                }
                return res;
            });
            return {
                wallet: {...prevState.wallet, participants: [...participants]},
                searchResults: {...prevState.searchResults, results}
            }
        });
    };

    editParticipant = async (participant, role) => {
        await this.setLoadingStateOnParticipant(participant, true);
        try {
            const mappedRole = walletHelper.mapValueRoleToString(role);
            const data = {userId: participant.id, walletId: this.state.wallet.id, role: mappedRole};
            await assignmentToWalletApi.changeRole(data);
            await this.setState(prevState => {
                const participants = prevState.wallet.participants.map(part => {
                    if (part.id === participant.id) {
                        part.loading = false;
                        part.role = mappedRole;
                    }
                    return part;
                });
                return {wallet: {...prevState.wallet, participants}}
            });
        }
        catch(e) {
            await this.setLoadingStateOnParticipant(participant, false);
        }
    };

    setLoadingStateOnParticipant = async (participant, status) => {
        await this.setState(prevState => {
            const participants = prevState.wallet.participants.map(part => {
                if (part.id === participant.id) {
                    part.loading = status;
                }
                return part;
            });
            return {wallet: {...prevState.wallet, participants}}
        });
    };


    createMenu = () => {
        const menu = [<div onClick={this.toggleParticipantsModal}>People</div>];
        if (walletHelper.hasAllPrivileges(this.state.wallet.role)) {
            menu.unshift(<div onClick={this.toggleEditModal}>Modify</div>)
        }
        return menu;
    };

    toggleMenu = () => {
        this.setState({menuOpened: !this.state.menuOpened})
    };

    render() {
        const {wallet, loaded, isEditModalOpened, isPartipantsModalOpened, searchResults} = this.state;
        const menu = this.createMenu();
        return (
            <Titled title={loaded ? wallet.name : ''}>
                <div>
                    {loaded &&
                    <ExpenseWrapper walletId={this.state.wallet.id} defaultCurrency={walletHelper.mapStringCurrencyToValue(wallet.defaultCurrency)}/>
                    }
                    <CustomSpiner active={!loaded}/>
                    <div className="row-space-between">
                        <h2>{wallet.name}</h2>
                        <Popup
                            trigger={<Icon link name="ellipsis vertical"/>}
                            content={<DropdownMenu onClick={this.toggleMenu} options={menu}/>}
                            on='click'
                            onOpen={this.toggleMenu}
                            onClose={this.toggleMenu}
                            open={this.state.menuOpened}
                            position='left center'
                        />
                    </div>
                    {loaded &&
                    <EditWalletModal onSave={this.handleEdit} isOpen={isEditModalOpened} onClose={this.toggleEditModal}
                                     wallet={wallet}/>
                    }
                    {loaded &&
                    <WalletParticipantsModal
                        onEdit={this.editParticipant}
                        hasAllPrivileges={walletHelper.hasAllPrivileges(this.state.wallet.role)}
                        participants={wallet.participants}
                        onDelete={this.onAdded}
                        searchResults={searchResults}
                        onSearch={this.searchPeople}
                        onAdded={this.onAdded}
                        isOpen={isPartipantsModalOpened}
                        onClose={this.toggleParticipantsModal}/>
                    }
                </div>
            </Titled>
        )
    }
}

const mapDispatchToProps = dispatch => {
    return {actions: bindActionCreators(profileActions, dispatch), dispatch};
};

const flatten = object => {
    return Object.assign({}, ...function flattenInternal(objToFlatten) {
        return [].concat(
            ...Object.keys(objToFlatten).map(
                key => typeof objToFlatten[key] === 'object' ? flattenInternal(objToFlatten[key], key) : ({[key]: objToFlatten[key]})
            )
        )
    }(object));
};

export default connect(null, mapDispatchToProps)(withRouter(WalletContainer))