import React from 'react'
import { Field, reduxForm } from 'redux-form'
import ValidatedField from '../common/formInput';
import { validators } from '../../common/index';
import {SimpleButton} from '../common/buttons';
import DropdownField from '../common/formDropdown';
import { Grid } from 'semantic-ui-react';
import { walletHelper } from '../../common';

const nameRequired = validators.required('Name');
const categoryRequired = validators.required('Category');
const currencyRequired = validators.required('Default currency');

let NewWalletForm = ({handleSubmit, submitting, invalid}) => {
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
                           component={DropdownField}
                           label="Default currency"
                    />
                </Grid.Column>
            </Grid>
            <SimpleButton className="fluid primary" disabled={invalid || submitting}>
                Create wallet!
            </SimpleButton>
        </form>
    )
};

NewWalletForm = reduxForm({
    form: 'newWalletForm'
})(NewWalletForm);

export default NewWalletForm