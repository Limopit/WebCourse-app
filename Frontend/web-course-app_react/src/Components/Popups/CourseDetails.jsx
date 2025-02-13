import React, { useState } from "react";
import "./CourseDetails.css";
import ReactQuill from "react-quill";
import { useNavigate } from "react-router-dom";
import { createTakenCourseRecord } from "../../Api/createNewEntity";

const CourseDetails = ({ course, onClose, takenCourses }) => {
    const [activeItem, setActiveItem] = useState(null);
    const navigate = useNavigate();

    const isCourseTaken = takenCourses.some(takenCourse => takenCourse.id === course.id);

    const handleItemClick = (item) => {
        setActiveItem(item);
    };

    const handleTakeCourse = async (id) => {
        if (isCourseTaken) {
            navigate(`courses/${id}`);
        } else {
            await createTakenCourseRecord(id);
            navigate(`courses/${id}`);
        }
    };

    const isLesson = (item) => {
        return item.duration !== undefined && item.quizDetails !== undefined;
    };

    return (
        <div className="course-details-overlay">
            <div className="course-details-container">
                <button className="close-button" onClick={onClose}>×</button>
                
                <div className="header-container">
                    <div className="course-logo">
                        {course.logo ? (
                            <img src={course.logo} alt="Course Logo" />
                        ) : (
                            <div className="logo-placeholder">No Logo Available</div>
                        )}
                    </div>
                    <h2>{course.title}</h2>
                </div>
                
                <div className="course-content">
                    <div className="course-items">
                        <h3>Course Content</h3>
                        <ul>
                            <li onClick={() => handleItemClick(course)}>
                                <strong>Course:</strong> {course.title}
                            </li>
                            {course.lessonDetails.map((lesson, index) => (
                                <li key={index} onClick={() => handleItemClick(lesson)}>
                                    <strong>Lesson {index + 1}:</strong> {lesson.title}
                                </li>
                            ))}
                        </ul>
                        <button
                            className="take-course-button"
                            onClick={() => handleTakeCourse(course.id)}
                        >
                            {isCourseTaken ? "Continue a course" : "Take a course"}
                        </button>
                    </div>
                    
                    <div className="item-details">
                        {activeItem ? (
                            <>
                                {isLesson(activeItem) ? (
                                    <div className="lesson-info">
                                        <h3>{activeItem.title}</h3>
                                        <p className="item-description">{activeItem.description}</p>
                                        <div className="lesson-meta">
                                            <p><strong>Duration:</strong> {activeItem.duration} hours</p>
                                            <p><strong>Type:</strong> Lesson</p>
                                        </div>
                                        <div>
                                            <h4>Content</h4>
                                            <ReactQuill
                                                value={activeItem.content || "<p>No content available.</p>"}
                                                readOnly={true}
                                                theme="snow"
                                                modules={{ toolbar: false }}
                                            />
                                        </div>
                                        {activeItem.quizDetails && activeItem.quizDetails.length > 0 && (
                                            <div>
                                                <h4>Quizzes</h4>
                                                {activeItem.quizDetails.map((quiz, quizIndex) => (
                                                    <div key={quizIndex} className="quiz-info">
                                                        <p><strong>Question:</strong> {quiz.question}</p>
                                                        <p><strong>Options:</strong> {quiz.options.join(", ")}</p>
                                                        <p><strong>Answer:</strong> {quiz.answer}</p>
                                                    </div>
                                                ))}
                                            </div>
                                        )}
                                    </div>
                                ) : (
                                    <div className="course-info">
                                        <div className="course-details-info">
                                            <h3>Course Description</h3>
                                            <p className="item-description">{activeItem.description}</p>
                                            <div className="course-meta">
                                                <p><strong>Level:</strong> {course.level}</p>
                                                <p><strong>Category:</strong> {course.category}</p>
                                                <p><strong>Language:</strong> {course.language}</p>
                                            </div>
                                            <div className="course-dates">
                                                <p><strong>Creation Date:</strong> {new Date(course.creationDate).toLocaleDateString()}</p>
                                                <p><strong>Update Date:</strong> {new Date(course.updateDate).toLocaleDateString()}</p>
                                            </div>
                                            <div>
                                                <h4>Requirements</h4>
                                                <p>{course.requierments || "No requirements available."}</p>
                                            </div>
                                        </div>
                                    </div>
                                )}
                            </>
                        ) : (
                            <p>Select an item to view details.</p>
                        )}
                    </div>
                </div>
            </div>
        </div>
    );
};

export default CourseDetails;