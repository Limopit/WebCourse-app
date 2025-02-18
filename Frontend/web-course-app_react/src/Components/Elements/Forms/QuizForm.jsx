import React from "react";
import {validateQuiz} from "../../../Api/validationHandler";

const QuizForm = ({ quiz, index, onChange, onRemove }) => {
    const [errors, setErrors] = React.useState({
        question: '',
        options: [],
        answer: ''
    });

    const handleChange = (e) => {
        const { name, value } = e.target;
        const updatedQuiz = { ...quiz, [name]: value };
        
        onChange(index, updatedQuiz);
        
        const errors = validateQuiz(updatedQuiz);
        setErrors(errors);
    };

    const handleOptionChange = (optionIndex, value) => {
        const newOptions = [...quiz.options];
        newOptions[optionIndex] = value;
        
        const updatedQuiz = { ...quiz, options: newOptions };
        onChange(index, updatedQuiz);
        
        const errors = validateQuiz(updatedQuiz);
        setErrors(errors);
    };

    const addOption = () => {
        const updatedQuiz = { ...quiz, options: [...quiz.options, ''] };
        onChange(index, updatedQuiz);
        
        const errors = validateQuiz(updatedQuiz);
        setErrors(errors);
    };

    const removeOption = (optionIndex) => {
        const newOptions = quiz.options.filter((_, i) => i !== optionIndex);
        const updatedQuiz = { ...quiz, options: newOptions };
        onChange(index, updatedQuiz);
        
        const errors = validateQuiz(updatedQuiz);
        setErrors(errors);
    };

    return (
        <div className="quiz-form">
            <h3>Question {index + 1}</h3>
            
            <div className="form-group">
                <label>Question</label>
                <input
                    type="text"
                    name="question"
                    value={quiz.question}
                    onChange={handleChange}
                    required
                />
                {errors.question && <span className="error-message">{errors.question}</span>}
            </div>
            <div className="form-group">
                <label>Options</label>
                {quiz.options.map((option, optionIndex) => (
                    <div key={optionIndex} className="option-container">
                        <input
                            type="text"
                            value={option}
                            onChange={(e) => handleOptionChange(optionIndex, e.target.value)}
                            required
                        />
                        <button
                            type="button"
                            onClick={() => removeOption(optionIndex)}
                            className="remove-option-button"
                        >
                            ×
                        </button>
                    </div>
                ))}
                {errors.options && <span className="error-message">{errors.options}</span>}
                <button type="button" onClick={addOption} className="add-option-button">
                    Add Option
                </button>
            </div>
            <div className="form-group">
                <label>Correct Answer</label>
                <input
                    type="text"
                    name="answer"
                    value={quiz.answer}
                    onChange={handleChange}
                    required
                />
                {errors.answer && <span className="error-message">{errors.answer}</span>}
            </div>
            <button type="button" onClick={() => onRemove(index)} className="remove-quiz-button">
                Remove Quiz
            </button>
        </div>
    );
};

export default QuizForm;