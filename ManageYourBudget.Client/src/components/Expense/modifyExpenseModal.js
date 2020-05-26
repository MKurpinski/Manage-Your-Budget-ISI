import { Modal } from 'semantic-ui-react';
import React from 'react';
import ModifyExpenseForm from './modifyExpenseForm';
import moment from 'moment';
const ModifyExpenseModal = ({isOpen, defaultCurrency, expense, onSave, onClose, downloadCurrency, isEdit}) => {
    let initialValues  = {
        currency: defaultCurrency,
        type: '0',
        category: '0',
        date: moment(),
        rate: 1
    };
    expense = expense ? expense : {};
    initialValues = {...initialValues, ...expense};
    return (
        <Modal style={{padding: '24px'}} open={isOpen} onClose={onClose}>
            <h1>Modify expense</h1>
            <ModifyExpenseForm onSubmit={onSave} downloadCurrency={downloadCurrency} initialValues={initialValues} isEdit={false}/>
        </Modal>
    )
};

export default ModifyExpenseModal;