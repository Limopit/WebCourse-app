import {useState, useEffect, useImperativeHandle, forwardRef} from "react";
import { useNavigate } from "react-router-dom";
import { submitLessonForm } from "../../../Api/createNewEntity"

export const LessonForm = forwardRef(({ selectedButton, formData, onFormChange }, ref) => {
    const [localFormData, setLocalFormData] = useState({
        title: '',
        description: '',
        duration: 0,
        type: '',
        content: ''
    });

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
    };

    const handleSubmit = async () => {
        const requestData = {
            title: localFormData.title,
            description: localFormData.description,
            duration: localFormData.duration,
            type: localFormData.type,
            content: localFormData.content
        };

        try {
            const result = await submitLessonForm(requestData);
            if (result) {
                setLocalFormData({
                    title: '',
                    description: '',
                    duration: 0,
                    type: '',
                    content: ''
                });
                return result;
            } else {
                throw new Error("Lesson creation failed");
            }
        } catch (error) {
            console.error("Error while creating a lesson:", error);
            throw error;
        }
    };

    useImperativeHandle(ref, () => ({
        submit: async () => {
            return await handleSubmit();
        }
    }));

    return (
        <div className="form-container">
            <h2>Lesson Configuration: {selectedButton?.label}</h2>

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
                    <label>Duration (hours)</label>
                    <input
                        type="number"
                        name="duration"
                        value={localFormData.duration}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div className="form-group">
                    <label>Type</label>
                    <input
                        type="text"
                        name="type"
                        value={localFormData.type}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div className="form-group">
                    <label>Content</label>
                    <textarea
                        name="content"
                        value={localFormData.content}
                        onChange={handleChange}
                        required
                    />
                </div>
            </form>
        </div>
    );
});