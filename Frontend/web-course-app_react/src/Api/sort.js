export const Sort = (courses, setFilteredCourses, criteria, order) => {
    const sortedCourses = [...courses].sort((a, b) => {
        let valueA = criteria === "title" ? a.title.toLowerCase() : new Date(a.date);
        let valueB = criteria === "title" ? b.title.toLowerCase() : new Date(b.date);

        let comparison = valueA > valueB ? 1 : valueA < valueB ? -1 : 0;
        return order === "desc" ? -comparison : comparison;
    });

    setFilteredCourses(sortedCourses);
};
