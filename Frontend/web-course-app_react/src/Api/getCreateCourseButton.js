import React from 'react';
import {useNavigate} from "react-router-dom";

const CreateCourseButton = ({ isAuthenticated }) => {
    const navigate = useNavigate();

    return (
        isAuthenticated ? (
            <div className="create-course-container">
                <button className="create-course-button" onClick={() => navigate('/create')}>Create Course</button>
            </div>
        ) : null
    );
};

export default CreateCourseButton;
