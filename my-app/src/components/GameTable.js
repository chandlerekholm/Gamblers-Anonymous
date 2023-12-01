import React, { useEffect } from 'react';

const GameTable = () => {
    useEffect(() => {
        // Call backend to start a game session
        console.log('Starting a new game session...');
        // backend API call would go here
    }, []);

    return (
        <div>
            <h1>Texas Holdem Game</h1>
            <p>Game session is starting...</p>
            {/* placeholder for game visuals */}
            <div>
                {/* display cards, chips, etc. */}
                <p>Game table visuals go here...</p>
            </div>
        </div>
    );
};

export default GameTable;
