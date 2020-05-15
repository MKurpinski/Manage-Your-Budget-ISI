import React from 'react'
import { Field, reduxForm } from 'redux-form'
import ValidatedField from '../common/formInput';
import { validators } from '../../common/index';
import {SimpleButton} from '../common/buttons';

const emailRequired = validators.required('Email');

let StartResetForm = ({handleSubmit, submitting, invalid}) => {
    return (
        <form onSubmit={handleSubmit}>
            <Field
                name="email"
                component={ValidatedField}
                type="text"
                label="Email"
                validate={[emailRequired, validators.email]}
                icon="at"
            />
            <SimpleButton className="fluid primary" disabled={invalid || submitting}>
                Submit
            </SimpleButton>
        </form>
    )
};

StartResetForm = reduxForm({
    form: 'startResetForm'
})(StartResetForm);

export default StartResetForm