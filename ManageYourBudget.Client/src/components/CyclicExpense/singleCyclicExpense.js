import React, { Fragment } from 'react';
import { Icon, Responsive, Segment, Table } from 'semantic-ui-react';
import walletHelper from '../../common/walletHelper';
import { DATE_FORMAT } from '../../common/constants';
import moment from 'moment';
import ConfirmationModal from '../common/confirmationModal';
import SimpleButton from '../common/buttons/simpleButton';
import ModifyCyclicExpenseModal from '../Expense/modifyCyclicExpenseModal';


export default class SingleCyclicExpense extends React.Component {
    state = {
        isDeleteModalOpened: false,
        isEditingModalOpened: false
    };

    toggleDeleteModal = () => {
        this.setState({isDeleteModalOpened: !this.state.isDeleteModalOpened});
    };

    onDeleteInternal = () => {
        this.props.onDelete(this.props.expense);
    };

    toggleEditing = () => {
        this.setState({isEditingModalOpened: !this.state.isEditingModalOpened});
    };

    onEditInternal = async (editedData) => {
        await this.props.onExpenseEdited(editedData, this.props.expense);
        this.toggleEditing()
    };

    renderProp = (label, value) => (
        <div className="row-space-between">
            <strong>{label}: </strong> {value}
        </div>
    );

    render() {
        const {expense, currency} = this.props;
        const classNameToOfRow = expense.type.toLowerCase();

        return (
            <Fragment>
                <ModifyCyclicExpenseModal
                    onSave={this.onEditInternal}
                    isEdit={true}
                    expense={expense}
                    defaultCurrency={currency}
                    downloadCurrency={this.props.downloadCurrency}
                    isOpen={this.state.isEditingModalOpened}
                    onClose={this.toggleEditing}
                />

                <ConfirmationModal
                    onConfirm={this.onDeleteInternal}
                    header="Are you sure you want to delete this item?"
                    content="This is permanent change and cannot be undone"
                    onReject={this.toggleDeleteModal}
                    isOpened={this.state.isDeleteModalOpened}
                />

                <Responsive className={classNameToOfRow} as={Table.Row} minWidth={Responsive.onlyTablet.minWidth}>
                    <Table.Cell textAlign="center">{expense.name}</Table.Cell>
                    <Table.Cell textAlign="center">{expense.place}</Table.Cell>
                    <Table.Cell textAlign="right">
                        {expense.price.toFixed(2)} {walletHelper.mapValueCurrencyToString(currency)}
                    </Table.Cell>
                    <Table.Cell textAlign="center">{expense.category} ({expense.type})</Table.Cell>
                    <Table.Cell textAlign="center">{expense.periodType}</Table.Cell>
                    <Table.Cell textAlign="center">{moment(expense.nextApplyingDate).format(DATE_FORMAT)}</Table.Cell>
                    <Table.Cell className="row-space-around">
                        <Icon onClick={this.toggleEditing} link name='edit outline'/>
                        <Icon onClick={this.toggleDeleteModal} link name='close'/>
                    </Table.Cell>
                </Responsive>

                <Responsive as={Segment} className={classNameToOfRow} {...Responsive.onlyMobile}>
                    <div className="aligned-right">
                        <Icon style={{marginBottom: '10px', marginRight: '-5px'}} onClick={this.toggleDeleteModal} link name='close'/>
                    </div>
                    {this.renderProp('Name', expense.name)}
                    {this.renderProp('Place', expense.place)}
                    {this.renderProp('Price', `${expense.price.toFixed(2)} ${walletHelper.mapValueCurrencyToString(currency)}`)}
                    {this.renderProp('Category', `${expense.category} (${expense.type})`)}
                    {this.renderProp('Every', expense.periodType)}
                    {this.renderProp('Next applying date', moment(expense.nextApplyingDate).format(DATE_FORMAT))}
                    <SimpleButton style={{marginTop: '10px'}} fluid onClick={this.toggleEditing}>
                        Edit
                    </SimpleButton>
                </Responsive>
            </Fragment>
        );
    }
}