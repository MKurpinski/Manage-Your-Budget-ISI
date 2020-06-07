import React from 'react';
import ModifyExpenseModal from './modifyExpenseModal';

const ModifyCyclicExpenseModal = (props) => {
  return <ModifyExpenseModal isCyclic={true} {...props}/>
};

export default ModifyCyclicExpenseModal;