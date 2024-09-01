import { get } from '../utils/apiCaller';

export const getInfoCurrentUser = () => {
    return get(`/api/Auth/Profile`);
};

export const getAccountById = (id) => {
    return get(`/api/Account/${id}`)
}

export const getAddressesByUser = (customerId) => {
    return get(`/api/Account/${customerId}/Addresses`)
}