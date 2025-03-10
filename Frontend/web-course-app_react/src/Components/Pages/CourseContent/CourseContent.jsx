import React, { useContext, useEffect, useState } from 'react';
import { useParams } from "react-router-dom";
import { AuthContext } from "../../../Context/AuthContext";
import CreateCourseButton from "../../../Api/getCreateCourseButton";
import AdditionalContent from "../../../Api/getAdditionalContent";
import Header from "../../Elements/Header/Header";
import "./CourseContent.css";
import { fetchCourseDetails } from "../../../Api/fetchCourses";
import ReactQuill from "react-quill";
import "react-quill/dist/quill.snow.css";

const CourseContent = () => {
    const { id } = useParams();
    const [course, setCourse] = useState(null);
    
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    
    const [activeItem, setActiveItem] = useState(null);
    
    const [userAnswers, setUserAnswers] = useState({});
    const [answerStatus, setAnswerStatus] = useState({});
    
    const { isAuthenticated } = useContext(AuthContext);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const data = await fetchCourseDetails(id);
                setCourse(data);
            } catch (err) {
                setError(err.message);
            } finally {
                setLoading(false);
            }
        };

        if (id) {
            fetchData();
        }
    }, [id]);

    const handleItemClick = (item) => {
        setActiveItem(item);
        setUserAnswers({});
        setAnswerStatus({});
    };

    const handleAnswerSelect = (questionId, selectedOption) => {
        setUserAnswers((prevAnswers) => ({
            ...prevAnswers,
            [questionId]: selectedOption,
        }));
    };

    const checkAnswers = () => {
        if (!activeItem || !activeItem.quizDetails) return;

        const newStatus = {};
        activeItem.quizDetails.forEach((quiz) => {
            newStatus[quiz.id] = userAnswers[quiz.id] === quiz.answer;
        });

        setAnswerStatus(newStatus);
    };

    if (loading) return <p>Loading course...</p>;
    if (error) return <p>Error: {error}</p>;
    if (!course) return <p>Course not found</p>;

    return (
        <div>
            <Header
                createCourseButton={<CreateCourseButton isAuthenticated={isAuthenticated} />}
                additionalContent={<AdditionalContent location={id} isAuthenticated={isAuthenticated} />}
            />

            <div className="course-content-wrapper">
                <div className="course-content-sidebar">
                    <h3>{course.title}</h3>
                    <ul>
                        {course.lessonDetails && course.lessonDetails.length > 0 ? (
                            course.lessonDetails.map((lesson) => (
                                <li
                                    key={lesson.id}
                                    onClick={() => handleItemClick(lesson)}
                                    className={activeItem === lesson ? "active" : ""}
                                >
                                    <strong>Lesson {lesson.order}:</strong> {lesson.title}
                                </li>
                            ))
                        ) : (
                            <p>No lessons available</p>
                        )}
                    </ul>
                </div>

                <div className="course-content-main">
                    {activeItem ? (
                        <>
                            <div className="lesson-info">
                                <h3>{activeItem.title}</h3>
                                <div>
                                    <ReactQuill
                                        value={activeItem.content || ""}
                                        readOnly={true}
                                        theme="snow"
                                        modules={{ toolbar: false }}
                                    />
                                </div>
                            </div>

                            {activeItem.quizDetails && activeItem.quizDetails.length > 0 && (
                                <div className="quiz-section">
                                    <h4>Quiz</h4>
                                    {activeItem.quizDetails.map((quiz) => (
                                        <div key={quiz.id} className="quiz-question">
                                            <p>
                                                <strong>Question {quiz.order}:</strong> {quiz.question}
                                                {answerStatus[quiz.id] !== undefined && (
                                                    <span className="answer-status">
                                                        {answerStatus[quiz.id] ? "✔️" : "❌"}
                                                    </span>
                                                )}
                                            </p>
                                            <div className="quiz-options">
                                                {quiz.options.map((option, optionIndex) => (
                                                    <label key={optionIndex}>
                                                        <input
                                                            type="radio"
                                                            name={`question-${quiz.id}`}
                                                            value={option}
                                                            onChange={() => handleAnswerSelect(quiz.id, option)}
                                                            checked={userAnswers[quiz.id] === option}
                                                        />
                                                        {option}
                                                    </label>
                                                ))}
                                            </div>
                                        </div>
                                    ))}
                                    <button onClick={checkAnswers} className="submit-quiz-button">
                                        Submit Answers
                                    </button>
                                </div>
                            )}
                        </>
                    ) : (
                        <p>Select a lesson to view details.</p>
                    )}
                </div>
            </div>
        </div>
    );
};

export default CourseContent;