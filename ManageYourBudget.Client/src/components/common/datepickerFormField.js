import DatePicker from 'react-datepicker';
import React from 'react';
import moment from 'moment';
import { Input } from 'semantic-ui-react';
import { DATE_FORMAT } from '../../common/constants';

const DatepickerFormField = ({input, disabled, label, placeholder, defaultValue, maxDate, minDate, meta: {touched, error}}) => (
    <div style={{width: '100%'}}>
        <label>{label}</label>
        <DatePicker
            todayButton="Today"
            maxDate={maxDate ? moment(maxDate) : maxDate}
            minDate={minDate}
            className={touched && error ? 'error' : ''}
            placeholderText={placeholder}
            {...input}
            disabled={disabled}
            value = {moment(input.value).format(DATE_FORMAT)}
            dateForm={DATE_FORMAT}
            customInput={<Input fluid/>}
            selected={input.value ? moment(input.value) : null}/>
        <div className="error-message">
            {touched &&
            ((error && <div>{error}</div>))}
        </div>
    </div>
);

export default DatepickerFormField;