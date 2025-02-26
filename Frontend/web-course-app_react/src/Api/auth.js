import api from "../Interceptors/InterceptorSetup";
import {jwtDecode} from "jwt-decode";

const API_BASE_URL = '/auth';

export const login = async (email, password) => {
    const response = await api.post(`${API_BASE_URL}/login`, { email, password });
    const { jwt } = response.data;
    sessionStorage.setItem("accessToken", jwt);
    
    const payload = jwtDecode(jwt)
    sessionStorage.setItem("role", payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]);
    return true;
};

export const signup = async (firstname, lastname, email, password) => {
    const response = await api.post(`${API_BASE_URL}/register`, {
        firstname,
        lastname,
        email,
        password,
        role: 'user',
    });

    return response.data;
};

export const logout = async () => {
    const token = sessionStorage.getItem("accessToken");
    if (!token) {
        console.error("No access token found");
        throw new Error("No token found");
    }

    await api.post(`${API_BASE_URL}/logout`, {}, {});

    sessionStorage.clear();
    localStorage.setItem("isAuthenticated", "false");

    window.location.reload();
};