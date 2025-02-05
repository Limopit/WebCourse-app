import React, { useState, useContext } from 'react';
import "./CreateCourse.css";
import AdditionalContent from "../../../Api/getAdditionalContent";
import Header from "../../Elements/Header/Header";
import { useLocation } from "react-router-dom";
import { AuthContext } from "../../../Context/AuthContext";
import { CourseForm } from "../../Elements/CourseForm/CourseForm";

const CreateCourse = () => {
    const location = useLocation();
    const { isAuthenticated } = useContext(AuthContext);

    const [buttons, setButtons] = useState([{ id: 1, label: 'Course Config' }]);
    const [animating, setAnimating] = useState(false);
    const [selectedButton, setSelectedButton] = useState(null);
    const [showForm, setShowForm] = useState(false);

    const addButton = () => {
        setAnimating(true);
        const newButton = { id: Date.now(), label: `Lesson ${buttons.length + 1}` };
        setButtons(prevButtons => [...prevButtons, newButton]);

        setTimeout(() => {
            setAnimating(false);
        }, 100);
    };

    return (
        <div>
            <Header additionalContent={<AdditionalContent location={location} isAuthenticated={isAuthenticated} />} />
            <div className="course-form-container">
                {/* Контейнер с кнопками */}
                <div className="content-container">
                    <div className="course-config-container">
                        <div className="buttons-container">
                            {buttons.map((button, index) => (
                                <button
                                    key={button.id}
                                    className={`course-config-page ${animating && index === buttons.length - 1 ? 'adding' : ''}`}
                                    onClick={() => {
                                        setSelectedButton(button);
                                        setShowForm(true);
                                    }}
                                >
                                    {button.label}
                                </button>
                            ))}
                            <button className="add-button" onClick={addButton}>+</button>
                        </div>
                    </div>
                </div>

                {/* Форма теперь отдельно, справа от контейнера с кнопками */}
                {showForm && (
                    <div className="form-container">
                        <CourseForm selectedButton={selectedButton} />
                    </div>
                )}
            </div>
        </div>
    );
};

export default CreateCourse;