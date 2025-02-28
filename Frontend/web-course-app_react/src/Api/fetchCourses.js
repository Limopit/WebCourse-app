import api from "../Interceptors/InterceptorSetup";

export const fetchCourses = async (pageNumber = 1, pageSize = 10) => {
    try {
        const response = await api.get("/courses/approved", {
            params: {
                pageNumber,
                pageSize
            }
        });
        return response.data.courses;
    } catch (error) {
        console.error("Loading error: ", error);
        return [];
    }
};

export const fetchTakenCourses = async () => {
    try {
        const response = await api.get("user/courses/taken");
        return response.data.userTakenCourses;
    } catch (error) {
        console.error("Loading error: ", error);
        return [];
    }
};

export const fetchCourseDetails = async (id) => {
    try {
        const response = await api.get(`/courses/${id}`);
        return response.data;
    } catch (error) {
        console.error("Loading error: ", error);
        return null;
    }
};

export const fetchPendingCourses = async (pageNumber = 1, pageSize = 10) => {
    try {
        const response = await api.get("/courses/pending", {
            params: {
                pageNumber,
                pageSize
            }
        });
        return response.data.courses;
    } catch (error) {
        console.error("Loading error: ", error);
        return [];
    }
};