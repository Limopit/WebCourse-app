import React, { useState } from "react";
import "./CourseDetails.css";
import ReactQuill from "react-quill";
import {useNavigate} from "react-router-dom";
import {createTakenCourseRecord} from "../../Api/createNewEntity";

const CourseDetails = ({ course, onClose }) => {
    const [activeItem, setActiveItem] = useState(null);
    const navigate = useNavigate();

    const handleItemClick = (item) => {
        setActiveItem(item);
    };

    const handleTakeCourse = async (id) => {
        await createTakenCourseRecord(id);
        navigate(`courses/${id}`);
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
                    <button className="take-course-button" onClick={() => handleTakeCourse(course.id)}>Take a course</button> 
                    </div>
                    <div className="item-details">
                        {activeItem ? (
                            <>
                                {activeItem.type === "lesson" && (
                                    <div className="lesson-info">
                                        <h3>{activeItem.title}</h3>
                                        <p className="item-description">{activeItem.description}</p>
                                        <div className="lesson-meta">
                                            <p><strong>Duration:</strong> {activeItem.duration} hours</p>
                                            <p><strong>Type:</strong> {activeItem.type}</p>
                                        </div>
                                        <div>
                                            <h2>Сохраненное содержимое</h2>
                                            <ReactQuill
                                                value={activeItem.content}
                                                readOnly={true}
                                                theme="snow"
                                                modules={{toolbar: false}}
                                            />
                                        </div>
                                    </div>
                                )}

                                {activeItem === course && (
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
