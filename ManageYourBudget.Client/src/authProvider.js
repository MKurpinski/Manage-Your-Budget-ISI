import jwt_decode from 'jwt-decode'

const TOKEN_KEY = 'manage_your_budget_token';

const saveToken = token => {
    localStorage.setItem(TOKEN_KEY, token);
};

const isAuthenticated = () => {
    const token = getToken();
    if(token){
        try{
            const decodedToken = jwt_decode(token);
            return decodedToken && decodedToken.exp && (decodedToken.exp * 1000) > Date.now();
        }
        catch(err) {
            return false;
        }
    }
    return false;
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