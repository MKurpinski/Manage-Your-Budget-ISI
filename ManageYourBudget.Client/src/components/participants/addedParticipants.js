import React from 'react';
import AddedParticipant from './participant';
import { List } from 'semantic-ui-react';

const AddedParticipants = ({participants, hasAllPrivileges, onDelete, onEdit}) => {
    const onDeleteInternal = (participant) => onDelete(participant);
    const onEditInternal = (participant, role) => onEdit(participant, role)

    return (
        <List style={{height: '38vh', overflowY: 'auto'}} divided verticalAlign='middle'>
            {!participants.length &&
            <div style={{overflow: 'hidden'}} className="centered-row">No assigned people !</div>}
            {participants.map(participant => <AddedParticipant onChangeRole={onEditInternal}
                                                               onDelete={onDeleteInternal}
                                                               hasAllPrivileges={hasAllPrivileges}
                                                               participant={participant} key={participant.id}/>)}

        </List>
    );
};

export default AddedParticipants;