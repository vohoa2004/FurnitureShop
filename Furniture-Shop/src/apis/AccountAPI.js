import { get } from '../utils/apiCaller';

export const getInfoCurrentUser = () => {
    return get(`/api/Auth/Profile`);
};