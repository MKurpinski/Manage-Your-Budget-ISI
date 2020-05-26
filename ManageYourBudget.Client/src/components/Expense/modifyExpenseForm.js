import React from 'react'
import { change, Field, getFormValues, reduxForm } from 'redux-form'
import ValidatedField from '../common/formInput';
import { validators } from '../../common/index';
import { SimpleButton } from '../common/buttons';
import DropdownField from '../common/formDropdown';
import { Grid, Icon, Popup } from 'semantic-ui-react';
import { walletHelper } from '../../common';
import { connect } from 'react-redux';
import DatepickerFormField from '../common/datepickerFormField';
import helpers from '../../common/helpers';
import { bindActionCreators } from 'redux';

const nameRequired = validators.required('Name');
const dateRequired = validators.required('Date');
const valueRequired = validators.required('Value');
const valueBiggerThan = validators.biggerThan('Value', 0);
const rateBiggerThan = validators.biggerThan('Rate', 0);

let ModifyExpenseForm = ({handleSubmit, submitting, invalid, initialValues, current, isEdit, downloadCurrency, change}) => {
    const dataNotChanged = isEdit && JSON.stringify(initialValues) === JSON.stringify(current);

    const setFirstCategory = (value) => {
        const categories = (value === walletHelper.expenseType) ? walletHelper.expenseCategories :walletHelper.incomeCategories;
        if(!categories.filter(x => x.value === current.category).length){
            change('category', categories[0].value);
        }
    };

    return (
        <form onSubmit={handleSubmit} style={{minWidth: '70%'}}>
            <Field
                name="name"
                component={ValidatedField}
                type="text"
                label="Title"
                validate={[nameRequired]}
            />
            <Grid>
                <Grid.Column width={8}>
                    <Field
                        name="place"
                        component={ValidatedField}
                        type="text"
                        label="Place"
                    />
                </Grid.Column>
                <Grid.Column width={8}>
                    <Field name="date"
                           placeholder="Choose date.."
                           component={DatepickerFormField}
                           validate={[dateRequired]}
                           label="Date"
                    />
                </Grid.Column>
                <Grid.Column width={5}>
                    <Field
                        name="price"
                        component={ValidatedField}
                        type="number"
                        label={<div>Price {current && current.currency !== initialValues.currency && current.price ?
                            <strong>({(current.price * current.rate).toFixed(2)}{walletHelper.mapValueCurrencyToString(initialValues.currency)}) </strong> : null}</div>}
                        prepareValue={helpers.parseToNumberOfDecimalPoints(2)}
                        validate={[valueRequired, valueBiggerThan]}
                    />
                </Grid.Column>
                <Grid.Column width={6}>
                    <Field name="currency"
                           options={walletHelper.currencies}
                           customOnChange={downloadCurrency}
                           component={DropdownField}
                           label="Currency"
                    />
                </Grid.Column>
                <Grid.Column width={5}>
                    <Field name="rate"
                           disabled={current && current.currency === initialValues.currency}
                           component={ValidatedField}
                           type="number"
                           prepareValue={helpers.parseToNumberOfDecimalPoints(2)}
                           validate={[rateBiggerThan]}
                           label={
                               <div>Rate <Popup trigger={<Icon name='info circle'/>}
                                                content={`Exchange rate between default currency of wallet (${walletHelper.mapValueCurrencyToString(initialValues.currency)}) and currently chosen one`}/>
                               </div>
                           }
                    />
                </Grid.Column>
                <Grid.Column width={8}>
                    <Field name="category"
                           options={(current && current.type !== walletHelper.expenseType) ? walletHelper.incomeCategories :walletHelper.expenseCategories }
                           component={DropdownField}
                           label="Category"
                    />
                </Grid.Column>
                <Grid.Column width={8}>
                    <Field name="type"
                           options={walletHelper.expenseTypes}
                           component={DropdownField}
                           label="Type"
                           customOnChange={setFirstCategory}
                    />
                </Grid.Column>
            </Grid>
            <SimpleButton className="fluid primary" disabled={invalid || submitting || dataNotChanged}>
                Confirm
            </SimpleButton>
        </form>
    )
};

ModifyExpenseForm = reduxForm({
    form: 'modifyExpenseForm',
    enableReinitialize: true
})(ModifyExpenseForm);

const mapStateToProps = (state, ownProps) => {
    return {
        initialValues: ownProps.initialValues ? ownProps.initialValues : {},
        current: getFormValues('modifyExpenseForm')(state)
    }
};

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators({change}, dispatch);
};

ModifyExpenseForm = connect(mapStateToProps, mapDispatchToProps)(ModifyExpenseForm);

export default ModifyExpenseForm