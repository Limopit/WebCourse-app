const redirectInterceptor = (error) => {
    if (error.response && error.response.status === 401) {
        sessionStorage.removeItem("accessToken");
        localStorage.setItem("isAuthenticated", "false");
        window.location.href = "/auth";
    }
    return Promise.reject(error);
};

export { redirectInterceptor };