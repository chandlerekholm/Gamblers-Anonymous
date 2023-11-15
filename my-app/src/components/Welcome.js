import React from 'react';

const Welcome = ({ userName }) => {
    return (
        <div style={{ backgroundColor: '#121212', color: 'white', padding: '20px', textAlign: 'center' }}>
            <h2>Welcome {userName}</h2>
            <button style={buttonStyle}>Create Game Lobby</button>
            <button style={buttonStyle}>Join Game Lobby</button>
        </div>
    );
};

const buttonStyle = {
    width: '100%',
    padding: '10px',
    backgroundColor: '#1DB954',
    color: 'white',
    border: 'none',
    borderRadius: '5px',
    cursor: 'pointer',
};

export default Welcome;