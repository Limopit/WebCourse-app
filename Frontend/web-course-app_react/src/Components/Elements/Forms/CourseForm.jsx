import { useState, useEffect, useImperativeHandle, forwardRef } from "react";
import { createCourseEntity } from "../../../Api/createNewEntity";
import { useNavigate } from "react-router-dom";
import { validateField, validateForm, hasErrors } from "../../../Api/validationHandler";

export const CourseForm = forwardRef(({ selectedButton, formData, onFormChange, onButtonLabelChange }, ref) => {
    const navigate = useNavigate();

    const [localFormData, setLocalFormData] = useState({});

    const [showLogoInput, setShowLogoInput] = useState(false);
    
    const [errors, setErrors] = useState({});
    const validationExcludedFields = ['logo']

    useEffect(() => {
        if (formData && Object.keys(formData).length > 0) {
            setLocalFormData(formData);
        } else {
            setLocalFormData({
                title: '',
                description: '',
                level: '',
                category: '',
                language: '',
                requirements: '',
                logo: ''
            });
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

        setErrors((prevErrors) => ({
            ...prevErrors,
            [name]: validationExcludedFields.includes(name) ? "" : validateField(name, value),
        }));
    };

    const handleLogoSubmit = (e) => {
        e.preventDefault();
        setShowLogoInput(false);
    };

    const handleSubmit = async (lessonResults = []) => {
        const formErrors = validateForm(localFormData, validationExcludedFields);
        setErrors(formErrors);
        if (hasErrors(formErrors)) {
            console.error(formErrors);
            console.error("Form has errors. Please fix them before submitting.");
            return;
        }

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
        submit: (lessonResults = []) => handleSubmit(lessonResults),
        validate: () => {
            const formErrors = validateForm(localFormData, validationExcludedFields);
            setErrors(formErrors);
            return formErrors;
        }
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
                    {errors.logo && <span className="error-message">{errors.logo}</span>}
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
                        onBlur={(e) => setErrors((prevErrors) => ({
                            ...prevErrors,
                            title: validateField("title", e.target.value)
                        }))}
                        required
                    />
                    {errors.title && <span className="error-message">{errors.title}</span>}
                </div>
                <div className="form-group">
                    <label>Description</label>
                    <textarea
                        name="description"
                        value={localFormData.description}
                        onChange={handleChange}
                        onBlur={(e) => setErrors((prevErrors) => ({
                            ...prevErrors,
                            description: validateField("description", e.target.value)
                        }))}
                        required
                    />
                    {errors.description && <span className="error-message">{errors.description}</span>}
                </div>
                <div className="form-group">
                    <label>Level</label>
                    <input
                        type="text"
                        name="level"
                        value={localFormData.level}
                        onChange={handleChange}
                        onBlur={(e) => setErrors((prevErrors) => ({
                            ...prevErrors,
                            level: validateField("level", e.target.value)
                        }))}
                        required
                    />
                    {errors.level && <span className="error-message">{errors.level}</span>}
                </div>
                <div className="form-group">
                    <label>Category</label>
                    <input
                        type="text"
                        name="category"
                        value={localFormData.category}
                        onChange={handleChange}
                        onBlur={(e) => setErrors((prevErrors) => ({
                            ...prevErrors,
                            category: validateField("category", e.target.value)
                        }))}
                        required
                    />
                    {errors.category && <span className="error-message">{errors.category}</span>}
                </div>
                <div className="form-group">
                    <label>Language</label>
                    <input
                        type="text"
                        name="language"
                        value={localFormData.language}
                        onChange={handleChange}
                        onBlur={(e) => setErrors((prevErrors) => ({
                            ...prevErrors,
                            language: validateField("language", e.target.value)
                        }))}
                        required
                    />
                    {errors.language && <span className="error-message">{errors.language}</span>}
                </div>
                <div className="form-group">
                    <label>Requirements</label>
                    <textarea
                        name="requirements"
                        value={localFormData.requirements}
                        onChange={handleChange}
                        onBlur={(e) => setErrors((prevErrors) => ({
                            ...prevErrors,
                            requirements: validateField("requirements", e.target.value)
                        }))}
                        required
                    />
                    {errors.requirements && <span className="error-message">{errors.requirements}</span>}
                </div>
            </form>
        </div>
    );
});