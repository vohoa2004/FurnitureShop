import { get, post } from '../utils/apiCaller';

export const createOrder = (orderDto) => {
    return post(`/api/Order/CreateOrder`, orderDto);
};

export const createOrderVNPay = (params, orderDto) => {
    return post(`/api/Order/ProcessPaymentAfterCheckingStatus${params}`, orderDto);
};

export const checkPaymentStatus = (params) => {
    return get(`/api/Order/CheckPaymentStatus${params}`);
};

export const getOrdersByCustomer = (customerId, params) => {
    const queryString = new URLSearchParams(params).toString();
    return get(`/api/Order/${customerId}?${queryString}`);
}