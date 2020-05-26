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
                           customOnChange,
                           meta: {touched, error, warning}
                       }) => {

    const onChange = (param, data) => {
        if(customOnChange){
            customOnChange(data.value);
        }
        input.onChange(data.value)
    };
    return (
        <div>
            <label>{label}</label>
            <div>
                <Dropdown
                    fluid
                    error={touched && !!error}
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