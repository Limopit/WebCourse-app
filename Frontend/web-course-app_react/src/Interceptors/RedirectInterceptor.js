import axios from 'axios';
import Cookies from 'js-cookie';
import { refreshToken } from '../Api/auth';

let isRefreshing = false;

const redirectInterceptor = (error) => {
    const originalRequest = error.config;

    if (error.response && error.response.status === 401 && !originalRequest._retry) {
        if (isRefreshing) {
            return Promise.reject(error); 
        }

        originalRequest._retry = true;
        isRefreshing = true;

        return refreshToken()
            .then(accessToken => {
                isRefreshing = false;

                localStorage.setItem('accessToken', accessToken);
                originalRequest.headers['Authorization'] = `Bearer ${accessToken}`;

                return axios(originalRequest);
            })
            .catch(refreshError => {
                isRefreshing = false;

                console.log("Ошибка обновления токена: ", refreshError);

                localStorage.removeItem('accessToken');
                Cookies.remove('RefreshToken');
                localStorage.setItem('isAuthenticated', 'false');

                return Promise.reject(refreshError);
            });
    }

    return Promise.reject(error);
};

export { redirectInterceptor };
