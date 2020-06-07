import baseApi from './baseApi'

export default {
    get: (walletId) => baseApi.get('cyclicExpense', walletId),
    save: (expense) => baseApi.post('cyclicExpense', expense),
    remove: (id) => baseApi.remove('cyclicExpense', id),
    update: (updatedData, id) => baseApi.put(`cyclicExpense/${id}`, updatedData)
}