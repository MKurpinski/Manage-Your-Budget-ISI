import React from 'react';
import { Button } from 'semantic-ui-react';

export default class ButtonAsInput extends React.Component{
    render() {
    const {onClick, value} = this.props;
        return (
            <Button
                type="button"
                fluid
                className="button-no-padding"
                onClick={onClick}>
                <p style={{fontSize: '11px'}}>{value}</p>
            </Button>
        )
    }

};