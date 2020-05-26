import { Icon, Image, List } from 'semantic-ui-react';
import React from 'react';

const ParticipantToAdd = ({participant, onClick}) => {
    const onClickInternal = () => {
        if (participant.loading) {
            return;
        }
        onClick(participant);
    };

    return (
        <List.Item>
            <div className="row-space-between">
                <div className="centered-row">
                    <Image avatar src={participant.pictureSrc}/>
                    <List.Header>{`${participant.firstName} ${participant.lastName}`}</List.Header>
                </div>
                <Icon disabled={participant.loading} onClick={onClickInternal} link
                      name={participant.added ? 'minus' : 'plus'}/>
            </div>
        </List.Item>
    )
};

export default ParticipantToAdd;