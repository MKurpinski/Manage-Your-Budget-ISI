import React, { Component } from 'react';
import PropTypes from 'prop-types'
import { withRouter } from 'react-router-dom';
import baseApi from './api/baseApi';
import { toastrService } from './common';

class App extends Component {
    constructor() {
        super();
        baseApi.init();
        toastrService.init()
    }

    render() {
        return (
            <div id="app">
                <span className="preload-handler"/>
                {this.props.children}
            </div>
        );
    }
}

App.propTypes = {
    match: PropTypes.object.isRequired,
    location: PropTypes.object.isRequired,
    history: PropTypes.object.isRequired,
    children: PropTypes.object.isRequired,
};

export default withRouter(App);
