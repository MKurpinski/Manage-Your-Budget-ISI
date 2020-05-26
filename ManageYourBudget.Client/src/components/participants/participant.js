import React from 'react';
import { Button, Divider, Dropdown, Icon, Image, List, Transition } from 'semantic-ui-react';
import { walletHelper } from '../../common';
import SimpleButton from '../common/buttons/simpleButton';
import ConfirmationModal from '../common/confirmationModal';

export default class Participant extends React.Component {
    state = {
        editingOpened: false,
        newRole: 0,
        isOpenedDeleteModal: false,
    };

    getFullName = () => `${this.props.participant.firstName} ${this.props.participant.lastName}`;

    toggleEdit = () => this.setState({editingOpened: !this.state.editingOpened});

    componentDidMount() {
        this.setState({newRole: walletHelper.mapStringRoleToValue(this.props.participant.role)})
    }

    onChange = (e, {value}) => {
        this.setState({newRole: value});
    };

    startDelete = () => {
        this.setState({isOpenedDeleteModal: !this.state.isOpenedDeleteModal});
    };

    confirmDelete = () => {
        this.props.onDelete(this.props.participant);
    };

    onChangeRole = () => {
      this.props.onChangeRole(this.props.participant, this.state.newRole)
    };

    render() {
        const {participant, hasAllPrivileges} = this.props;
        return (
            <List.Item>
                <ConfirmationModal onConfirm={this.confirmDelete} header="Are you sure?"
                                   content="Do You want to unassign user from wallet?"
                                   isOpened={this.state.isOpenedDeleteModal}
                                   onReject={this.startDelete} {...this.state.deleteModalData}/>
                <Image avatar src={participant.pictureSrc}/>
                <List.Content>
                    <List.Header>{this.getFullName()}</List.Header>
                    {participant.role}
                </List.Content>
                {
                    hasAllPrivileges &&
                    <List.Content floated='right'>
                        <Button onClick={this.toggleEdit}>{this.state.editingOpened ?
                            'Hide'
                            :
                            'More'
                        }</Button>
                    </List.Content>
                }
                <Transition visible={this.state.editingOpened} animation='slide down' duration={100}>
                    <div className="double-margin-bottom">
                        <Divider/>
                        <div className="row-space-between">
                            <div>
                                <Dropdown
                                    size="small"
                                    selection
                                    options={walletHelper.roles}
                                    onChange={this.onChange}
                                    value={this.state.newRole}
                                />
                                <SimpleButton
                                    disabled={this.state.newRole === walletHelper.mapStringRoleToValue(this.props.participant.role) || participant.loading}
                                    onClick={this.onChangeRole}
                                    className="mini margin-left">
                                    Save
                                </SimpleButton>
                            </div>
                                <Icon onClick={this.startDelete} link name="minus"/>
                        </div>
                    </div>
                </Transition>
            </List.Item>
        );
    }
};