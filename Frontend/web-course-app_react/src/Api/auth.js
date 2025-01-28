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
            var responseData = await response.json();
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
