import React, { Component } from 'react';
import PropTypes from 'prop-types'
import axios from 'axios';

import authProvider from './authProvider';
import { BASE_URL } from './constants';
import { withRouter } from 'react-router-dom';

class App extends Component {
    constructor(){
        super();

        axios.interceptors.response.use(
            response => response,
            error => {
                switch (error.response.status) {
                    case 401:
                        authProvider.removeToken();
                        this.props.history.push('/login');
                        break;
                    default:
                        break;
                }
                return Promise.reject(error);
            }
        );

        axios.interceptors.request.use(config => {
            const token = authProvider.getToken();
            config.headers.Authorization =
                token && config.url.startsWith(BASE_URL)
                    ? `Bearer ${token}`
                    : '';
            return config;
        });
    }
  render() {
    return (
        <div>
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
