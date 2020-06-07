import { Responsive, Table } from 'semantic-ui-react';
import React, { Fragment } from 'react';

const ResponsiveTable = ({header, getContent}) => {
    return (
        <Fragment>
            <Responsive as={Table} celled size='small' minWidth={Responsive.onlyTablet.minWidth}>
                <Table.Header>
                    {header}
                </Table.Header>
                <Table.Body>
                    {getContent()}
                </Table.Body>
            </Responsive>
            <Responsive {...Responsive.onlyMobile}>
                {getContent()}
            </Responsive>
        </Fragment>
    )
};

export default ResponsiveTable;