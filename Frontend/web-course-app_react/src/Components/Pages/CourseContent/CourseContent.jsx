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
    const { isAuthenticated } = useContext(AuthContext);
    const [course, setCourse] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [activeItem, setActiveItem] = useState(null);

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
                            course.lessonDetails.map((lesson, index) => (
                                <li
                                    key={index}
                                    onClick={() => handleItemClick(lesson)}
                                    className={activeItem === lesson ? "active" : ""}
                                >
                                    <strong>Lesson {index + 1}:</strong> {lesson.title}
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
                            {activeItem.type === "lesson" && (
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