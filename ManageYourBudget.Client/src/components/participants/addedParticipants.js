import React from 'react';
import AddedParticipant from './participant';
import { List } from 'semantic-ui-react';

const AddedParticipants = ({participants, hasAllPrivileges, onDelete, onEdit}) => {

    return (
        <List style={{height: '38vh', overflowY: 'auto'}}
              divided
              verticalAlign='middle'
        >
            {!participants.length &&
            <div style={{overflow: 'hidden'}} className="centered-row">No assigned people !</div>}
            {participants.map(participant =>
                <AddedParticipant
                    onChangeRole={onEdit}
                    onDelete={onDelete}
                    hasAllPrivileges={hasAllPrivileges}
                    participant={participant}
                    key={participant.id}
                />)
            }
        </List>
    );
};

export default AddedParticipants;