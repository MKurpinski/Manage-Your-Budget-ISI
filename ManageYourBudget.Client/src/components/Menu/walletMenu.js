import React from 'react';
import { Icon, Popup } from 'semantic-ui-react';
import DropdownMenu from '../common/buttons/dropdownMenu';

const WalletMenu = ({onOpen, onClose, open, onClick, name, options}) => {
    return (
        <div className="row-space-between">
            <h2>{name}</h2>
            <Popup
                trigger={<Icon style={{marginTop: "-20px"}} link name="ellipsis vertical"/>}
                content={<DropdownMenu onClick={onClick} options={options}/>}
                on='click'
                onOpen={onOpen}
                onClose={onClose}
                open={open}
                position='left center'
            />
        </div>
    )
};

export default WalletMenu;