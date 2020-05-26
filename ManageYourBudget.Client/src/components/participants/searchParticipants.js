import React from 'react';
import _ from 'lodash';
import { Input, List } from 'semantic-ui-react';
import ParticipantToAdd from './participantToAdd';

export default class SearchParticipants extends React.Component {
    state = {
        searchTerm: ''
    };
    minLength = 3;
    debounceTime = 300;

    constructor(props) {
        super(props);
        this.debouncedSearch = _.debounce(props.onSearch, this.debounceTime);
    }

    onChange = async (event) => {
        await this.setState({searchTerm: event.target.value});
        if (this.state.searchTerm.length >= this.minLength) {
            this.debouncedSearch(this.state.searchTerm);
        }
    };

    onAdded = (participant) => this.props.onAdded(participant);

    render() {
        const {results, startedSearch} = this.props;
        return (
            <div style={{height: '35vh'}}>
                <Input style={{paddingTop: '5px', marginBottom: "5px"}} onChange={this.onChange} fluid icon='users' iconPosition='left'
                       placeholder='Type 3 letters to start searching...'/>
                <div>
                    <List style={{height: '32vh', overflowY: 'auto'}} divided verticalAlign='middle'>
                        {results.map(res => <ParticipantToAdd key={res.id} onClick={this.onAdded} participant={res}/>)}
                    </List>
                    {startedSearch && this.state.searchTerm && results.length === 0 &&
                    <div className="centered-row">Could not find anything :(</div>}
                </div>
            </div>
        )
    }
}