import React from 'react'
import { Dimmer, Header } from 'semantic-ui-react'


const CustomSpiner = ({active}) => {
    return (
        <Dimmer style={{backgroundColor: 'rgba(0,0,0,.90)'}} active={active} page>
            <Header as='h2' icon inverted>
                <i className="custom-spinner money bill alternate outline icon"/>
                Getting things ready..
            </Header>
        </Dimmer>
    )
};

export default CustomSpiner;
