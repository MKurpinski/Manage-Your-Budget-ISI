import React from 'react';
import { Message, Segment } from 'semantic-ui-react';
import LinkAsButton from '../../components/common/buttons/linkAsButton';
import { routesConstants } from '../../routing';

export default class NotFoundContainer extends React.Component {

    render() {
        return (
            <div style={{width: '100vw', height: '100vh'}} className="auth-container">
                <Segment style={{padding: '30px'}} size="massive">
                    <Message>
                        <Message.Header>We're really sorry!</Message.Header>
                        <p>The requested page cannot be found :(</p>
                    </Message>
                    <LinkAsButton to={routesConstants.MAIN} className="fluid">
                        Go to main page!
                    </LinkAsButton>
                </Segment>
            </div>
        );
    }
}