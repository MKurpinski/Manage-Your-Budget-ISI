import baseApi from './baseApi'

export default {
    get: (currency, toCurrency, at) => baseApi.get('currency', {currency, toCurrency, at})
}