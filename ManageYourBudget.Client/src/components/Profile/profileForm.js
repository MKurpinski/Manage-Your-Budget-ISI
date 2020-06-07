import React from 'react'
import { Field, getFormValues, reduxForm } from 'redux-form'
import ValidatedField from '../common/formInput';
import { validators } from '../../common/index';
import SimpleButton from '../common/buttons/simpleButton';
import { connect } from 'react-redux';
import { FORMS } from '../../common/constants';

const firstNameRequired = validators.required('FirstName');
const lastNameRequired = validators.required('LastName');

let ProfileForm = ({error, handleSubmit, submitting, invalid, user, current}) => {
    const dataNotChanged = (!user || !current) || JSON.stringify(user) === JSON.stringify(current);

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
                props={{disabled: true}}
                component={ValidatedField}
                type="text"
                label="Email"
                icon="at"
            />
            <SimpleButton fluid className="primary" disabled={invalid || submitting || dataNotChanged}>
                Submit
            </SimpleButton>
        </form>
    )
};

const mapStateToProps = (state, ownProps) => {
    return {
        initialValues: ownProps.initial,
        current: getFormValues(FORMS.PROFILE_FORM)(state)
    }
};

ProfileForm = reduxForm({
    form: FORMS.PROFILE_FORM,
    enableReinitialize: true
})(ProfileForm);

ProfileForm = connect(mapStateToProps)(ProfileForm);

export default ProfileForm