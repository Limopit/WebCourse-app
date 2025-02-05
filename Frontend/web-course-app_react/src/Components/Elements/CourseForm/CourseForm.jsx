import './CourseForm.css';
import { useState } from "react";
import { submitCourseForm } from "../../../Api/createNewCourse";

export const CourseForm = ({ selectedButton }) => {
    const [formData, setFormData] = useState({
        title: '',
        description: '',
        level: '',
        category: '',
        language: '',
        requirements: '',
        logo: ''
    });

    const [showLogoInput, setShowLogoInput] = useState(false);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData((prevData) => ({
            ...prevData,
            [name]: value
        }));
    };

    const handleLogoSubmit = (e) => {
        e.preventDefault();
        setShowLogoInput(false);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        const requestData = {
            title: formData.title,
            description: formData.description,
            logo: formData.logo,
            level: formData.level,
            category: formData.category,
            language: formData.language,
            requirements: formData.requirements
        };

        const result = await submitCourseForm(requestData);

        if (result) {
            setFormData({
                title: '',
                description: '',
                level: '',
                category: '',
                language: '',
                requirements: '',
                logo: ''
            });
        } else {
            alert("Error while creating a course");
        }
    };

    return (
        <div className="course-form">
            <h2>Course Configuration: {selectedButton?.label}</h2>

            <div className="course-logo-container" onClick={() => setShowLogoInput(true)}>
                {formData.logo ? (
                    <img src={formData.logo} alt="Course Logo" />
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
                        value={formData.logo}
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
                        value={formData.title}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div className="form-group">
                    <label>Description</label>
                    <textarea
                        name="description"
                        value={formData.description}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div className="form-group">
                    <label>Level</label>
                    <input
                        type="text"
                        name="level"
                        value={formData.level}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div className="form-group">
                    <label>Category</label>
                    <input
                        type="text"
                        name="category"
                        value={formData.category}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div className="form-group">
                    <label>Language</label>
                    <input
                        type="text"
                        name="language"
                        value={formData.language}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div className="form-group">
                    <label>Requirements</label>
                    <textarea
                        name="requirements"
                        value={formData.requirements}
                        onChange={handleChange}
                        required
                    />
                </div>
                <button type="submit" className="submit-button">Save</button>
            </form>
        </div>
    );
};
