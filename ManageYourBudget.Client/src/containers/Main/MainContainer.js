import React from 'react'
import { connect } from 'react-redux';
import profileActions from '../../actions';
import bindActionCreators from 'redux/src/bindActionCreators';
import { profileApi } from '../../api';

class MainContainer extends React.Component {
    componentDidMount() {
        profileApi.getProfile().then(profile => {
            this.props.actions.getProfile(profile);
        });
    }

    render() {
        return <div>Main container</div>
    }
}

const mapDispatchToProps = dispatch => {
    return { actions: bindActionCreators(profileActions, dispatch)};
};

export default connect(null, mapDispatchToProps)(MainContainer);