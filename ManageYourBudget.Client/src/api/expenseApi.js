import baseApi from './baseApi'

export default {
    save: (expense) => baseApi.post('expense', expense)
}