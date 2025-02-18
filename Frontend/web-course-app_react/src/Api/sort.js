export const Sort = (courses, setFilteredCourses, criteria, order) => {
    const sortedCourses = courses.toSorted((a, b) => {
        const valueA = criteria === "title" ? a.title.toLowerCase() : new Date(a.date);
        const valueB = criteria === "title" ? b.title.toLowerCase() : new Date(b.date);

        const comparison = valueA > valueB ? 1 : valueA < valueB ? -1 : 0;
        return order === "desc" ? -comparison : comparison;
    });

    setFilteredCourses(sortedCourses);
};
