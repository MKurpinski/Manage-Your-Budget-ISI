import React from 'react';
import PropTypes from 'prop-types';
import { Input } from 'semantic-ui-react';

const FormInput = ({
                       input,
                       placeholder,
                       label,
                       type,
                       icon,
                       disabled,
                       prepareValue,
                       meta: {touched, error, warning}
                   }) => {

    const customOnChange = (e, v) => {
        prepareValue = prepareValue ? prepareValue : (val) => val;
        input.onChange(prepareValue(v.value));
    };
    return (
        <div>
            <label>{label}</label>
            <div>
                <Input disabled={disabled} fluid icon={icon} iconPosition={icon && 'left'}
                       error={touched && !!error} {...input} step="0.01"
                       placeholder={placeholder} type={type} onChange={customOnChange}/>
                <div className="error-message">
                    {touched &&
                    ((error && <div>{error}</div>) ||
                        (warning && <div>{warning}</div>))}
                </div>
            </div>
        </div>
    )
};

FormInput.propTypes = {
    input: PropTypes.object,
    icon: PropTypes.string,
    placeholder: PropTypes.string,
    label: PropTypes.any.isRequired,
    meta: PropTypes.object,
    type: PropTypes.oneOf(['text', 'email', 'password', 'number'])
};

export default FormInput;