import React, { useState, useRef, useEffect } from "react";
import user_icon from "../Components/Assets/user.png";
import {logout} from "./auth";

const AdditionalContent = ({ location, isAuthenticated }) => {
    const [menuOpen, setMenuOpen] = useState(false);
    const menuRef = useRef(null);

    useEffect(() => {
        const handleClickOutside = (event) => {
            if (menuRef.current && !menuRef.current.contains(event.target)) {
                setMenuOpen(false);
            }
        };

        if (menuOpen) {
            document.addEventListener("mousedown", handleClickOutside);
        }

        return () => {
            document.removeEventListener("mousedown", handleClickOutside);
        };
    }, [menuOpen]);

    if (location.pathname === "/auth") {
        return null;
    }

    return (
        <div className="signin-container">
            {isAuthenticated ? (
                <div className="profile-menu-container" ref={menuRef}>
                    <button className="image-button" onClick={() => setMenuOpen(!menuOpen)}>
                        <img src={user_icon} alt="Profile" />
                    </button>
                    {menuOpen && (
                        <div className="dropdown-menu">
                            <button className="dropdown-item">Profile</button>
                            <button className="dropdown-item" onClick={logout}>Logout</button>
                        </div>
                    )}
                </div>
            ) : (
                <a href="/auth" className="auth-link">
                    <button className="signin-button">Sign In / Sign Up</button>
                </a>
            )}
        </div>
    );
};

export default AdditionalContent;
