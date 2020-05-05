import baseApi from './baseApi'

export default {
    createNew: (wallet) => baseApi.post('wallet', wallet),
    getAll: () => baseApi.get('wallet'),
    get: (id) => baseApi.get('wallet', id),
    update: (wallet, id) => baseApi.put(`${wallet}/${id}`, wallet),
    archive: (id) => baseApi.remove('wallet', id),
}