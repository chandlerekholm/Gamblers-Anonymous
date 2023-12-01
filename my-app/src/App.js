import React, { useState } from 'react';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import AuthForm from './AuthForm';
import Welcome from './Welcome';
import GameLobby from './GameLobby';
import GameTable from './GameTable';

const App = () => {
    const [user, setUser] = useState(null);

    const handleGuestLogin = (guestName) => {
        setUser(guestName || 'Guest');
    };

    return (
        <Router>
            <Switch>
                <Route path="/" exact>
                    {user ? <Welcome userName={user} /> : <AuthForm onGuestLogin={handleGuestLogin} />}
                </Route>
                <Route path="/welcome" component={Welcome} />
                <Route path="/lobby" component={GameLobby} />
                <Route path="/game" component={GameTable} />
                {/* Add other routes as needed */}
            </Switch>
        </Router>
    );
};

export default App;
