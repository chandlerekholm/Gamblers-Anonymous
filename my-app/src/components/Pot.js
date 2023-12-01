import React from 'react';
import './Pot.css';

const Pot = ({ amount }) => {
    return (
        <div className="pot">Pot: ${amount}</div>
    );
};

export default Pot;
