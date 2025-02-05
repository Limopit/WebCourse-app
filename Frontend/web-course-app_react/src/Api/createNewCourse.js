import {useNavigate} from "react-router-dom";

export const submitCourseForm = async (formData) => {
    const navigate = useNavigate();
    
    const response = await fetch("https://localhost:5003/gateway/courses", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${sessionStorage.getItem("accessToken")}`
        },
        body: JSON.stringify(formData)
    });

    if (!response.ok) {
        throw new Error(`Error: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    navigate("/")
    return data;
}
