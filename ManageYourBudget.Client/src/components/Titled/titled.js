import React from 'react';
import DocumentTitle from 'react-document-title';


const Titled = ({title, children}) => {
    title = `${title} - Manage Your Budget`;
  return (
      <DocumentTitle title={title}>{children}</DocumentTitle>
  )
};

export default Titled;