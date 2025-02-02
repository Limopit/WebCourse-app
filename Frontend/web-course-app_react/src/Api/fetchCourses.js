export const fetchCourses = async () => {
    try {
        const response = await fetch("https://localhost:5003/gateway/courses/approved");
        if (!response.ok) {
            throw new Error(`Error while loading data: ${response.status}`);
        }

        const data = await response.json();
        return data.courses;
    } catch (error) {
        console.error("Loading error: ", error);
        return [];
    }
};
