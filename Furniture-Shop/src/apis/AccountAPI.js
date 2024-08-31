import { get } from '../utils/apiCaller';

export const getInfoCurrentUser = () => {
    return get(`/api/Auth/Profile`);
};

export const getAddressesByUser = (customerId) => {
    return get(`/api/Account/${customerId}/Addresses`)
}