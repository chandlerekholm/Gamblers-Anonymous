import React, { useState } from 'react';
import AuthForm from './components/AuthForm';
import Welcome from './components/Welcome';


const App = () => {
    const [user, setUser] = useState(null);

    const handleGuestLogin = (guestName) => {
        setUser(guestName || 'Guest');
    };

    if (user) {
        return <Welcome userName={user} />;
    }

    return <AuthForm onGuestLogin={handleGuestLogin} />;
};

export default App;
