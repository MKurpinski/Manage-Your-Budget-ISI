import React from 'react';
import PropTypes from 'prop-types';
import { Button } from 'semantic-ui-react';

const SimpleButton = ({onClick, disabled, children, className, isLoading, ...rest}) => {
    return (
        <Button className={className} loading={isLoading} onClick={onClick} disabled={disabled} {...rest}>
            {children}
        </Button>
    )
};

SimpleButton.propTypes = {
    onClick: PropTypes.func,
    disabled: PropTypes.bool,
    isLoading: PropTypes.bool,
    className: PropTypes.string
};

export default SimpleButton;