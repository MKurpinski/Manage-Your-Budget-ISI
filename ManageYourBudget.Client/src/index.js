import React from 'react';
import ReactDOM from 'react-dom';
import './index.scss';
import registerServiceWorker from './registerServiceWorker';
import { BrowserRouter } from 'react-router-dom';
import { Provider } from 'react-redux';
import { routes } from './routing';
import configureStore from './store';

const store = configureStore();

ReactDOM.render(
    <Provider store={store}>
        <BrowserRouter>{routes}</BrowserRouter>
    </Provider>,
    document.getElementById('root')
);
registerServiceWorker();
