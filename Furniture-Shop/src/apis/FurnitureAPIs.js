import { get } from '../utils/apiCaller';

export const getFurnitureById = (id) => {
    return get(`/api/Furniture/${id}`);
};

export const searchProducts = (params) => {
    const queryString = new URLSearchParams(params).toString();
    return get(`/api/Furniture/Search?${queryString}`)
}
