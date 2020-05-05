import baseApi from './baseApi'

export default {
    startFlow: (startFlowData) => baseApi.post('reset/start', startFlowData),
    reset: (resetData) => baseApi.post('reset', resetData),
    validateHash: (hash) => baseApi.post(`reset/validate${hash}`)
}