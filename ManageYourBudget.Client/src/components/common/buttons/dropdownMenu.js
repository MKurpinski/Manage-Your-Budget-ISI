import { List } from 'semantic-ui-react';
import React from 'react';

const DropdownMenu = ({options, onClick}) => {
    return (
        <List style={{minWidth: "100px"}} divided relaxed onClick={onClick}>
            {options.map((option, index) => {
                return (
                    <List.Item className="dropdown-menu-item" key={index}>
                        {option}
                    </List.Item>
                )
            })}
        </List>
    )
};

export default DropdownMenu;