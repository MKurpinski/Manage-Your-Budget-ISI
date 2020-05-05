import baseApi from './baseApi'

export default {
    login: (loginData) => baseApi.post('auth/login', loginData),
    register: (registerData) => baseApi.post(`auth`, registerData),
    getGoogleRedirectUri: () => baseApi.get('external/google'),
    loginWithGoogle: (googleData) => baseApi.post(`external/google`, googleData),
    loginWithFacebook: (facebookData) => baseApi.post(`external/facebook`, facebookData),
    logout: () => baseApi.post(`external/logout`)
}