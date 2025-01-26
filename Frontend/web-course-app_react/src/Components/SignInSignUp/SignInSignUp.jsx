import React, { useState } from "react";
import "./SignInSignUp.css";

import email_icon from "../Assets/email.png";
import password_icon from "../Assets/password.png";
import { login, signup } from "../../Api/auth";
import logo from "../Assets/user.png";

const SignInSignUp = () => {
    const [active, setActive] = useState("signin");
    const [previous, setPrevious] = useState(null);
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);

    const handleSwitch = (newState) => {
        if (newState !== active) {
            setPrevious(active);
            setActive(newState);
        }
    };

    const handleSubmit = async () => {
        setLoading(true);
        setError(null);
        try {
            let data;
            if (active === "signin") {
                data = await login(email, password);
            } else if (active === "signup") {
                data = await signup(email, password);
            }
            console.log("Success:", data);
        } catch (error) {
            setError(error.message || "Что-то пошло не так");
        } finally {
            setLoading(false);
        }
    };

    return (
        <div>
            <div className="header">
                <div className="logo-container">
                    <img src={logo} alt="Logo" className="logo" />
                </div>
            </div>
            <div className="main-container">
                <div className="main-container-header">
                    <div className="text">
                        <span className="fixed">Sign</span>
                        <div className="animated-wrapper">
                            {previous && (
                                <span
                                    className={`animated ${
                                        previous === "signin" ? "move-down" : "move-up"
                                    }`}
                                >
                                    {previous === "signin" ? "In" : "Up"}
                                </span>
                            )}
                            <span
                                className={`animated ${
                                    active === "signin" ? "appear-down" : "appear-up"
                                }`}
                            >
                                {active === "signin" ? "In" : "Up"}
                            </span>
                        </div>
                    </div>
                </div>
                <div className="inputs">
                    <div className="input">
                        <img src={email_icon} alt="Email Icon" />
                        <input
                            type="email"
                            placeholder="Email"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                        />
                    </div>
                    <div className="input">
                        <img src={password_icon} alt="Password Icon" />
                        <input
                            type="password"
                            placeholder="Password"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                        />
                    </div>
                </div>
                <div className="sign-modes-container">
                    <div
                        className={`sign-mode ${active === "signin" ? "active" : ""}`}
                        onClick={() => handleSwitch("signin")}
                    >
                        Sign In
                    </div>
                    <div
                        className={`sign-mode ${active === "signup" ? "active" : ""}`}
                        onClick={() => handleSwitch("signup")}
                    >
                        Sign Up
                    </div>
                    <div
                        className="slider"
                        style={{
                            transform: active === "signin" ? "translateX(0)" : "translateX(100%)",
                        }}
                    />
                </div>
                <div className="action-button-container">
                    <button
                        className="action-button"
                        onClick={handleSubmit}
                        disabled={loading}
                    >
                        {loading ? "Loading..." : active === "signin" ? "Sign In" : "Register"}
                    </button>
                </div>
                {error && <div className="error-message">{error}</div>}
            </div>
        </div>
    );
};

export default SignInSignUp;
