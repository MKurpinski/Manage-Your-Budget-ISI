import React, { Fragment } from 'react';
import { Icon } from 'semantic-ui-react';
import ModifyExpenseModal from './modifyExpenseModal';
import { currencyApi, cyclicExpenseApi, expenseApi } from '../../api';
import walletHelper from '../../common/walletHelper';
import { bindActionCreators } from 'redux';
import { change, startSubmit, stopSubmit } from 'redux-form';
import { connect } from 'react-redux';
import { toastrService } from '../../common';
import AdvancedExpenseSearch from './expenseSearchWrapper';
import ModifyCyclicExpenseModal from './modifyCyclicExpenseModal';
import moment from 'moment';
import CyclicExpenseWrapper from '../CyclicExpense/cyclicExpenseWrapper';
import { helpers } from '../../common';
import { FORMS } from '../../common/constants';

class ExpenseWrapper extends React.Component {
    state = {
        isAddingModalOpened: false,
        isAddingCyclicModalOpened: false,
    };

    toggleModal = (property) => {
        this.setState(prevState => {
            return {[property]: !prevState[property]}
        })
    };

    toggleAdding = () => this.toggleModal('isAddingModalOpened');
    toggleCyclicAdding = () => this.toggleModal('isAddingCyclicModalOpened');

    addExpense = async (expenseData, isFromCyclic) => {
        this.addBaseExpenseWrapper(async () => {
            const dataToSave = helpers.prepareExpenseToSave(expenseData, this.props.walletId);
            const added = await expenseApi.save(dataToSave);

            if (isFromCyclic !== true) {
                this.toggleAdding();
            }

            toastrService.success(`Successfully added ${isFromCyclic === true ? 'normal expense from cyclic' : ''}`);
            this.props.onAdded(added);
        });
    };

    addCyclicExpense = async (cyclicExpenseData) => {
        this.addBaseExpenseWrapper(async () => {
            const dataToSave = helpers.prepareExpenseToSave(cyclicExpenseData, this.props.walletId);
            const added = await cyclicExpenseApi.save(dataToSave);

            toastrService.success('Successfully added cyclic expense');
            if (moment().isSame(cyclicExpenseData.startingFrom, 'day')) {
                await this.addExpense({ ...cyclicExpenseData, cyclicExpenseId: added.id }, true);
            }
            this.toggleCyclicAdding();
        });
    };

    addBaseExpenseWrapper = async (func) => {
        this.props.dispatch(startSubmit(FORMS.MODIFY_EXPENSE_FORM));
        try {
            await func();
        }
        catch (error) {
            this.props.dispatch(stopSubmit(FORMS.MODIFY_EXPENSE_FORM));
        }
    };

    removeExpense = async (expense) => {
        try {
            await expenseApi.remove(expense.id);
            this.props.onExpenseDelete(expense);
            toastrService.success('Successfully deleted');
        }
        catch (error) {
            toastrService.error('Something went wrong!', 'Ups!');
        }
    };

    editExpense = async (editedDataOriginal, originalExpense) => {
        try {
            let editedData = {...editedDataOriginal};
            editedData = helpers.prepareExpenseToSave(editedData, this.props.walletId);

            await expenseApi.update(editedData, originalExpense.id);

            editedData = helpers.prepareExpenseToDisplay(editedData);

            this.props.onExpenseUpdated({...originalExpense, ...editedData});
            toastrService.success('Successfully modified');
        }
        catch (error) {
            toastrService.error('Something went wrong!', 'Ups!');
        }
    };

    downloadExchangeRate = async (currency) => {
        try {
            const response = await currencyApi.get(
                walletHelper.mapValueCurrencyToString(currency),
                walletHelper.mapValueCurrencyToString(this.props.defaultCurrency)
            );
            this.props.change(FORMS.MODIFY_EXPENSE_FORM, 'rate', response.rate)
        }
        catch (err) {
            this.props.change(FORMS.MODIFY_EXPENSE_FORM, 'currency', this.props.defaultCurrency);
            toastrService.error('We have problem with exchange rate system. You can only add value in default currency :(', 'Ups!');
        }
    };

    render() {
        return (
            <div>
                <ModifyExpenseModal onSave={this.addExpense} downloadCurrency={this.downloadExchangeRate}
                                    isOpen={this.state.isAddingModalOpened}
                                    defaultCurrency={this.props.defaultCurrency}
                                    onClose={this.toggleAdding}
                                    />

                {
                    this.props.isCyclicExpensesModalOpened &&
                    <CyclicExpenseWrapper onClose={this.props.toggleCyclicExpenseModal}
                                          isOpen={this.props.isCyclicExpensesModalOpened}
                                          walletRole={this.props.walletRole}
                                          currency={this.props.defaultCurrency}
                                          downloadCurrency={this.downloadExchangeRate}
                                          walletId={this.props.walletId}/>
                }


                <ModifyCyclicExpenseModal onSave={this.addCyclicExpense} downloadCurrency={this.downloadExchangeRate}
                                          isOpen={this.state.isAddingCyclicModalOpened}
                                          onClose={this.toggleCyclicAdding}
                                          defaultCurrency={this.props.defaultCurrency}/>

                <div className="section-padding" style={{display: 'flex', justifyContent: 'flex-end'}}>
                    <p style={{marginTop: '5px'}}>Add</p>
                    <Icon size='big' link name="add circle" onClick={this.toggleAdding}/>
                    {walletHelper.hasAllPrivileges(this.props.walletRole) &&
                        <Fragment>
                            <p style={{marginTop: '5px', marginLeft: '5px'}}>Add cyclic</p>
                            <Icon.Group onClick={this.toggleCyclicAdding} size='big'>
                                <Icon link name='add circle'/>
                                <Icon link corner name='repeat'/>
                            </Icon.Group>
                        </Fragment>
                    }
                </div>
                <div className="section-padding">
                    <AdvancedExpenseSearch
                        onExpenseEdited={this.editExpense}
                        downloadCurrency={this.downloadExchangeRate}
                        onExpenseDelete={this.removeExpense}
                        currency={this.props.defaultCurrency}
                        expenses={this.props.expenses}
                        walletId={this.props.walletId}
                        onSearch={this.props.onExpenseSearch}
                    />
                </div>
            </div>);
    }
}

const mapDispatchToProps = (dispatch) => {
    return {...bindActionCreators({change}, dispatch), dispatch};
};

export default connect(null, mapDispatchToProps)(ExpenseWrapper)