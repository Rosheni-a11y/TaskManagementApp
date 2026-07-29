const API_BASE_URL = 'http://localhost:5071/api';

export async function createUser(user: any) {
    const response = await fetch(`${API_BASE_URL}/users`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(user)
    });
    return response.json();
}

export async function getUsers() {
    const response = await fetch(`${API_BASE_URL}/users`);
    return response.json();
}

export async function getTasksByUserId(userId: number) {
    const response = await fetch(`${API_BASE_URL}/tasks/${userId}`);
    return response.json();
}

export async function createTask(task: any, token: string) {
    const response = await fetch(`${API_BASE_URL}/tasks`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify(task)
    });
    return response.json();
}



export async function updateTask(taskId: number, task: any, token: string) {
    const response = await fetch(`${API_BASE_URL}/tasks/${taskId}/full`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify(task)
    });
    return response;
}

export async function deleteTask(taskId: number, token: string) {
    const response = await fetch(`${API_BASE_URL}/tasks/${taskId}`, {
        method: 'DELETE',
        headers: {
            'Authorization': `Bearer ${token}`
        }
    });
    return response;
}

export async function login(email: string, password: string) {
    const response = await fetch(`${API_BASE_URL}/auth/login`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ email, password })
    });
    return response.json();
}

export async function resetPassword(email: string, newPassword: string) {
    const response = await fetch(`${API_BASE_URL}/auth/reset-password`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ email, newPassword })
    });
    return response.json();
}

export async function getAllTasks() {
    const token = localStorage.getItem('authToken');

    const response = await fetch(`${API_BASE_URL}/tasks`, {
        method: 'GET',
        headers: {
            Authorization: `Bearer ${token}`,
            'Content-Type': 'application/json'
        }
    });

    if (!response.ok) {
        throw new Error('Failed to retrieve tasks');
    }

    return await response.json();
}