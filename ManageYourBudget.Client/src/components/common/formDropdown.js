import React from 'react';
import { Dropdown } from 'semantic-ui-react';

const DropdownField = ({
                           input,
                           placeholder,
                           label,
                           type,
                           icon,
                           disabled,
                           options,
                           defaultValue,
                           meta: {touched, error, warning}
                       }) => {

    const onChange = (param, data) => {
        input.onChange(data.value)
    };

    return (
        <div>
            <label>{label}</label>
            <div>
                <Dropdown
                    fluid
                    error={touched && !!error}
                    size="large"
                    selection {...input}
                    options={options}
                    disabled={disabled}
                    placeholder={placeholder}
                    onChange={onChange}
                />
                <div className="error-message">
                    {touched &&
                    ((error && <div>{error}</div>) ||
                        (warning && <div>{warning}</div>))}
                </div>
            </div>
        </div>
    )
};

export default DropdownField;