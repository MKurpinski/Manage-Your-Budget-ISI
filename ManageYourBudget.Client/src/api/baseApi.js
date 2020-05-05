import axios from 'axios'
import { BASE_URL } from '../constants';

export default {
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

const buildUrl = (url) => `${BASE_URL}/${url}`;

const buildUrlWithId = (url, id) => `${buildUrl(url)}${id ? '/' + id : ''}`;

const getDate = (date) => date ? date : {};