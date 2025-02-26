import api from "../Interceptors/InterceptorSetup";

export const approveUserCourse = async (id) => {
    const response = await api.put(`user/courses/created/${id}?status=1`);
    return response.data;
};