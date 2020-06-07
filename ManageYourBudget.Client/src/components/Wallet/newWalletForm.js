import React from 'react'
import { Field, getFormValues, reduxForm } from 'redux-form'
import ValidatedField from '../common/formInput';
import { validators } from '../../common/index';
import { SimpleButton } from '../common/buttons';
import DropdownField from '../common/formDropdown';
import { Grid } from 'semantic-ui-react';
import { walletHelper } from '../../common';
import { connect } from 'react-redux';
import { FORMS } from '../../common/constants';

const nameRequired = validators.required('Name');
const categoryRequired = validators.required('Category');
const currencyRequired = validators.required('Default currency');

let NewWalletForm = ({handleSubmit, submitting, invalid, initialValues, current, isEdit}) => {
    const dataNotChanged = isEdit && JSON.stringify(initialValues) === JSON.stringify(current);
    return (
        <form onSubmit={handleSubmit} style={{minWidth: '70%'}}>
            <Field
                name="name"
                component={ValidatedField}
                type="text"
                label="Name"
                validate={[nameRequired]}
            />
            <Grid>
                <Grid.Column mobile={16} tablet={8} computer={8}>
                    <Field name="category"
                           options={walletHelper.categories}
                           validate={[categoryRequired]}
                           placeholder="Choose category.."
                           component={DropdownField}
                           label="Category"
                    />
                </Grid.Column>
                <Grid.Column mobile={16} tablet={8} computer={8}>
                    <Field name="currency"

                           options={walletHelper.currencies}
                           validate={[currencyRequired]}
                           placeholder="Choose default currency.."
                           props={{disabled: isEdit}}
                           component={DropdownField}
                           label="Default currency"
                    />
                </Grid.Column>
            </Grid>
            <SimpleButton className="fluid primary" disabled={invalid || submitting || dataNotChanged}>
                {isEdit ? 'Confirm' : 'Create wallet!'}
            </SimpleButton>
        </form>
    )
};

NewWalletForm = reduxForm({
    form: FORMS.NEW_WALLET_FORM,
    enableReinitialize: true
})(NewWalletForm);

const mapStateToProps = (state, ownProps) => {
    return {
        initialValues: ownProps.initialValues ? ownProps.initialValues : {},
        current: getFormValues(FORMS.NEW_WALLET_FORM)(state)
    }
};

NewWalletForm = connect(mapStateToProps)(NewWalletForm);

export default NewWalletForm