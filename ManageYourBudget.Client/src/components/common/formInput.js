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
                       meta: {touched, error, warning}
                   }) => (
    <div>
        <label>{label}</label>
        <div>
            <Input disabled={disabled} size="large" fluid icon={icon} iconPosition={icon && 'left'} error={touched && !!error} {...input}
                   placeholder={placeholder} type={type}/>
            <div className="error-message">
                {touched &&
                ((error && <div>{error}</div>) ||
                    (warning && <div>{warning}</div>))}
            </div>
        </div>
    </div>
);

FormInput.propTypes = {
    input: PropTypes.object,
    icon: PropTypes.string,
    placeholder: PropTypes.string,
    label: PropTypes.string.isRequired,
    meta: PropTypes.object,
    type: PropTypes.oneOf(['text', 'email', 'password'])
};

export default FormInput;