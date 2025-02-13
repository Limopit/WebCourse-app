export const validateField = (fieldName, value) => {
    if (fieldName === "duration") {
        if (typeof value !== "number" || value < 0) {
            return "Duration must be a non-negative number";
        }
        return "";
    }

    if (!value || !value.toString().trim()) {
        return "This field is required";
    }
    return "";
};

export const validateForm = (formData, excludedFields = []) => {
    const errors = {};
    Object.keys(formData).forEach((field) => {
        if (!excludedFields.includes(field)) {
            const error = validateField(field, formData[field]);
            if (error) {
                errors[field] = error;
            }
        }
    });
    return errors;
};

export const hasErrors = (errors) => {
    return Object.values(errors).some((error) => error !== "");
};

export const validateQuiz = (quiz) => {
    const newErrors = {
        question: '',
        options: [],
        answer: ''
    };

    if (!quiz.question.trim()) {
        newErrors.question = "Question is required";
    }

    const validOptions = quiz.options.filter(option => option.trim() !== '').length;
    if (validOptions < 2) {
        newErrors.options = "At least 2 non-empty options are required";
    }

    if (!quiz.answer.trim()) {
        newErrors.answer = "Correct answer is required";
    }

    return newErrors;
};