import React from 'react';
import { Button, Divider, Segment } from 'semantic-ui-react';
import { walletHelper } from '../../common';

const BalanceStatistic = ({expenses, currency}) => {
    currency = walletHelper.mapValueCurrencyToString(currency);
    const calculate = (type) => {
      return expenses.filter(x => x.type === type).map(x => x.price).reduce((a, b) => a + b, 0).toFixed(2);
    };

    const income = calculate('Income');
    const expense = calculate('Expense');
    const balance = (income - expense);
    return (
        <Segment>
            <Button fluid color="red">
                Expenses <br/> {expense} {currency}
            </Button>
            <br/>
            <Button fluid color="green">
                Incomes <br/> {income} {currency}
            </Button>
            <Divider/>
            <Button fluid color={balance === 0.00 ? null : balance > 0 ? 'green' : 'red'}>
                Balance <br/> {`${balance > 0 ? '+' : ''} ${balance.toFixed(2)} ${currency}`}
            </Button>
        </Segment>
    );
};

export default BalanceStatistic;