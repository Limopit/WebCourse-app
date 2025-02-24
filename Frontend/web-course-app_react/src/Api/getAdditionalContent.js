import React, { useState, useRef, useEffect } from "react";
import user_icon from "../Components/Assets/user.png";
import { jwtDecode } from "jwt-decode";
import { logout } from "./auth";
import Dropdown from "../Components/Elements/Dropdown/Dropdown";
import Notification from "../Components/Elements/Notification/Notification";

const AdditionalContent = ({ location, isAuthenticated }) => {
    const [menuOpen, setMenuOpen] = useState(false);
    const [notificationOpen, setNotificationOpen] = useState(false);
    const menuRef = useRef(null);
    const [email, setEmail] = useState("");

    useEffect(() => {
        const handleClickOutside = (event) => {
            if (menuRef.current && !menuRef.current.contains(event.target)) {
                setMenuOpen(false);
                setNotificationOpen(false);
            }
        };

        document.addEventListener("mousedown", handleClickOutside);

        return () => {
            document.removeEventListener("mousedown", handleClickOutside);
        };
    }, []);

    useEffect(() => {
        const token = sessionStorage.getItem("accessToken");
        if (token) {
            const decodedToken = jwtDecode(token);
            setEmail(decodedToken.nameid);
            console.log(decodedToken.nameid);
        }
    }, []);

    if (location.pathname === "/auth") {
        return null;
    }

    return (
        <div className="additional-content">
            <div className="signin-container">
                {isAuthenticated ? (
                    <div className="profile-menu-container" ref={menuRef}>
                        <div className="notification-button-container">
                            <button
                                className="notification-button"
                                onClick={() => setNotificationOpen(!notificationOpen)}
                            >
                                🔔
                            </button>
                            {notificationOpen && (
                                <div>
                                    <Notification userEmail={email} />
                                </div>
                            )}
                        </div>
                        <Dropdown
                            trigger={
                                <button className="profile-button" onClick={() => setMenuOpen(!menuOpen)}>
                                    <img src={user_icon} alt="Profile" />
                                </button>
                            }
                        >
                            <button className="dropdown-item">Profile</button>
                            <button className="dropdown-item" onClick={logout}>Logout</button>
                        </Dropdown>
                    </div>
                ) : (
                    <a href="/auth" className="auth-link">
                        <button className="signin-button">Sign In / Sign Up</button>
                    </a>
                )}
            </div>
        </div>
    );
};

export default AdditionalContent;