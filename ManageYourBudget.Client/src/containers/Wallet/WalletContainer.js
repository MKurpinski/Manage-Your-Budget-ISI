import Titled from '../../components/Titled/titled';
import React from 'react';
import CustomSpiner from '../../components/common/customSpinner';
import { assignmentToWalletApi, searchApi, walletApi } from '../../api';
import { withRouter } from 'react-router';
import EditWalletModal from '../../components/Wallet/editWalletModal';
import { bindActionCreators } from 'redux';
import profileActions from '../../actions';
import { connect } from 'react-redux';
import { startSubmit, stopSubmit } from 'redux-form';
import { toastrService } from '../../common';
import walletHelper from '../../common/walletHelper';
import WalletParticipantsModal from '../../components/Wallet/walletPartipantsModal';
import ExpenseWrapper from '../../components/Expense/ExpenseWrapper';
import { helpers } from '../../common/index';
import queryString from 'query-string'
import WalletMenu from '../../components/Menu/walletMenu';
import { FORMS } from '../../common/constants';
import ChartContainer from '../Chart/chartContainer';

class WalletContainer extends React.Component {
    state = {
        wallet: {},
        loaded: false,
        menuOpened: false,
        isEditModalOpened: false,
        isPartipantsModalOpened: false,
        isChartModalOpened: false,
        isCyclicExpensesModalOpened: false,
        searchResults: {
            results: [],
            startedSearch: false
        },
        expenses: []
    };

    async componentDidMount() {
        try {
            const wallet = await walletApi.get(this.props.match.params.id);
            wallet.participants = wallet.participants.map(participant => helpers.flatten(participant));
            this.setState({wallet, loaded: true})
        }
        catch (err) {
        }
    }

    toggleModal = (property) => {
        return () => {
            this.setState(prevState => {
                return {[property]: !prevState[property]}
            })
        }
    };

    toggleEditModal = this.toggleModal('isEditModalOpened');
    toggleMenu = this.toggleModal('menuOpened');
    toggleParticipantsModal = this.toggleModal('isPartipantsModalOpened');
    toggleCyclicExpenseModalModal = this.toggleModal('isCyclicExpensesModalOpened');
    toggleChartModal = this.toggleModal('isChartModalOpened');

    searchPeople = async (searchTerm) => {
        const searchResults = await searchApi.searchUsers({searchTerm});

        searchResults.results = searchResults.results.map(member => {
            member.added = this.state.wallet.participants.some(x => x.id === member.id);
            return member;
        });

        this.setState({
            searchResults: {
                results: searchResults.results.map(member => {
                    return {...member, added: this.state.wallet.participants.some(x => x.id === member.id)}
                })
                , startedSearch: true
            }
        });
    };

    handleEdit = async (editWalletData) => {
        this.props.dispatch(startSubmit(FORMS.EDIT_WALLET_FORM));
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
            this.props.dispatch(stopSubmit(FORMS.EDIT_WALLET_FORM));
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
        catch (e) {
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

    onExpenseAdded = (expense) => {
        this.setState(prevState => {
            return {
                expenses: [expense, ...prevState.expenses]
            }
        });
    };

    onExpenseSearch = ({searchResults, searchParams, strategy, isInit}) => {
        if (!isInit) {
            this.props.history.push(`?${queryString.stringify(searchParams)}`);
        }
        if (strategy === walletHelper.appendStrategy.REPLACE) {
            this.setState({expenses: searchResults})
        }
        else {
            this.setState(prevState => {
                return {
                    expenses: [...prevState.expenses, ...searchResults]
                }
            })
        }
    };

    onExpenseDelete = (expenseToDelete) => {
        this.setState(prevState => {
            return {
                expenses: prevState.expenses.filter(x => x.id !== expenseToDelete.id)
            }
        })
    };

    onExpenseUpdated = (editedData) => {
        editedData.price = +editedData.price;
        this.setState(prevState => {
            return {
                expenses: prevState.expenses.map(expense => {
                    return expense.id === editedData.id ? editedData : expense
                })
            }
        })
    };

    createMenu = () => {
        const menu = [
            <div onClick={this.toggleParticipantsModal}>People</div>,
            <div onClick={this.toggleCyclicExpenseModalModal}>Cyclic operations</div>,
            <div onClick={this.toggleChartModal}>Charts</div>
        ];
        if (walletHelper.hasAllPrivileges(this.state.wallet.role)) {
            menu.unshift(<div onClick={this.toggleEditModal}>Modify</div>)
        }
        return menu;
    };

    render() {
        const {wallet, loaded, isEditModalOpened, isPartipantsModalOpened, searchResults} = this.state;
        const menu = this.createMenu();
        return (
            <Titled title={loaded ? wallet.name : ''}>
                <div>
                    {loaded &&
                    <div>
                        <WalletMenu
                            onClick={this.toggleMenu}
                            name={wallet.name}
                            options={menu}
                            onOpen={this.toggleMenu}
                            onClose={this.toggleMenu}
                            open={this.state.menuOpened}
                        />

                        <ExpenseWrapper
                            onExpenseDelete={this.onExpenseDelete}
                            onExpenseSearch={this.onExpenseSearch}
                            onExpenseUpdated={this.onExpenseUpdated}
                            walletId={this.state.wallet.id}
                            walletRole={this.state.wallet.role}
                            onAdded={this.onExpenseAdded}
                            expenses={this.state.expenses}
                            isCyclicExpensesModalOpened={this.state.isCyclicExpensesModalOpened}
                            toggleCyclicExpenseModal={this.toggleCyclicExpenseModalModal}
                            defaultCurrency={walletHelper.mapStringCurrencyToValue(wallet.defaultCurrency)}/>

                        <EditWalletModal
                            onSave={this.handleEdit}
                            isOpen={isEditModalOpened}
                            onClose={this.toggleEditModal}
                            wallet={wallet}/>

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

                        {this.state.isChartModalOpened &&
                        <ChartContainer walletId={this.state.wallet.id}
                                        isOpen={this.state.isChartModalOpened}
                                        onClose={this.toggleChartModal}/>
                        }                    </div>
                    }
                    <CustomSpiner active={!loaded}/>
                </div>
            </Titled>
        )
    }
}

const mapDispatchToProps = dispatch => {
    return {actions: bindActionCreators(profileActions, dispatch), dispatch};
};

export default connect(null, mapDispatchToProps)(withRouter(WalletContainer))