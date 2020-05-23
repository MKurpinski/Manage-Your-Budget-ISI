import React from 'react';
import { Image } from 'semantic-ui-react';
import { Link } from 'react-router-dom';
import { routesConstants } from '../../routing';

const UserInfo = ({user}) => {
    const fullName = `${user.firstName} ${user.lastName}`;

    if(!user || !user.firstName){
        return null;
    }
    return (
        <Link to={routesConstants.PROFILE}>
            <Image src={user.pictureSrc} avatar/>
            <span>{fullName}</span>
        </Link>
    )
};
export default UserInfo;