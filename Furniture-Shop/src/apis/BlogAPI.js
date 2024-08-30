import { get } from "../utils/apiCaller"

export const searchBlog = (params) => {
    const queryString = new URLSearchParams(params).toString();
    return get(`/api/Blog/Search?${queryString}`)
}

export const getBlogById = (id) => {
    return get(`/api/Blog/${id}`)
}