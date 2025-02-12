import React from "react";

const QuizForm = ({ quiz, index, onChange, onRemove }) => {
    const handleChange = (e) => {
        const { name, value } = e.target;
        onChange(index, { ...quiz, [name]: value });
    };

    const handleOptionChange = (optionIndex, value) => {
        const newOptions = [...quiz.options];
        newOptions[optionIndex] = value;
        onChange(index, { ...quiz, options: newOptions });
    };

    const addOption = () => {
        onChange(index, { ...quiz, options: [...quiz.options, ''] });
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
            </div>
            <div className="form-group">
                <label>Options</label>
                {quiz.options.map((option, optionIndex) => (
                    <input
                        key={optionIndex}
                        type="text"
                        value={option}
                        onChange={(e) => handleOptionChange(optionIndex, e.target.value)}
                        required
                    />
                ))}
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
            </div>
            <button type="button" onClick={() => onRemove(index)} className="remove-quiz-button">
                Remove Quiz
            </button>
        </div>
    );
};

export default QuizForm;