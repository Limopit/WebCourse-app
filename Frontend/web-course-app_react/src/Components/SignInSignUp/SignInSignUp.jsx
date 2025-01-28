import React, { useState } from "react";
import "./SignInSignUp.css";

import email_icon from "../Assets/email.png";
import password_icon from "../Assets/password.png";
import user_icon from "../Assets/user.png";
import { login, signup } from "../../Api/auth";
import logo from "../Assets/brain-brick.png";
import logo_text from "../Assets/brain-brick-text.png";

const SignInSignUp = () => {
    const [active, setActive] = useState("signin");
    const [previous, setPrevious] = useState(null);
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [firstname, setFirstname] = useState("");
    const [lastname, setLastname] = useState("");
    const [error, setError] = useState(null);

    const handleSwitch = (newState) => {
        if (newState !== active) {
            setPrevious(active);
            setActive(newState);
        }
    };

    const handleSubmit = async () => {
        setError(null);
        try {
            if (active === "signin") {
                await login(email, password);
            } else if (active === "signup") {
                await signup(firstname, lastname, email, password);
            }
        } catch (error) {
            setError(error.message);
        }
    };

    return (
        <div>
            <div className="header">
                <a href="/" style={{ display: 'inline-flex', alignItems: 'center', textDecoration: 'none' }}>
                <div className="logo-container">
                    <img src={logo} alt="Logo" className="logo" />
                    <img src={logo_text} alt="Logo text" className="logo-text" />
                </div>
                </a>
            </div>
            <div className="main-container" style={{height: previous === "signin" ? "75vh" : "55vh"}}>
                <div className="main-container-header">
                    <div className="text">
                        <span className="fixed">Sign</span>
                        <div className="animated-wrapper">
                            {previous && (
                                <span className={`animated ${previous === "signin" ? "move-down" : "move-up"}`}>
                                    {previous === "signin" ? "In" : "Up"}
                                </span>
                            )}
                            <span className={`animated ${active === "signin" ? "appear-down" : "appear-up"}`}>
                                {active === "signin" ? "In" : "Up"}
                            </span>
                        </div>
                    </div>
                </div>
                <div className="inputs">
                    <div className="input">
                        <img src={email_icon} alt="Email Icon"/>
                        <input
                            type="email"
                            placeholder="Email"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                        />
                    </div>
                    <div className="input">
                        <img src={password_icon} alt="Password Icon"/>
                        <input
                            type="password"
                            placeholder="Password"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                        />
                    </div>
                    {active === "signup" && (
                        <>
                            <div className="signup-input">
                                <img src={user_icon} alt="Password Icon"/>
                                <input
                                    type="text"
                                    placeholder="First Name"
                                    value={firstname}
                                    onChange={(e) => setFirstname(e.target.value)}
                                />
                            </div>
                            <div className="signup-input">
                                <img src={user_icon} alt="Password Icon"/>
                                <input
                                    type="text"
                                    placeholder="Last Name"
                                    value={lastname}
                                    onChange={(e) => setLastname(e.target.value)}
                                />
                            </div>
                        </>
                    )}
                </div>
                <div className="sign-modes-container">
                    <div className={`sign-mode ${active === "signin" ? "active" : ""}`}
                         onClick={() => handleSwitch("signin")}>
                        Sign In
                    </div>
                    <div className={`sign-mode ${active === "signup" ? "active" : ""}`}
                        onClick={() => handleSwitch("signup")}>
                        Sign Up
                    </div>
                    <div className="slider" style={{transform: active === "signin" ? "translateX(0)" : "translateX(100%)"}}/>
                </div>
                <div className="action-button-container">
                    <button className="action-button" onClick={handleSubmit}>
                        {active === "signin" ? "Sign In" : "Register"}
                    </button>
                </div>
                {error && <div className="error-message">{error}</div>}
            </div>
        </div>
    );
};

export default SignInSignUp;
