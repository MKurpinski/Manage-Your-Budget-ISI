import React from 'react'
import { Field, reduxForm } from 'redux-form'
import ValidatedField from '../common/formInput';
import { validators } from '../../common/index';
import { SimpleButton } from '../common/buttons';

const passwordRequired = validators.required('Password');
const oldPasswordRequired = validators.required('Old password');
const comparePasswords = validators.compare('Confirm password', 'password', 'password');

let ChangePasswordForm = ({error, handleSubmit, submitting, invalid}) => {
    return (
        <form onSubmit={handleSubmit}>
            <Field
                name="oldPassword"
                component={ValidatedField}
                type="password"
                label="Old Password"
                validate={[oldPasswordRequired]}
                icon="dna"
            />
            <Field
                name="password"
                component={ValidatedField}
                type="password"
                label="Password"
                validate={[passwordRequired]}
                icon="dna"
            />
            <Field
                name="confirmPassword"
                component={ValidatedField}
                type="password"
                label="Confirm password"
                validate={[comparePasswords]}
                icon="dna"
            />
            <SimpleButton className="fluid primary" type="submit" disabled={invalid || submitting}>
                Submit
            </SimpleButton>
        </form>
    )
};

ChangePasswordForm = reduxForm({
    form: 'changePasswordForm'
})(ChangePasswordForm);

export default ChangePasswordForm;