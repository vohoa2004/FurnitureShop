import { post } from '../utils/apiCaller';

export const login = (loginDTO) => {
    return post(`/api/Auth/LoginWithUsernameAndPassword`, loginDTO);
};

export const register = (registerDTO) => {
    return post(`/api/Auth/Register`, registerDTO)
}