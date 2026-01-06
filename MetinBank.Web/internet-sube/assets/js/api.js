const API_BASE_URL = 'http://localhost:5000/api';

// Get auth token
function getToken() {
    return localStorage.getItem('authToken');
}

// Get user info
function getUser() {
    const userStr = localStorage.getItem('kullanici');
    return userStr ? JSON.parse(userStr) : null;
}

// Set auth data
function setAuthData(token, user) {
    localStorage.setItem('authToken', token);
    localStorage.setItem('kullanici', JSON.stringify(user));
}

// Clear auth data
function clearAuthData() {
    localStorage.removeItem('authToken');
    localStorage.removeItem('kullanici');
}

// Check if authenticated
function isAuthenticated() {
    return getToken() !== null;
}

// API GET request
async function apiGet(endpoint) {
    const token = getToken();
    const response = await fetch(`${API_BASE_URL}${endpoint}`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            ...(token && { 'Authorization': `Bearer ${token}` })
        }
    });

    if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
    }

    return await response.json();
}

// API POST request
async function apiPost(endpoint, data = {}) {
    const token = getToken();
    const response = await fetch(`${API_BASE_URL}${endpoint}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            ...(token && { 'Authorization': `Bearer ${token}` })
        },
        body: JSON.stringify(data)
    });

    if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
    }

    return await response.json();
}

// API PUT request
async function apiPut(endpoint, data = {}) {
    const token = getToken();
    const response = await fetch(`${API_BASE_URL}${endpoint}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
            ...(token && { 'Authorization': `Bearer ${token}` })
        },
        body: JSON.stringify(data)
    });

    if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
    }

    return await response.json();
}

// API DELETE request
async function apiDelete(endpoint) {
    const token = getToken();
    const response = await fetch(`${API_BASE_URL}${endpoint}`, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
            ...(token && { 'Authorization': `Bearer ${token}` })
        }
    });

    if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
    }

    return await response.json();
}
