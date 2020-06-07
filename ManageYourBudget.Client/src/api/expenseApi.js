import baseApi from './baseApi'

export default {
    save: (expense) => baseApi.post('expense', expense),
    remove: (id) => baseApi.remove('expense', id),
    update: (updatedData, id) => baseApi.put(`expense/${id}`, updatedData)
}