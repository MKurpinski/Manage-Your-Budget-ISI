import React from 'react';
import PropTypes from 'prop-types';
import SimpleButton from './simpleButton';
import { Icon } from 'semantic-ui-react';

const IconButton = ({iconName, children, ...rest}) => {
    return (
        <SimpleButton color={iconName} {...rest}>
            <Icon name={iconName}/> {children}
        </SimpleButton>
    )
};

IconButton.propTypes = {
    onClick: PropTypes.func,
    disabled: PropTypes.bool,
    isLoading: PropTypes.bool,
    className: PropTypes.string,
    iconName: PropTypes.string
};

export default IconButton;