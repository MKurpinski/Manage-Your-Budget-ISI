import { Modal } from 'semantic-ui-react';
import NewWalletForm from './newWalletForm';
import React from 'react';
import { walletHelper } from '../../common';

const EditWalletModal = ({isOpen, wallet, onSave, onClose}) => {
    wallet = {
        ...wallet,
        category: walletHelper.mapStringCategoryToValue(wallet.category),
        currency: walletHelper.mapStringCurrencyToValue(wallet.defaultCurrency),
    };
    return (
        <Modal style={{padding: '24px'}} open={isOpen} onClose={onClose}>
            <h1>Modify wallet</h1>
            <NewWalletForm onSubmit={onSave} initialValues={wallet} hand isEdit={true}/>
        </Modal>
    )
};

export default EditWalletModal;