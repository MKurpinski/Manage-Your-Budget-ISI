import { Dimmer, Grid, Loader, Message, Responsive, Segment, Sticky, Table } from 'semantic-ui-react';
import React from 'react';
import SimpleButton from '../common/buttons/simpleButton';
import BalanceStatistic from './balanceStatistic';
import SingleExpense from './singleExpense';
import ResponsiveTable from '../common/responsiveTable';

const ExpensesSection = ({isMore, isLoading, onMoreClick, expenses, currency, onExpenseDelete, onExpenseEdited, downloadCurrency}) => {
    const StatisticsColumn = () =>
        (
            <Grid.Column mobile={16} tablet={16} computer={3}>
                <Responsive as={Sticky} {...Responsive.onlyComputer}>
                    <BalanceStatistic
                        expenses={expenses}
                        currency={currency}
                    />
                </Responsive>
                <Responsive as={Sticky} maxWidth={Responsive.onlyTablet.maxWidth}>
                    <BalanceStatistic
                        expenses={expenses}
                        currency={currency}
                    />
                </Responsive>
            </Grid.Column>
        );

    const renderExpenses = () => {
        return expenses.map(expense =>
            <SingleExpense
                downloadCurrency={downloadCurrency}
                key={expense.id}
                expense={expense}
                onExpenseEdited={onExpenseEdited}
                onDelete={onExpenseDelete}
                currency={currency}/>
        )
    };

    const ExpensesColumn = () =>
        (
            <Grid.Column mobile={16} tablet={16} computer={13}>
                <ResponsiveTable
                    getContent={renderExpenses}
                    header=
                        {
                            <Table.Row>
                                <Table.HeaderCell textAlign="center">Name</Table.HeaderCell>
                                <Table.HeaderCell textAlign="center">Place</Table.HeaderCell>
                                <Table.HeaderCell textAlign="center">Price</Table.HeaderCell>
                                <Table.HeaderCell textAlign="center">Category</Table.HeaderCell>
                                <Table.HeaderCell textAlign="center">Date</Table.HeaderCell>
                                <Table.HeaderCell/>
                            </Table.Row>
                        }
                >
                </ResponsiveTable>
            </Grid.Column>
        );

    return (
        <div className="section-padding">
            <Segment vertical>
                <Dimmer active={isLoading} inverted>
                    <Loader/>
                </Dimmer>
                {!isLoading && !expenses.length &&
                <Message>
                    <Message.Header className="centered-row">No elements matching provided criteria</Message.Header>
                </Message>
                }
                {!!expenses.length &&
                <div>
                    <Responsive as={Grid} {...Responsive.onlyComputer}>
                        <ExpensesColumn/>
                        <StatisticsColumn/>
                    </Responsive>
                    <Responsive as={Grid} maxWidth={Responsive.onlyComputer.minWidth}>
                        <StatisticsColumn/>
                        <ExpensesColumn/>
                    </Responsive>
                    <div className="centered-row section-padding">
                        {isMore && <SimpleButton className="basic" onClick={onMoreClick}>Load more..</SimpleButton>}
                    </div>
                </div>
                }
            </Segment>
        </div>
    );
};

export default ExpensesSection;