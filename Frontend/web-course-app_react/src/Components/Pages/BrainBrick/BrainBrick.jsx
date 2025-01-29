import React, { useEffect, useState } from 'react';
import { fetchCourses } from "../../../Api/fetchCourses";
import "./BrainBrick.css";
import Header from "../../Elements/Header/Header";
import { useLocation } from 'react-router-dom';


const BrainBrick = () => {
    const [courses, setCourses] = useState([]);
    const [filteredCourses, setFilteredCourses] = useState([]);
    const [loading, setLoading] = useState(true);
    const [search, setSearch] = useState("");
    const location = useLocation();

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

    const getAdditionalContent = () => {
        if (location.pathname === '/auth') {
            return null;
        }
        return (
            <div className="signin-container">
                <a href="/auth" className="auth-link">
                    <button className="signin-button">Sign In / Sign Up</button>
                </a>
            </div>
        );
    };
    
    return (
        <div>
            <Header additionalContent={getAdditionalContent()}></Header>
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
                        <p>Загрузка...</p>
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
