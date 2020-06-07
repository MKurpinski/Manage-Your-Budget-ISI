import React from 'react'
import { Field, reduxForm } from 'redux-form'
import ValidatedField from '../common/formInput';
import { validators } from '../../common/index';
import { SimpleButton, LinkAsButton } from '../common/buttons';
import { Divider } from 'semantic-ui-react';
import { routesConstants } from '../../routing';
import { FORMS } from '../../common/constants';

const emailRequired = validators.required('Email');
const firstNameRequired = validators.required('FirstName');
const lastNameRequired = validators.required('LastName');
const passwordRequired = validators.required('Password');
const comparePasswords = validators.compare('Confirm password', 'password', 'password');

let RegisterForm = ({error, handleSubmit, submitting, invalid}) => {
    return (
        <form onSubmit={handleSubmit}>
            <Field
                name="firstName"
                component={ValidatedField}
                type="text"
                label="FirstName"
                validate={[firstNameRequired]}
                icon="address card outline"
            />
            <Field
                name="lastName"
                component={ValidatedField}
                type="text"
                label="LastName"
                validate={[lastNameRequired]}
                icon="address card outline"
            />
            <Field
                name="email"
                component={ValidatedField}
                type="text"
                label="Email"
                icon="at"
                validate={[emailRequired, validators.email]}
            />
            <Field
                name="password"
                component={ValidatedField}
                type="password"
                label="Password"
                icon="dna"
                validate={[passwordRequired]}
            />
            <Field
                name="confirmPassword"
                component={ValidatedField}
                type="password"
                label="Confirm password"
                icon="dna"
                validate={[comparePasswords]}
            />
            {error}
                <SimpleButton fluid className="primary" disabled={invalid || submitting}>
                    Register
                </SimpleButton>
                <Divider horizontal>Or</Divider>
                <LinkAsButton to={routesConstants.LOGIN} className="fluid secondary">
                    Already have account? Login!
                </LinkAsButton>
        </form>
    )
};

RegisterForm = reduxForm({
    form: FORMS.REGISTER_FORM
})(RegisterForm);

export default RegisterForm