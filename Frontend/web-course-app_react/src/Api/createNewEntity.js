import {jwtDecode} from "jwt-decode";

const API_BASE_URL = 'https://localhost:5003/gateway';
export const createCourseEntity = async (formData) => {
        const response = await fetch(`${API_BASE_URL}/courses`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${sessionStorage.getItem("accessToken")}`
            },
            body: JSON.stringify(formData)
        });

        if (!response.ok) {
            throw new Error(`Error: ${response.status} ${response.statusText}`);
        }

        const data = await response.json();
        return data;
}

export const createLessonEntity = async (formData) => {
    const response = await fetch(`${API_BASE_URL}/lessons`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${sessionStorage.getItem("accessToken")}`
        },
        body: JSON.stringify(formData)
    });

    if (!response.ok) {
        throw new Error(`Error: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data;
}

export const createQuizEntity = async (formData) => {
    const response = await fetch(`${API_BASE_URL}/quizzes`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${sessionStorage.getItem("accessToken")}`
        },
        body: JSON.stringify(formData)
    });

    if (!response.ok) {
        throw new Error(`Error: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data;
}

export const createTakenCourseRecord = async (courseId) => {
    const token = sessionStorage.getItem("accessToken");
    const payload = jwtDecode(token);
    const response = await fetch(`${API_BASE_URL}/user/courses/taken`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token}`
        },
        body: JSON.stringify({
            email: payload.sub,
            courseId: courseId,
            startDate: "2025-02-11T22:10:53.263Z"
        })
    });

    if (!response.ok) {
        throw new Error(`Error: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data;
};

