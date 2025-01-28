// src/App.js
import './App.css';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import SignInSignUp from "./Components/SignInSignUp/SignInSignUp";

function App() {
    return (
        <Router>
            <div>
                <Routes>
                    <Route path="/auth" element={<SignInSignUp />} />
                    <Route path="/" element={<h1>Home Page</h1>} />
                </Routes>
            </div>
        </Router>
    );
}

export default App;
