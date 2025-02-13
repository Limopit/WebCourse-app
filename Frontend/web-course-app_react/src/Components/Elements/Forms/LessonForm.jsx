import { useState, useEffect, useRef, useImperativeHandle, forwardRef } from "react";
import { createQuizEntity, createLessonEntity } from "../../../Api/createNewEntity";
import RichTextEditor from "../RichTextEditor/RichTextEditor";
import QuizForm from "./QuizForm";
import { hasErrors, validateField, validateForm } from "../../../Api/validationHandler";

export const LessonForm = forwardRef(({ selectedButton, formData, onFormChange, onButtonLabelChange, quizzes, onQuizzesChange, onQuizRemove }, ref) => {
    const [localFormData, setLocalFormData] = useState({
        title: '',
        description: '',
        duration: 0,
        content: ''
    });

    const [isExpanded, setIsExpanded] = useState(false);
    const [errors, setErrors] = useState({});
    const richTextEditorRef = useRef(null);

    const validationExcludedFields = [];

    useEffect(() => {
        if (formData && Object.keys(formData).length > 0) {
            setLocalFormData(formData);
        } else {
            setLocalFormData({
                title: '',
                description: '',
                duration: 0,
                content: ''
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
            onButtonLabelChange(value || `Lesson ${selectedButton.id}`);
        }

        setErrors((prevErrors) => ({
            ...prevErrors,
            [name]: validationExcludedFields.includes(name) ? "" : validateField(name, value),
        }));
    };

    const handleContentChange = (content) => {
        const updatedData = {
            ...localFormData,
            content: content
        };
        setLocalFormData(updatedData);
        onFormChange(updatedData);
    };

    const handleQuizChange = (index, quiz) => {
        const newQuizzes = [...quizzes];
        newQuizzes[index] = quiz;
        onQuizzesChange(newQuizzes);
    };

    const handleQuizRemove = (index) => {
        const newQuizzes = quizzes.filter((_, i) => i !== index);
        onQuizzesChange(newQuizzes);
    };

    const handleSubmit = async () => {
        const formErrors = validateForm(localFormData, validationExcludedFields);
        setErrors(formErrors);
        console.error(formErrors);

        if (hasErrors(formErrors)) {
            console.error("Form has errors. Please fix them before submitting.");
            return;
        }

        try {
            const quizIds = await Promise.all(
                quizzes.map(async (quiz) => {
                    const quizData = {
                        question: quiz.question,
                        options: quiz.options,
                        answer: quiz.answer
                    };
                    const quizResult = await createQuizEntity(quizData);
                    return quizResult;
                })
            );

            const requestData = {
                title: localFormData.title,
                description: localFormData.description,
                duration: localFormData.duration,
                content: localFormData.content,
                Quizzes: quizIds
            };

            const result = await createLessonEntity(requestData);
            if (result) {
                setLocalFormData({
                    title: '',
                    description: '',
                    duration: 0,
                    content: ''
                });
                onQuizzesChange([]);
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
        },
        validate: () => {
            const formErrors = validateForm(localFormData, validationExcludedFields);
            setErrors(formErrors);
            return formErrors;
        }
    }));

    return (
        <div className="form-container-content">
            <h2>Lesson Configuration: {selectedButton?.label}</h2>

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
                    <label>Duration (hours)</label>
                    <input
                        type="number"
                        name="duration"
                        value={localFormData.duration}
                        onChange={handleChange}
                        onBlur={(e) => setErrors((prevErrors) => ({
                            ...prevErrors,
                            duration: validateField("duration", e.target.value)
                        }))}
                        required
                    />
                    {errors.duration && <span className="error-message">{errors.duration}</span>}
                </div>
                <div className="form-group">
                    <label>Content</label>
                    <div
                        ref={richTextEditorRef}
                        className={`rich-text-editor-wrapper ${isExpanded ? "expanded" : ""}`}
                    >
                        <RichTextEditor
                            value={localFormData.content || ""}
                            onChange={handleContentChange}
                            onFocus={() => setIsExpanded(true)}
                            onBlur={() => setIsExpanded(false)}
                            isExpanded={isExpanded}
                        />
                    </div>
                    {errors.content && <span className="error-message">{errors.content}</span>}
                </div>
                <div className="quizzes-section">
                    {quizzes.map((quiz, index) => (
                        <QuizForm
                            key={index}
                            quiz={quiz}
                            index={index}
                            onChange={handleQuizChange}
                            onRemove={handleQuizRemove}
                        />
                    ))}
                    <button
                        type="button"
                        onClick={() => onQuizzesChange([...quizzes, { question: '', options: [], answer: '' }])}
                        className="add-quiz-button"
                    >
                        Add Quiz
                    </button>
                </div>
            </form>
        </div>
    );
});