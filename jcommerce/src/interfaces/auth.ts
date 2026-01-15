export interface User {
    id: string;
    name: string;
    email: string;
    role: 'User' | 'Admin';
    createdAt: string;
}

export interface LoginCredentials {
    email: string;
    password: string;
}

export interface RegisterCredentials {
    name: string;
    email: string;
    password: string;
    role: 'User';
}

export interface AuthResponse {
    user: User;
    token: string;
    expiresAt: string;
}

export interface AuthState {
    user: User | null;
    token: string | null;
    isAuthenticated: boolean;
    isLoading: boolean;
    error: string | null;
}