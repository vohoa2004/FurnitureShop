import axios from 'axios';
import config from '../config';
import cookieUtils from './cookieUtils';

/**
 * Creates an Axios instance for making HTTP requests.
 *
 * @param {string} endpoint - The API endpoint to which the request should be made.
 * @param {string} method - The HTTP method for the request (e.g., 'GET', 'POST', 'PUT', 'DELETE').
 * @param {object} [headers={}] - An object containing custom headers for the request. Default is an empty object.
 * @param {object} [params={}] - An object containing URL parameters for the request. Default is an empty object.
 * @param {object} [body={}] - An object containing the request body. Default is an empty object.
 * @returns {Promise} - A Promise that resolves to the response of the HTTP request.
 */
export const request = (
    endpoint,
    method,
    headers = {},
    params = {},
    body = {}
) => {
    const token = cookieUtils.getToken();
    const authHeaders = token ? { Authorization: `Bearer ${token}` } : {};
    return axios({
        url: config.publicRuntime.API_URL + endpoint,
        method: method,
        headers: Object.assign({}, headers, authHeaders),
        params: Object.assign(params),
        data: body,
    });
};

/**
 * Sends a GET request to the specified endpoint.
 *
 * @param {string} endpoint - The API endpoint to which the GET request should be made.
 * @param {object} [params={}] - An object containing URL parameters for the request. Default is an empty object.
 * @param {object} [headers={}] - An object containing custom headers for the request. Default is an empty object.
 * @returns {Promise} - A Promise that resolves to the response of the HTTP request.
 */
export const get = (
    endpoint,
    params = {},
    headers = {}
) => {
    return request(endpoint, 'GET', headers, params);
};

/**
 * Sends a POST request to the specified endpoint.
 *
 * @param {string} endpoint - The API endpoint to which the POST request should be made.
 * @param {object} [body={}] - An object containing the request body. Default is an empty object.
 * @param {object} [params={}] - An object containing URL parameters for the request. Default is an empty object.
 * @param {object} [headers={}] - An object containing custom headers for the request. Default is an empty object.
 * @returns {Promise} - A Promise that resolves to the response of the HTTP request.
 */
export const post = (
    endpoint,
    body = {},
    params = {},
    headers = {}
) => {
    return request(endpoint, 'POST', headers, params, body);
};

/**
 * Sends a PUT request to the specified endpoint.
 *
 * @param {string} endpoint - The API endpoint to which the PUT request should be made.
 * @param {object} [body={}] - An object containing the request body. Default is an empty object.
 * @param {object} [params={}] - An object containing URL parameters for the request. Default is an empty object.
 * @param {object} [headers={}] - An object containing custom headers for the request. Default is an empty object.
 * @returns {Promise} - A Promise that resolves to the response of the HTTP request.
 */
export const put = (
    endpoint,
    body = {},
    params = {},
    headers = {}
) => {
    return request(endpoint, 'PUT', headers, params, body);
};

/**
 * Sends a DELETE request to the specified endpoint.
 *
 * @param {string} endpoint - The API endpoint to which the DELETE request should be made.
 * @param {object} [body={}] - An object containing the request body. Default is an empty object.
 * @param {object} [params={}] - An object containing URL parameters for the request. Default is an empty object.
 * @param {object} [headers={}] - An object containing custom headers for the request. Default is an empty object.
 * @returns {Promise} - A Promise that resolves to the response of the HTTP request.
 */
export const remove = (
    endpoint,
    body = {},
    params = {},
    headers = {}
) => {
    return request(endpoint, 'DELETE', headers, params, body);
};
