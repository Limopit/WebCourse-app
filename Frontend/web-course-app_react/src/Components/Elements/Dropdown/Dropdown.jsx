import React, { useState, useRef, useEffect } from "react";
import "./Dropdown.css"

const Dropdown = ({ trigger, children, onToggle }) => {
    const [isOpen, setIsOpen] = useState(false);
    const dropdownRef = useRef(null);

    useEffect(() => {
        const handleClickOutside = (event) => {
            if (dropdownRef.current && !dropdownRef.current.contains(event.target)) {
                setIsOpen(false);
                if (onToggle) onToggle(false);
            }
        };

        if (isOpen) {
            document.addEventListener("mousedown", handleClickOutside);
        }

        return () => {
            document.removeEventListener("mousedown", handleClickOutside);
        };
    }, [isOpen, onToggle]);

    const handleTriggerClick = () => {
        setIsOpen((prev) => !prev);
        if (onToggle) onToggle(!isOpen);
    };

    return (
        <div className="dropdown-container" ref={dropdownRef}>
            <div onClick={handleTriggerClick}>{trigger}</div>
            <div className={`dropdown-menu ${isOpen ? "open" : ""}`}>
                {children}
            </div>
        </div>
    );
};

export default Dropdown;