import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import Card from './Card';
import Player from './Player';
import Pot from './Pot';
import './GameTable.css';

const GameTable = ({ playerName }) => {
    const navigate = useNavigate();
    const [players, setPlayers] = useState([
        { name: "<empty>", chips: 0 },
        { name: "<empty>", chips: 0 },
        { name: playerName, chips: 1500 }, // Your player
        { name: "<empty>", chips: 0 },
    ]);
    const [apiError, setApiError] = useState('');
    const communityCards = [{ rank: '?', suit: '' }, { rank: '?', suit: '' }, { rank: '?', suit: '' }];

    useEffect(() => {
        const checkPlayers = async () => {
            try {
                // Attempt to fetch data from the API
                const response = await fetch('/api/check-players');
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                const data = await response.json();
                setPlayers(data.players);
                setApiError(''); // Clear any existing error
            } catch (error) {
                // Catch any errors and set an error message
                setApiError('Failed to load players. Retrying...');
                console.error('There was a problem with the fetch operation:', error);
            }
        };

        // Call the function immediately, and then every 30 seconds
        checkPlayers();
        const intervalId = setInterval(checkPlayers, 30000);

        // Clear the interval on component unmount
        return () => clearInterval(intervalId);
    }, []);

    const handleExit = () => navigate('/lobby'); // Adjust the route as needed

    return (
        <div className="table-container">
            {apiError && <div className="api-error">{apiError}</div>}
            <div className="game-table">
                <div className="community-cards">
                    {/* Render face-down community cards */}
                    <div className="card facedown"></div>
                    <div className="card facedown"></div>
                    <div className="card facedown"></div>
                </div>
                <div className="pot">Pot: $300</div>
            </div>
            <div className="players">
                {/* Render player components */}
                {/* ... */}
            </div>
            <button className="exit-button">Exit Game</button>
        </div>
    );
};

export default GameTable;

