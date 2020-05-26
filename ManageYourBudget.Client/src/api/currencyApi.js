import baseApi from './baseApi'

export default {
    get: (currency, toCurrency) => baseApi.get('currency', {currency, toCurrency})
}