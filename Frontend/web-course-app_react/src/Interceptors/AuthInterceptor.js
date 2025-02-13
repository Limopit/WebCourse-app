const getAuthToken = () => {
    return sessionStorage.getItem("accessToken");
};

const authInterceptor = (config) => {
    const token = getAuthToken();
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
};

export { authInterceptor };