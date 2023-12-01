import React from 'react';
import './Card.css';

const Card = ({ rank, suit }) => {
    return (
        <div className={`card ${suit}`}>
            {rank} {suit}
        </div>
    );
};

export default Card;
