import React, { useContext, useEffect, useState } from 'react';
import { fetchCourses } from "../../../Api/fetchCourses";
import "./BrainBrick.css";
import Header from "../../Elements/Header/Header";
import { useLocation } from 'react-router-dom';
import AdditionalContent from "../../../Api/getAdditionalContent";
import { Sort } from "../../../Api/sort";
import { AuthContext } from "../../../Context/AuthContext";
import Dropdown from "../../Elements/Dropdown/Dropdown";
import CreateCourseButton from "../../../Api/getCreateCourseButton";
import CourseDetails from "../../Popups/CourseDetails";
import {fetchCourseDetails} from "../../../Api/fetchCourses";

const BrainBrick = () => {
    const [courses, setCourses] = useState([]);
    const [filteredCourses, setFilteredCourses] = useState([]);
    const [initialCourseOrder, setInitialCourseOrder] = useState([]);
    const [selectedCourse, setSelectedCourse] = useState(null);
    
    const [loading, setLoading] = useState(true);
    
    const [search, setSearch] = useState("");
    const [sortText, setSortText] = useState("Sort");
    
    const [isDropdownOpen, setIsDropdownOpen] = useState(false);

    const location = useLocation();
    
    const { isAuthenticated } = useContext(AuthContext);

    useEffect(() => {
        const loadItems = async () => {
            try {
                const data = await fetchCourses();
                setCourses(data);
                setFilteredCourses(data);
                setInitialCourseOrder(data);
            } catch (error) {
                console.error("Data loading error: ", error);
                setCourses([]);
                setFilteredCourses([]);
                setInitialCourseOrder([]);
            } finally {
                setTimeout(() => {
                    setLoading(false);
                }, 2000);
            }
        };

        loadItems();
    }, []);

    useEffect(() => {
        const filtered = courses.filter(course =>
            course.title.toLowerCase().includes(search.toLowerCase())
        );
        setFilteredCourses(filtered);
    }, [search, courses]);

    const resetSort = () => {
        setFilteredCourses([...initialCourseOrder]);
        setSearch("");
        setSortText("Sort");
    };

    const handleCourseClick = async (course) => {
        try {
            const courseDetails = await fetchCourseDetails(course.id);
            setSelectedCourse(courseDetails);
        } catch (error) {
            console.error("Error getting course data:", error);
        }
    };

    const handleCloseDetails = () => {
        setSelectedCourse(null);
    };

    return (
        <div>
            <Header
                createCourseButton={<CreateCourseButton isAuthenticated={isAuthenticated} />}
                additionalContent={<AdditionalContent location={location} isAuthenticated={isAuthenticated} />}
            />

            <div className="course-container">
                <div className="sort-and-filter-container">
                    <button className="reset-list-button" onClick={resetSort}>Reset</button>
                    <input
                        type="text"
                        placeholder="Search courses..."
                        className="search-input"
                        value={search}
                        onChange={(e) => setSearch(e.target.value)}
                    />
                    <Dropdown
                        trigger={
                            <button className={`sort-options-button ${isDropdownOpen ? "active" : ""}`}>
                                {sortText}
                            </button>
                        }
                        onToggle={(isOpen) => setIsDropdownOpen(isOpen)}
                    >
                        <button
                            className="dropdown-item"
                            onClick={() => {
                                Sort(filteredCourses, setFilteredCourses, 'title', 'asc');
                                setSortText("A - Z");
                            }}
                        >
                            A - Z
                        </button>
                        <button
                            className="dropdown-item"
                            onClick={() => {
                                Sort(filteredCourses, setFilteredCourses, 'title', 'desc');
                                setSortText("Z - A");
                            }}
                        >
                            Z - A
                        </button>
                    </Dropdown>
                </div>

                <div className={`course-list-container ${loading ? "loading" : ""}`}>
                    {loading ? (
                        <p>Please, wait...</p>
                    ) : filteredCourses.length > 0 ? (
                        filteredCourses.map(item => (
                            <div key={item.id} className="course-item" onClick={() => handleCourseClick(item)}>
                                <img src={item.logo} alt="Image is missing"/>
                                <div className="course-text">
                                    <h3>{item.title}</h3>
                                    <p>
                                        {item.description.length > 50
                                            ? item.description.substring(0, 50) + "..."
                                            : item.description || "Description is missing"}
                                    </p>
                                </div>

                            </div>
                        ))
                    ) : (
                        <p>No data</p>
                    )}
                </div>
            </div>

            {selectedCourse && (
                <CourseDetails course={selectedCourse} onClose={handleCloseDetails} />
            )}
        </div>
    );
};

export default BrainBrick;