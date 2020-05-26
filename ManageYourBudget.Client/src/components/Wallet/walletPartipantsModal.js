import React from 'react';
import { Modal, Tab } from 'semantic-ui-react';
import AddedParticipants from '../participants/addedParticipants';
import './wallet.css';
import SearchParticipants from '../participants/searchParticipants';


export default class WalletParticipantsModal extends React.Component {

    createPanes = () => {
        const {searchResults} = this.props;
        const panes = [
            {
                menuItem: 'Assigned people',
                render: () => <AddedParticipants
                    onEdit={this.onEditInternal}
                    onDelete={this.onDeleteInternal}
                    hasAllPrivileges={this.props.hasAllPrivileges}
                    participants={this.props.participants}/>
            }
        ];
        if (this.props.hasAllPrivileges) {
            panes.push({
                    menuItem: 'Add people',
                    render: () => <SearchParticipants onAdded={this.onAdded} started
                                                      onSearch={this.searchPeople}
                                                      results={searchResults.results}
                                                      startedSearch={searchResults.startedSearch}/>
                }
            )
        }
        return panes;
    };

    onEditInternal = (participant, role) => {
        this.props.onEdit(participant, role);
    };

    onDeleteInternal = (participant) => {
        this.props.onDelete(participant);
    };

    searchPeople = async (searchTerm) => {
        this.props.onSearch(searchTerm);
    };

    onAdded = (participant) => {
        this.props.onAdded(participant);
    };

    render() {
        const {isOpen, onClose} = this.props;
        return (
            <Modal className="wallet-participants-modal" style={{padding: '16px', height: '50vh'}} open={isOpen}
                   onClose={onClose}>
                <Tab renderActiveOnly panes={this.createPanes()}/>
            </Modal>
        )
    }
}
