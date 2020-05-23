import axios from 'axios'
import { constants } from '../common';
import { BASE_URL } from '../common/constants';
import authProvider from '../authProvider';


const axiosConfig = () => {
    axios.interceptors.response.use(
        response => response,
        error => {
            switch (error.response.status) {
                case 401:
                    authProvider.removeToken();
                    window.location.href = '/';
                    break;
                default:
                    break;
            }
            return Promise.reject(error);
        }
    );

    axios.interceptors.request.use(config => {
        const token = authProvider.getToken();
        config.headers.Authorization =
            token && config.url.startsWith(BASE_URL)
                ? `Bearer ${token}`
                : '';
        return config;
    });
};

export default {
    init: axiosConfig,
    get: (url, date) =>  axios.get(buildGetUrl(url, date)).then(res => res.data),
    post: (url, date) => axios.post(buildUrl(url), getDate(date)).then(res => res.data),
    put: (url, date) => axios.put(buildUrl(url), getDate(date)).then(res => res.data),
    remove: (url, date) => axios.delete(buildUrlWithId(url, date)).then(res => res.data)
}

const buildGetUrl = (url, query) => {
    if(typeof query !== 'object'){
        return buildUrlWithId(url, query)
    }
    query = query ? query : {};
    const transformedQuery = Object.keys(query).reduce((acc, curr, index, array) => {
        acc += `${curr}=${query[curr]}${index === array.length - 1 ? '' : '&'}`;
        return acc;
    }, '');
    return `${buildUrl(url)}?${transformedQuery}`
};

const buildUrl = (url) => `${constants.BASE_URL}/${url}`;

const buildUrlWithId = (url, id) => `${buildUrl(url)}${id ? '/' + id : ''}`;

const getDate = (date) => date ? date : {};