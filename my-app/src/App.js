import React, { useState } from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import AuthForm from './components/AuthForm';
import Welcome from './components/Welcome';
import GameLobby from './components/GameLobby';
import GameTable from './components/GameTable';

const App = () => {
    const [user, setUser] = useState(null);

    const handleGuestLogin = (guestName) => {
        setUser(guestName || 'Guest');
    };

    return (
        <Router>
            <Routes>
                <Route path="/" element={user ? <Welcome userName={user} /> : <AuthForm onGuestLogin={handleGuestLogin} />} />
                <Route path="/welcome" element={<Welcome />} />
                <Route path="/lobby" element={<GameLobby />} />
                <Route path="/game" element={<GameTable />} />
                {/* Add other routes as needed */}
            </Routes>
        </Router>
    );
};

export default App;
