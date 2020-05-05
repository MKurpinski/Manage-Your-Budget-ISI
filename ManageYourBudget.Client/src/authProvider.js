const TOKEN_KEY = 'manage_your_budget_token';

const saveToken = token => {
    localStorage.setItem(TOKEN_KEY, token);
};

const isAuthenticated = () => {
    return !!localStorage.getItem(TOKEN_KEY);
};

const getToken = () => {
    return localStorage.getItem(TOKEN_KEY);
};

const removeToken = () => {
    return localStorage.removeItem(TOKEN_KEY);
};

export default {
    saveToken,
    getToken,
    isAuthenticated,
    removeToken
}