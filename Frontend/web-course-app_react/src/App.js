// src/App.js
import './App.css';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import SignInSignUp from "./Components/Pages/SignInSignUp/SignInSignUp";
import BrainBrick from "./Components/Pages/BrainBrick/BrainBrick";

function App() {
    return (
        <Router>
            <div>
                <Routes>
                    <Route path="/auth" element={<SignInSignUp />} />
                    <Route path="/" element={<BrainBrick />} />
                </Routes>
            </div>
        </Router>
    );
}

export default App;
