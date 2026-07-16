const API_BASE_URL = 'https://localhost:7158/api';

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

export async function updateTaskStatus(taskId: number, newStatus: string, token: string) {
    const response = await fetch(`${API_BASE_URL}/tasks/${taskId}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify(newStatus)
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