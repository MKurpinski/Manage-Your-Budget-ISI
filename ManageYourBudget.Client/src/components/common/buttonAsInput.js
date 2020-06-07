import React from 'react';
import { Button } from 'semantic-ui-react';

export default class ButtonAsInput extends React.Component{
    render() {
    const {onClick, value} = this.props;
        return (
            <Button
                fluid
                onClick={onClick}>
                {value}
            </Button>
        )
    }

};