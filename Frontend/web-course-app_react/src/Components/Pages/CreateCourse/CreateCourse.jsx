import React, { useState, useContext, useRef } from 'react';
import "./CreateCourse.css";
import "../../Elements/Forms/Form.css";
import AdditionalContent from "../../../Api/getAdditionalContent";
import Header from "../../Elements/Header/Header";
import { useLocation } from "react-router-dom";
import { AuthContext } from "../../../Context/AuthContext";
import { CourseForm } from "../../Elements/Forms/CourseForm";
import { LessonForm } from "../../Elements/Forms/LessonForm";
import {hasErrors} from "../../../Api/validationHandler";

const CreateCourse = () => {
    const location = useLocation();
    const { isAuthenticated } = useContext(AuthContext);

    const [buttons, setButtons] = useState([{ id: 1, label: 'New Course', type: 'course' }]);
    
    const [animating, setAnimating] = useState(false);
    
    const [activeFormId, setActiveFormId] = useState(buttons[0].id);
    const [formsData, setFormsData] = useState({});
    const [quizzes, setQuizzes] = useState({});
    const formRefs = useRef({});

    const addButton = () => {
        setAnimating(true);
        const newButton = { id: Date.now(), label: `Lesson ${buttons.length}`, type: 'lesson' };
        setButtons(prevButtons => [...prevButtons, newButton]);

        setTimeout(() => {
            setAnimating(false);
        }, 100);
    };

    const handleFormChange = (buttonId, data) => {
        setFormsData(prevFormsData => ({
            ...prevFormsData,
            [buttonId]: data,
        }));
    };

    const handleQuizzesChange = (buttonId, newQuizzes) => {
        setQuizzes(prevQuizzes => ({
            ...prevQuizzes,
            [buttonId]: newQuizzes,
        }));
    };

    const handleQuizRemove = (buttonId, index) => {
        setQuizzes(prevQuizzes => ({
            ...prevQuizzes,
            [buttonId]: prevQuizzes[buttonId].filter((_, i) => i !== index),
        }));
    };

    const updateButtonLabel = (buttonId, newLabel) => {
        setButtons(prevButtons =>
            prevButtons.map(button =>
                button.id === buttonId ? { ...button, label: newLabel } : button
            )
        );
    };

    const handleSaveAll = async () => {
        try {
            const lessonResults = [];
            let hasValidationErrors = false;

            for (const button of buttons) {
                const formRef = formRefs.current[button.id];
                const formErrors = await formRef.validate();
                if (hasErrors(formErrors)) {
                    console.log(formErrors);
                    hasValidationErrors = true;
                    console.error(`Form ${button.label} has errors.`);
                }
            }

            if (hasValidationErrors) {
                alert("Please fix all errors before saving.");
                return;
            }

            for (const button of buttons) {
                if (button.type === 'lesson') {
                    const formRef = formRefs.current[button.id];
                    const result = await formRef.submit();
                    lessonResults.push(result);
                }
            }

            const courseButton = buttons.find(button => button.type === 'course');
            const formRef = formRefs.current[courseButton.id];
            await formRef.submit(lessonResults);
        } catch (error) {
            console.log(error);
            alert("Error saving data");
        }
    };

    return (
        <div>
            <Header additionalContent={<AdditionalContent location={location} isAuthenticated={isAuthenticated} />} />
            
            <div className="course-form-container">
                <div className="content-container">
                    <div className="course-config-container">
                        <div className="buttons-container">
                            {buttons.map((button, index) => (
                                <button
                                    key={button.id}
                                    className={`course-config-page ${animating && index === buttons.length - 1 ? 'adding' : ''} ${index >= 1 ? 'lesson' : ''}`}
                                    onClick={() => setActiveFormId(button.id)}
                                >
                                    {button.label}
                                </button>
                            ))}
                            <button className="add-button" onClick={addButton}>+</button>
                        </div>
                    </div>
                </div>

                {buttons.map(button => (
                    <div key={button.id} className="form-container" style={{ display: activeFormId === button.id ? 'block' : 'none' }}>
                        {button.type === 'course' ? (
                            <CourseForm
                                ref={(el) => (formRefs.current[button.id] = el)}
                                selectedButton={button}
                                formData={formsData[button.id] || {}}
                                onFormChange={(data) => handleFormChange(button.id, { ...data, type: 'course' })}
                                onButtonLabelChange={(newLabel) => updateButtonLabel(button.id, newLabel)}
                            />
                        ) : (
                            <LessonForm
                                ref={(el) => (formRefs.current[button.id] = el)}
                                selectedButton={button}
                                formData={formsData[button.id] || {}}
                                onFormChange={(data) => handleFormChange(button.id, { ...data, type: 'lesson' })}
                                onButtonLabelChange={(newLabel) => updateButtonLabel(button.id, newLabel)}
                                quizzes={quizzes[button.id] || []}
                                onQuizzesChange={(newQuizzes) => handleQuizzesChange(button.id, newQuizzes)}
                                onQuizRemove={(index) => handleQuizRemove(button.id, index)}
                            />
                        )}
                    </div>
                ))}

                <button className="save-all-button" onClick={handleSaveAll}>Create Course</button>
            </div>
        </div>
    );
};

export default CreateCourse;