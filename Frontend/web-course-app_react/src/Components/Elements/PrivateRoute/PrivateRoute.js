import React from "react";
import { Navigate, Outlet, useLocation } from "react-router-dom";

const PrivateRoute = () => {
    const isAuthenticated = localStorage.getItem("isAuthenticated") === "true";
    const location = useLocation();

    if (!isAuthenticated) {
        return <Navigate to="/auth" state={{ from: location }} replace />;
    }

    return <Outlet />;
};

export default PrivateRoute;