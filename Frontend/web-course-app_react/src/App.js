import './App.css';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import SignInSignUp from "./Components/Pages/SignInSignUp/SignInSignUp";
import BrainBrick from "./Components/Pages/BrainBrick/BrainBrick";
import {AuthProvider} from "./Context/AuthContext";
import CreateCourse from "./Components/Pages/CreateCourse/CreateCourse";
import CourseContent from "./Components/Pages/CourseContent/CourseContent";
import PrivateRoute from "./Components/Elements/PrivateRoute/PrivateRoute";

function App() {
    return (
        <AuthProvider>
            <Router>
                <div>
                    <Routes>
                        <Route path="/auth" element={<SignInSignUp />} />
                        <Route path="/" element={<BrainBrick />} />
                        
                        <Route element={<PrivateRoute />}>
                            <Route path="/create" element={<CreateCourse />} />
                            <Route path="/courses/:id" element={<CourseContent />} />
                        </Route>
                    </Routes>
                </div>
            </Router>
        </AuthProvider>
    );
}

export default App;