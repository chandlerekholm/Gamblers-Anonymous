import React, { useState } from 'react';

const AuthForm = ({ onGuestLogin }) => {
    const [isLogin, setIsLogin] = useState(true);
    const [isGuest, setIsGuest] = useState(false);
    const [guestName, setGuestName] = useState('');

    const toggleMode = () => {
        setIsLogin(!isLogin);
        setIsGuest(false);
    };

    const handleGuestLogin = () => {
        setIsGuest(true);
    };

    const handleGuestNameSubmit = () => {
        onGuestLogin(guestName);
    };

    if (isGuest) {
        return (
            <div style={formStyle}>
                <input
                    type="text"
                    placeholder="Enter your guest name"
                    value={guestName}
                    onChange={(e) => setGuestName(e.target.value)}
                    style={inputStyle}
                />
                <button onClick={handleGuestNameSubmit} style={buttonStyle}>
                    Continue as Guest
                </button>
            </div>
        );
    }
    

    return (
        <div style={{ backgroundColor: '#121212', color: 'white', padding: '20px', maxWidth: '300px', margin: 'auto', borderRadius: '10px' }}>
            <h2>{isLogin ? 'Login' : 'Sign Up'}</h2>
            <form>
                {!isLogin && (
                    <input
                        type="text"
                        placeholder="Username"
                        style={inputStyle}
                    />
                )}
                <input
                    type="email"
                    placeholder="Email"
                    style={inputStyle}
                />
                <input
                    type="password"
                    placeholder="Password"
                    style={inputStyle}
                />
                <button type="submit" style={buttonStyle}>
                    {isLogin ? 'Login' : 'Sign Up'}
                </button>
            </form>
            <button onClick={toggleMode} style={toggleStyle}>
                {isLogin ? 'Need an account? Sign up' : 'Already have an account? Login'}
            </button>
            <button onClick={handleGuestLogin} style={buttonStyle}>
                Play as Guest
            </button>
        </div>
    );
};
const formStyle = {
    backgroundColor: '#121212',
    color: 'white',
    padding: '20px',
    maxWidth: '300px',
    margin: 'auto',
    borderRadius: '10px',
};

const inputStyle = {
    display: 'block',
    width: '100%',
    padding: '10px',
    margin: '10px 0',
    borderRadius: '5px',
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

const toggleStyle = {
    marginTop: '15px',
    backgroundColor: 'transparent',
    color: '#1DB954',
    border: 'none',
    cursor: 'pointer',
};

export default AuthForm;
