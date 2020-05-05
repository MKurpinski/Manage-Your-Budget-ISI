import baseApi from './baseApi'

export default {
    getProfile: () => baseApi.get('profile'),
    changePassword: (passwordData) => baseApi.put('profile/password', passwordData),
    addPassword: (passwordData) => baseApi.put('profile/password/new', passwordData),
    changeProfileInfo: (profileInfo) => baseApi.put('profile', profileInfo)
}