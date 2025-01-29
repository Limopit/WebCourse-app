import React, { useContext, useEffect, useState } from 'react';
import { fetchCourses } from "../../../Api/fetchCourses";
import "./BrainBrick.css";
import Header from "../../Elements/Header/Header";
import { useLocation } from 'react-router-dom';
import AdditionalContent from "../../../Api/getAdditionalContent";
import { AuthContext } from "../../../Context/AuthContext";

const BrainBrick = () => {
    const [courses, setCourses] = useState([]);
    const [filteredCourses, setFilteredCourses] = useState([]);
    const [loading, setLoading] = useState(true);
    const [search, setSearch] = useState("");
    const location = useLocation();
    const { isAuthenticated } = useContext(AuthContext);

    useEffect(() => {
        const loadItems = async () => {
            try {
                const data = await fetchCourses();
                setCourses(data);
                setFilteredCourses(data);
            } catch (error) {
                console.error("Data loading error: ", error);
                setCourses([]);
                setFilteredCourses([]);
            } finally {
                setLoading(false);
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

    return (
        <div>
            <Header additionalContent={<AdditionalContent location={location} isAuthenticated={isAuthenticated} />} />
            <div className="course-container">
                <input
                    type="text"
                    placeholder="Search courses..."
                    className="search-input"
                    value={search}
                    onChange={(e) => setSearch(e.target.value)}
                />
                <div className="courses-list">
                    {loading ? (
                        <p>Loading...</p>
                    ) : filteredCourses.length > 0 ? (
                        filteredCourses.map(item => (
                            <div key={item.id} className="course-item">
                                <img src={item.logo} alt="Image is missing" />
                                <div className="course-text">
                                    <h3>{item.title}</h3>
                                    <p>{item.description ? item.description : "Description is missing"}</p>
                                </div>
                            </div>
                        ))
                    ) : (
                        <p>No data</p>
                    )}
                </div>
            </div>
        </div>
    );
};

export default BrainBrick;
