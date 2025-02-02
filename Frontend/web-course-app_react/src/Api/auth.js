const API_BASE_URL = 'https://localhost:5003/gateway/auth';

export const login = async (email, password) => {
    try {
        const response = await fetch(`${API_BASE_URL}/login`, {
            method: 'POST',
            credentials: 'include',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ email, password }),
        });
        
        if (!response.ok) {
            const responseData = await response.json();
            throw new Error(responseData.error);
        }

        const { jwt, refreshToken } = await response.json();
        
        sessionStorage.setItem("accessToken", jwt);
        
        return true
    } catch (error) {
        throw error;
    }
};

export const signup = async (firstname, lastname, email, password) => {
    try {
        const response = await fetch(`${API_BASE_URL}/register`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({firstname, lastname, email, password, role: 'user' }),
        });

        if (!response.ok) {
            throw new Error('Failed to sign up');
        }

        return await response.json();
    } catch (error) {
        throw error;
    }
};

export const logout = async () => {
    try {
        const token = sessionStorage.getItem("accessToken");

        if (!token) {
            console.error("No access token found");
            throw new Error("No token found");
        }
        
        sessionStorage.clear();
        
        const response = await fetch(`${API_BASE_URL}/logout`, {
            method: 'POST',
            credentials: 'include',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
        });
        
        if (!response.ok) {
            throw new Error('Failed log out');
        }

        localStorage.removeItem("isAuthenticated");

        window.location.reload();
    } catch (error) {
        throw error;
    }
};
