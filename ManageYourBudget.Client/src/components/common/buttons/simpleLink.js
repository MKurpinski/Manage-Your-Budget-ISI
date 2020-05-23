import React from 'react';
import { Link } from 'react-router-dom';

const SimpleLink = ({to, children, ...rest}) => {
    return (
        <Link to={to} {...rest}>
            {children}
        </Link>
    )
};

export default SimpleLink;