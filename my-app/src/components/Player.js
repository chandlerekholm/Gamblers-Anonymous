import React from 'react';
import './Player.css';

const Player = ({ name, chips }) => {
    return (
        <div className="player">
            <div className="player-name">{name}</div>
            <div className="player-chips">Chips: {chips}</div>
        </div>
    );
};

export default Player;
