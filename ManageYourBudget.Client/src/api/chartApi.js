import baseApi from './baseApi'

export default {
    get: (chartType, additionalData) => baseApi.get(`chart/${chartType}`, additionalData)
}