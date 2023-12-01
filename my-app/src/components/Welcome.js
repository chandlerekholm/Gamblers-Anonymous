import React from 'react';
import { useNavigate } from 'react-router-dom';

const Welcome = ({ userName }) => {
    const navigate = useNavigate();

    const handleQuickPlay = () => {
        // call your backend to search for open game lobby
        console.log('Searching for an open game lobby...');

        // Quick create game for testing purposes
        navigate('/game');
    };

    return (
        <div>
            <h1>Welcome, {userName}!</h1>
            <button onClick={() => navigate('/lobby')}>Create Game Lobby</button>
            <button onClick={() => navigate('/lobby')}>Join Game Lobby</button>
            <button onClick={handleQuickPlay}>Quick Play!</button>
        </div>
    );
};

export default Welcome;
