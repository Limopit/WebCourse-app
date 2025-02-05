import React from 'react';
import logo from "../../Assets/brain-brick.png";
import logo_text from "../../Assets/brain-brick-text.png";

const Header = ({ createCourseButton, additionalContent }) => {
    return (
        <div className="header">
            <div className="left-container">
                <a href="/" style={{display: 'inline-flex', alignItems: 'center', textDecoration: 'none'}}>
                    <div className="logo-container">
                        <img src={logo} alt="Logo" className="logo"/>
                        <img src={logo_text} alt="Logo text" className="logo-text"/>
                    </div>
                </a>
                {createCourseButton && <div className="create-course-container">{createCourseButton}</div>}
            </div>
            {additionalContent && <div className="additional-content">{additionalContent}</div>}
        </div>

    );
};

export default Header;