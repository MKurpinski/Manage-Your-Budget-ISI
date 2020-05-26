import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import registerServiceWorker from './registerServiceWorker';
import { BrowserRouter } from 'react-router-dom';
import { Provider } from 'react-redux';
import { routes } from './routing';
import configureStore from './store';
import 'react-datepicker/dist/react-datepicker.css';

const store = configureStore();

ReactDOM.render(
    <Provider store={store}>
        <BrowserRouter>{routes}</BrowserRouter>
    </Provider>,
    document.getElementById('root')
);
registerServiceWorker();
