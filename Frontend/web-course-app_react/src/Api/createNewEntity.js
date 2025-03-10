import api from "../Interceptors/InterceptorSetup";
import { jwtDecode } from "jwt-decode";

export const createCourseEntity = async (formData) => {
    const response = await api.post("/courses", formData);
    return response.data;
};

export const createLessonEntity = async (formData) => {
    const response = await api.post("/lessons", formData);
    return response.data;
};

export const createQuizEntity = async (formData) => {
    const response = await api.post("/quizzes", formData);
    return response.data;
};

export const createTakenCourseRecord = async (courseId) => {
    const token = sessionStorage.getItem("accessToken");
    if (!token) {
        window.location.href = "/auth";
        return;
    }
    const payload = jwtDecode(token);
    
    const response = await api.post("/user/courses/taken", {
        email: payload.nameid,
        courseId: courseId,
        startDate: "2025-02-11T22:10:53.263Z",
    });
    return response.data;
};