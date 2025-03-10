import React, {useContext, useState} from "react";
import "./SignInSignUp.css";

import email_icon from "../../Assets/email.png";
import password_icon from "../../Assets/password.png";
import user_icon from "../../Assets/user.png";
import { login, signup } from "../../../Api/auth";
import Header from "../../Elements/Header/Header";
import {AuthContext} from "../../../Context/AuthContext";
import {useLocation, useNavigate} from "react-router-dom";

const SignInSignUp = () => {
    const [active, setActive] = useState("signIn");
    const [previous, setPrevious] = useState(null);
    
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [firstname, setFirstname] = useState("");
    const [lastname, setLastname] = useState("");
    
    const { login: authLogin } = useContext(AuthContext);
    
    const [error, setError] = useState(null);
    
    const navigate = useNavigate();
    const location = useLocation();

    const handleSwitch = (newState) => {
        if (newState !== active) {
            setPrevious(active);
            setActive(newState);
        }
    };

    const handleSubmit = async () => {
        setError(null);
        try {
            if (active === "signIn") {
                await login(email, password);
                authLogin();
                const from = location.state?.from || "/";
                navigate(from, { replace: true });
            } else if (active === "signUp") {
                await signup(firstname, lastname, email, password);
            }
        } catch (error) {
            setError(error.message);
        }
    };

    return (
        <div>
            <Header />
            
            <div className="main-container" style={{height: previous === "signIn" ? "75vh" : "55vh"}}>
                <div className="main-container-header">
                    <div className="text">
                        <span className="fixed">Sign</span>
                        <div className="animated-wrapper">
                            {previous && (
                                <span className={`animated ${previous === "signIn" ? "move-down" : "move-up"}`}>
                                    {previous === "signin" ? "In" : "Up"}
                                </span>
                            )}
                            <span className={`animated ${active === "signIn" ? "appear-down" : "appear-up"}`}>
                                {active === "signIn" ? "In" : "Up"}
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
                    {active === "signUp" && (
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
                    <div className={`sign-mode ${active === "signIn" ? "active" : ""}`}
                         onClick={() => handleSwitch("signIn")}>
                        Sign In
                    </div>
                    <div className={`sign-mode ${active === "signUp" ? "active" : ""}`}
                        onClick={() => handleSwitch("signUp")}>
                        Sign Up
                    </div>
                    <div className="slider" style={{transform: active === "signIn" ? "translateX(0)" : "translateX(100%)"}}/>
                </div>
                <div className="action-button-container">
                    <button className="action-button" onClick={handleSubmit}>
                        {active === "signIn" ? "Sign In" : "Sign Up"}
                    </button>
                </div>
                {error && <div className="error-message">{error}</div>}
            </div>
        </div>
    );
};

export default SignInSignUp;