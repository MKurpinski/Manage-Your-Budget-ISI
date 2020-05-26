import React from 'react';
import { Button } from 'semantic-ui-react';
import ModifyExpenseModal from './modifyExpenseModal';
import { expenseApi, currencyApi } from '../../api';
import walletHelper from '../../common/walletHelper';
import { bindActionCreators } from 'redux';
import { change, startSubmit, stopSubmit } from 'redux-form';
import { connect } from 'react-redux';
import toastrWrapper from '../../common/toastrWrapper';
import { toastrService } from '../../common';

const EXPENSE_FORM = 'modifyExpenseForm';

class ExpenseWrapper extends React.Component {
    state = {
        isAddingModalOpened: false,
    };

    toggleAdding = () => {
        this.setState({isAddingModalOpened: !this.state.isAddingModalOpened});
    };

    addExpense = async (expenseData) => {
        this.props.dispatch(startSubmit(EXPENSE_FORM));
        try {
            await expenseApi.save({...expenseData, price: (expenseData.price * expenseData.rate).toFixed(2), walletId: this.props.walletId});
            toastrService.success('Successfully added');
        }
        catch (error) {
            this.props.dispatch(stopSubmit(EXPENSE_FORM));
        }
    };

    render() {
        return (<div>
            <Button onClick={this.toggleAdding}/>
            <ModifyExpenseModal onSave={this.addExpense} downloadCurrency={this.downloadExchangeRate} isOpen={this.state.isAddingModalOpened}
                                onClose={this.toggleAdding} defaultCurrency={this.props.defaultCurrency}/>
        </div>);
    }

    downloadExchangeRate = async (currency) => {
        try {
            const response = await currencyApi.get(walletHelper.mapValueCurrencyToString(currency), walletHelper.mapValueCurrencyToString(this.props.defaultCurrency));
            this.props.change(EXPENSE_FORM, 'rate', response.rate)
        }
        catch(err) {
            this.props.change(EXPENSE_FORM, 'currency', this.props.defaultCurrency);
            toastrWrapper.error('We have problem exchange rate system. You can only add value in default currency :(', 'Ups!');
        }

    }
}

const mapDispatchToProps = (dispatch) => {
    return{...bindActionCreators({change}, dispatch), dispatch};
};

export default connect(null, mapDispatchToProps)(ExpenseWrapper)