import React, { useContext, useEffect, useState } from 'react';
import { fetchCourses, fetchPendingCourses, fetchTakenCourses } from "../../../Api/fetchCourses";
import "./BrainBrick.css";
import Header from "../../Elements/Header/Header";
import { useLocation } from 'react-router-dom';
import AdditionalContent from "../../../Api/getAdditionalContent";
import { Sort } from "../../../Api/sort";
import { AuthContext } from "../../../Context/AuthContext";
import Dropdown from "../../Elements/Dropdown/Dropdown";
import CreateCourseButton from "../../../Api/getCreateCourseButton";
import CourseDetails from "../../Popups/CourseDetails";
import { fetchCourseDetails } from "../../../Api/fetchCourses";

const BrainBrick = () => {
    const [courses, setCourses] = useState([]);
    const [takenCourses, setTakenCourses] = useState([]);
    const [unapprovedCourses, setUnapprovedCourses] = useState([]);
    const [filteredCourses, setFilteredCourses] = useState([]);
    const [selectedCourse, setSelectedCourse] = useState(null);
    const [currentItems, setCurrentItems] = useState([]);

    const [loading, setLoading] = useState(true);

    const [search, setSearch] = useState("");
    const [sortText, setSortText] = useState("Sort");
    const [statusFilter, setStatusFilter] = useState("All");

    const [isSortDropdownOpen, setIsSortDropdownOpen] = useState(false);
    const [isFilterDropdownOpen, setIsFilterDropdownOpen] = useState(false);

    const [showUnapproved, setShowUnapproved] = useState(false);

    const [currentPageApproved, setCurrentPageApproved] = useState(1);
    const [isLastPageApproved, setIsLastPageApproved] = useState(false);

    const [currentPageUnapproved, setCurrentPageUnapproved] = useState(1);
    const [isLastPageUnapproved, setIsLastPageUnapproved] = useState(false);

    const itemsPerPage = 10;

    const location = useLocation();
    const { isAuthenticated } = useContext(AuthContext);
    const userRole = sessionStorage.getItem("role");

    useEffect(() => {
        const loadData = async () => {
            setLoading(true);
            try {
                if (showUnapproved) {
                    if (isAuthenticated && userRole === "Admin") {
                        const unapprovedData = await fetchPendingCourses(currentPageUnapproved, itemsPerPage);
                        console.log("Unapproved Data:", unapprovedData);
                        const coursesArray = unapprovedData.courses || unapprovedData;
                        setUnapprovedCourses(coursesArray);
                        setIsLastPageUnapproved(coursesArray.length < itemsPerPage);
                    }
                } else {
                    const approvedData = await fetchCourses(currentPageApproved, itemsPerPage);
                    console.log("Approved Data:", approvedData);
                    const coursesArray = approvedData.courses || approvedData;
                    setCourses(coursesArray);
                    setIsLastPageApproved(coursesArray.length < itemsPerPage);
                }

                if (isAuthenticated) {
                    const takenCoursesData = await fetchTakenCourses();
                    console.log("Taken Courses Data:", takenCoursesData);
                    const takenCoursesArray = takenCoursesData.courses || takenCoursesData;
                    setTakenCourses(takenCoursesArray);
                }
            } catch (error) {
                console.error("Data loading error: ", error);
                setCourses([]);
                setUnapprovedCourses([]);
                setTakenCourses([]);
                setIsLastPageApproved(true);
                setIsLastPageUnapproved(true);
            } finally {
                setLoading(false);
            }
        };

        loadData();
    }, [currentPageApproved, currentPageUnapproved, showUnapproved, isAuthenticated, userRole]);

    useEffect(() => {
        const filtered = (showUnapproved ? unapprovedCourses : courses).filter(course => {
            const matchesSearch = course.title.toLowerCase().includes(search.toLowerCase());
            const isTaken = takenCourses.some(takenCourse => takenCourse.id === course.id);
            const matchesStatus =
                statusFilter === "All" ||
                (statusFilter === "Taken" && isTaken) ||
                (statusFilter === "Not Taken" && !isTaken);
            return matchesSearch && matchesStatus;
        });

        setFilteredCourses(filtered);
    }, [search, courses, unapprovedCourses, statusFilter, takenCourses, showUnapproved]);

    useEffect(() => {
        setCurrentItems(filteredCourses);
    }, [filteredCourses]);

    const resetSort = () => {
        setSearch("");
        setSortText("Sort");
        setStatusFilter("All");
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

    const handleNextPage = async () => {
        if (showUnapproved && !isLastPageUnapproved) {
            setCurrentPageUnapproved(prev => prev + 1);
        } else if (!showUnapproved && !isLastPageApproved) {
            setCurrentPageApproved(prev => prev + 1);
        }
    };

    const handlePreviousPage = () => {
        if (showUnapproved && currentPageUnapproved > 1) {
            setCurrentPageUnapproved(prev => prev - 1);
        } else if (!showUnapproved && currentPageApproved > 1) {
            setCurrentPageApproved(prev => prev - 1);
        }
    };

    const currentPage = showUnapproved ? currentPageUnapproved : currentPageApproved;

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
                            <button className={`sort-options-button ${isSortDropdownOpen ? "active" : ""}`}>
                                {sortText}
                            </button>
                        }
                        onToggle={(isOpen) => setIsSortDropdownOpen(isOpen)}
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
                    {isAuthenticated && (
                        <Dropdown
                            trigger={
                                <button className={`sort-options-button ${isFilterDropdownOpen ? "active" : ""}`}>
                                    {statusFilter}
                                </button>
                            }
                            onToggle={(isOpen) => setIsFilterDropdownOpen(isOpen)}
                        >
                            <button
                                className="dropdown-item"
                                onClick={() => setStatusFilter("All")}
                            >
                                All
                            </button>
                            <button
                                className="dropdown-item"
                                onClick={() => setStatusFilter("Taken")}
                            >
                                Taken
                            </button>
                            <button
                                className="dropdown-item"
                                onClick={() => setStatusFilter("Not Taken")}
                            >
                                Not Taken
                            </button>
                        </Dropdown>
                    )}
                    {isAuthenticated && userRole === "Admin" && (
                        <button
                            className={`mode-toggle-button ${showUnapproved ? "unapproved" : "approved"}`}
                            onClick={() => setShowUnapproved(!showUnapproved)}
                        >
                            {showUnapproved ? "Show Approved" : "Show Unapproved"}
                        </button>
                    )}
                </div>

                <div className={`course-list-container ${loading ? "loading" : ""}`}>
                    {loading ? (
                        <p>Please, wait...</p>
                    ) : currentItems.length > 0 ? (
                        currentItems.map(item => (
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

                <div className="pagination">
                    <button
                        onClick={handlePreviousPage}
                        disabled={currentPageApproved === 1 && currentPageUnapproved === 1}
                    >
                        Previous
                    </button>
                    <span className="page-number">Page {currentPage}</span>
                    <button
                        onClick={handleNextPage}
                        disabled={showUnapproved ? isLastPageUnapproved : isLastPageApproved}
                    >
                        Next
                    </button>
                </div>
            </div>

            {selectedCourse && (
                <CourseDetails
                    course={selectedCourse}
                    onClose={handleCloseDetails}
                    takenCourses={takenCourses}
                    showUnapproved={showUnapproved}
                />
            )}
        </div>
    );
};

export default BrainBrick;