import React from 'react'
import { Field, reduxForm } from 'redux-form'
import ValidatedField from '../common/formInput';
import { validators } from '../../common/index';
import { SimpleButton } from '../common/buttons';
import { Message } from 'semantic-ui-react';
import { FORMS } from '../../common/constants';

const emailRequired = validators.required('Email');
const passwordRequired = validators.required('Password');

let LoginForm = ({error, handleSubmit, submitting, invalid}) => {
    return (
        <form onSubmit={handleSubmit}>
            <Field
                icon="at"
                name="email"
                component={ValidatedField}
                type="text"
                label="Email"
                validate={[emailRequired, validators.email]}
            />
            <Field
                icon="dna"
                name="password"
                component={ValidatedField}
                type="password"
                label="Password"
                validate={[passwordRequired]}
            />
            {error && <Message color='red'>{error}</Message>}
            <SimpleButton fluid isLoading={submitting} className="primary auth-button" disabled={invalid || submitting}>
                Login
            </SimpleButton>
        </form>
    )
};

LoginForm = reduxForm({
    form: FORMS.LOGIN_FORM
})(LoginForm);

export default LoginForm