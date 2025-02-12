import { useState, useEffect, useImperativeHandle, forwardRef } from "react";
import { createCourseEntity } from "../../../Api/createNewEntity";
import { useNavigate } from "react-router-dom";

export const CourseForm = forwardRef(({ selectedButton, formData, onFormChange, onButtonLabelChange }, ref) => {
    const navigate = useNavigate();

    const [localFormData, setLocalFormData] = useState({
        title: '',
        description: '',
        level: '',
        category: '',
        language: '',
        requirements: '',
        logo: ''
    });

    const [showLogoInput, setShowLogoInput] = useState(false);

    useEffect(() => {
        if (formData) {
            setLocalFormData(formData);
        }
    }, [formData]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        const updatedData = {
            ...localFormData,
            [name]: value
        };
        setLocalFormData(updatedData);
        onFormChange(updatedData);

        if (name === 'title') {
            onButtonLabelChange(value || 'Course Config');
        }
    };

    const handleLogoSubmit = (e) => {
        e.preventDefault();
        setShowLogoInput(false);
    };

    const handleSubmit = async (lessonResults = []) => {
        const requestData = {
            title: localFormData.title,
            description: localFormData.description,
            logo: localFormData.logo,
            level: localFormData.level,
            category: localFormData.category,
            language: localFormData.language,
            requirements: localFormData.requirements,
            lessons: lessonResults
        };

        try {
            const result = await createCourseEntity(requestData);
            if (result) {
                navigate("/");
                return result;
            } else {
                throw new Error("Course creation failed");
            }
        } catch (error) {
            console.error("Error while creating a course:", error);
            throw error;
        }
    };

    useImperativeHandle(ref, () => ({
        submit: (lessonResults = []) => handleSubmit(lessonResults)
    }));

    return (
        <div className="form-container-content">
            <h2>Course Configuration: {selectedButton?.label}</h2>

            <div className="course-logo-container" onClick={() => setShowLogoInput(true)}>
                {localFormData.logo ? (
                    <img src={localFormData.logo} alt="Course Logo" />
                ) : (
                    <span>+</span>
                )}
            </div>

            {showLogoInput && (
                <form onSubmit={handleLogoSubmit} className="course-logo-form">
                    <input
                        type="text"
                        className="course-logo-input"
                        placeholder="Enter image URL"
                        value={localFormData.logo}
                        onChange={handleChange}
                        name="logo"
                        autoFocus
                    />
                    <button type="submit" className="apply-logo-button">✔</button>
                </form>
            )}

            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <label>Title</label>
                    <input
                        type="text"
                        name="title"
                        value={localFormData.title}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div className="form-group">
                    <label>Description</label>
                    <textarea
                        name="description"
                        value={localFormData.description}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div className="form-group">
                    <label>Level</label>
                    <input
                        type="text"
                        name="level"
                        value={localFormData.level}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div className="form-group">
                    <label>Category</label>
                    <input
                        type="text"
                        name="category"
                        value={localFormData.category}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div className="form-group">
                    <label>Language</label>
                    <input
                        type="text"
                        name="language"
                        value={localFormData.language}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div className="form-group">
                    <label>Requirements</label>
                    <textarea
                        name="requirements"
                        value={localFormData.requirements}
                        onChange={handleChange}
                        required
                    />
                </div>
            </form>
        </div>
    );
});