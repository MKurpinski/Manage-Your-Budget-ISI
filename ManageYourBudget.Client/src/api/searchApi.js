import baseApi from './baseApi'

export default {
    searchUsers: (searchOptions) => baseApi.get('profile/search', searchOptions)
}