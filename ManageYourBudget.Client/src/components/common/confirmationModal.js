import { Confirm } from 'semantic-ui-react';
import React from 'react';

const ConfirmationModal = ({onConfirm, onReject, header, content, confirmButton, rejectButton, isOpened, size}) => {
    return (
        <Confirm
            style={{zIndex: '100000'}}
            size={size}
            open={isOpened}
            cancelButton={rejectButton}
            confirmButton={confirmButton}
            onCancel={onReject}
            content={content}
            header={header}
            onConfirm={onConfirm}
        />
    )
};

ConfirmationModal.defaultProps = {
    header: '',
    content: 'Are you sure?',
    confirmButton: 'Confirm',
    rejectButton: 'Cancel',
    size: 'tiny'
};

export default ConfirmationModal;