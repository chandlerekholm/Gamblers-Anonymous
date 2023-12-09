import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import Card from './Card';
import Player from './Player';
import Pot from './Pot';
import './GameTable.css';

const GameTable = ({ playerName }) => {
    const navigate = useNavigate();
    const [players, setPlayers] = useState([
        { name: playerName, chips: 1500, isTurn: false },
        { name: "<empty>", chips: 0 },
        { name: "<empty>", chips: 0 }, // Your player
        { name: "<empty>", chips: 0 },
    ]);
    const [currentTurn, setCurrentTurn] = useState(null);
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
                setApiError('API error. Retrying...');
                console.error('There was a problem with the fetch operation:', error);
            }
        };
        const handlePlayerAction = (action) => {
            // API call sending the player's action to the backend
        };

        // Call the function immediately, and then every 30 seconds
        checkPlayers();
        const intervalId = setInterval(checkPlayers, 30000);

        // Clear the interval on component unmount
        return () => clearInterval(intervalId);
    }, []);

    const handlePlayerAction = (action) => {
        // API call for player's action
    };
    const handleExit = () => navigate('/lobby'); // Adjust the route as needed

    return (
        <div className="table-container">
            {apiError && (
                <>
                    <div className="api-error">{apiError}</div>
                    <p className="waiting-text">Waiting for other players to join...</p>
                </>
            )}
            <div className="game-table">
                <div className="community-cards">
                    {/* Render face-down community cards */}
                    <div className="card facedown"></div>
                    <div className="card facedown"></div>
                    <div className="card facedown"></div>
                </div>
                <Pot amount={300} />
            </div>
            <div className="player-container">
                {players.map((player, index) => (
                    <div className="player-box" key={index}>
                        <div>{player.name}</div>
                        <div className="chips">Chips: {player.chips}</div>
                        <div className="player-actions">
                            {/* Render buttons here, make sure to conditionally render based on player's turn */}
                            <button className="button">Fold</button>
                            <button className="button">Call</button>
                            <button className="button">Raise</button>
                        </div>
                    </div>
                ))}
            </div>

            <button className="exit-button" onClick={handleExit}>Exit Game</button>
        </div>
    );
};

export default GameTable;

