import React from 'react';
import { Link } from 'react-router-dom';

const LinkAsButton = ({to, className, children}) => {
    return (
        <Link to={to} className={`ui button ${className}`}>
            {children}
        </Link>
    )
};

export default LinkAsButton;