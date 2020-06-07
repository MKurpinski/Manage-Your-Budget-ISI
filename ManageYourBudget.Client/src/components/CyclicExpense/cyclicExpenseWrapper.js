import React, { Fragment } from 'react';
import { Loader, Message, Modal, Table } from 'semantic-ui-react';
import { cyclicExpenseApi } from '../../api';
import ResponsiveTable from '../common/responsiveTable';
import SingleCyclicExpense from './singleCyclicExpense';
import { toastrService } from '../../common';
import helpers from '../../common/helpers';

export default class CyclicExpenseWrapper extends React.Component {
    state = {
        cyclicExpenses: [],
        loaded: false
    };

    async componentDidMount() {
        const cyclicExpenses = await cyclicExpenseApi.get(this.props.walletId);
        this.setState({cyclicExpenses, loaded: true});
    }

    onExpenseEdit = async (editedOriginal, original) => {
        try {
            let edited = {...editedOriginal};
            edited = helpers.prepareExpenseToSave(edited, this.props.walletId);

            await cyclicExpenseApi.update(edited, original.id);

            edited = helpers.prepareExpenseToDisplay(edited);

            this.setState(prevState => {
                return {
                    cyclicExpenses: prevState.cyclicExpenses.map(x => x.id === original.id ? edited : x)
                }
            });

            toastrService.success('Successfully modified');
        }
        catch (e) {
            toastrService.error('Something went wrong', 'Ups');
        }
    };

    onExpenseDelete = async (expense) => {
        try {
            await cyclicExpenseApi.remove(expense.id);
            this.setState(prevState => {
                return {cyclicExpenses: prevState.cyclicExpenses.filter(x => x.id !== expense.id)};
            })
        }
        catch (e) {
            toastrService.error('Something went wrong', 'Ups');
        }
    };

    renderExpenses = () => {
        return this.state.cyclicExpenses.map(expense =>
            <SingleCyclicExpense
                downloadCurrency={this.props.downloadCurrency}
                key={expense.id}
                expense={expense}
                onExpenseEdited={this.onExpenseEdit}
                onDelete={this.onExpenseDelete}
                currency={this.props.currency}/>
        );
    };

    renderTable = () => {
        if (!this.state.cyclicExpenses.length) {
            return (
                <div style={{minHeight: '40vh'}} className="centered-row">
                    <Message style={{width: '100%'}}>
                        <Message.Header className="centered-row">
                            There aren't any cyclic expenses defined for this wallet
                        </Message.Header>
                    </Message>
                </div>
            );
        }
        return (
            <Fragment>
                <ResponsiveTable
                    getContent={this.renderExpenses}
                    header=
                        {
                            <Table.Row>
                                <Table.HeaderCell textAlign="center">Name</Table.HeaderCell>
                                <Table.HeaderCell textAlign="center">Place</Table.HeaderCell>
                                <Table.HeaderCell textAlign="center">Price</Table.HeaderCell>
                                <Table.HeaderCell textAlign="center">Category</Table.HeaderCell>
                                <Table.HeaderCell textAlign="center">Every</Table.HeaderCell>
                                <Table.HeaderCell textAlign="center">Next app. date</Table.HeaderCell>
                                <Table.HeaderCell/>
                            </Table.Row>
                        }
                >
                </ResponsiveTable>
            </Fragment>
        )
    };

    render() {
        const {isOpen, onClose} = this.props;
        return (
            <Modal style={{padding: '24px', minHeight: '50vh'}} open={isOpen} onClose={onClose}>
                {
                    (
                        this.state.loaded &&
                        this.renderTable()
                    )
                    ||
                    <Loader style={{borderColor: 'red'}} active inverted/>
                }
            </Modal>
        )
    }
}