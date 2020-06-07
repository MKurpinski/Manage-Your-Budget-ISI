import { Modal } from 'semantic-ui-react';
import React from 'react';
import ModifyExpenseForm from './modifyExpenseForm';
import moment from 'moment';
import walletHelper from '../../common/walletHelper';

const ModifyExpenseModal = ({isOpen, defaultCurrency, expense, onSave, onClose, downloadCurrency, isEdit, isCyclic}) => {
    let initialValues = {
        currency: defaultCurrency,
        type: '0',
        category: '0',
        date: moment(),
        startingFrom: moment(),
        periodType: 'Week',
        rate: 1
    };

    const prepareExpense = (expense) => {
        const copyOfExpense = {...expense};
        if (isOpen) {
            copyOfExpense.category = walletHelper.mapExpenseCategoryToValue(expense.category);
            copyOfExpense.type = walletHelper.mapStringTypeToValue(expense.type);
        }

        return copyOfExpense;
    };

    expense = expense ? prepareExpense(expense) : {};
    initialValues = {...initialValues, ...expense};
    return (
        <Modal style={{padding: '24px'}} open={isOpen} onClose={onClose}>
            <ModifyExpenseForm onSubmit={onSave} downloadCurrency={downloadCurrency} initialValues={initialValues}
                               isEdit={isEdit} isCyclic={isCyclic}/>
        </Modal>
    )
};

export default ModifyExpenseModal;