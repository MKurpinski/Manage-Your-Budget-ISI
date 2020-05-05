import baseApi from './baseApi'

export default {
    assign: (data) => baseApi.put('wallets/users/assign', data),
    unassign: (data) => baseApi.put('wallets/users/unassign', data),
    changeRole: (data) => baseApi.put('wallets/users/role', data),
}