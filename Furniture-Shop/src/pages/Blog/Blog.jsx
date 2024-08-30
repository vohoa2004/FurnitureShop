import React, { useState, useEffect } from 'react'
import Header from '../../components/Header/Header'
import TestimonialSlider from '../../components/Slider/TestimonialSlider'
import Footer from '../../components/Footer/Footer'
import { searchBlog } from '../../apis/BlogAPI';
import { formattedDate } from '../../utils/format';
import "./Blog.css"
import Pagination from '../../components/Pagination/Pagination';

export default function Blog() {
    const [createdAt, setCreatedAt] = useState(null);
    const [title, setTitle] = useState(null);
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(0);
    const [blogs, setBlogs] = useState([]);
    const [loading, setLoading] = useState(false); // Trạng thái loading khi fetch API
    const [error, setError] = useState(null); // Trạng thái lỗi nếu có lỗi khi fetch API

    const fetchBlogs = async () => {
        setLoading(true);
        setError(null);
        try {
            // Create an object with all the search parameters
            const params = {
                title: title || undefined,
                createdAt: createdAt || undefined,
                pageNo: currentPage,
                pageSize: 6,
            };

            // Filter out any undefined values
            const queryParams = Object.keys(params)
                .filter((key) => params[key] !== undefined)
                .reduce((acc, key) => {
                    acc[key] = params[key];
                    return acc;
                }, {});

            const response = await searchBlog(queryParams);
            const list = response.data.items
            setBlogs(list);
            setTotalPages(response.data.totalPages);
        } catch (error) {
            setError('Failed to fetch blogs');
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchBlogs();
    }, [currentPage]);

    const handleSearch = () => {
        setCurrentPage(1)
        fetchBlogs();
    };

    const handlePageChange = (page) => {
        setCurrentPage(page);
    };

    return (
        <div>
            <Header></Header>
            <div className="search-blog-container">

                <div className="search-field-row">
                    <input
                        type="text"
                        placeholder="Title"
                        value={title}
                        onChange={(e) => setTitle(e.target.value)}
                        className="search-field"
                    />
                </div>
                <div className="search-field-row">
                    <input aria-label="Blog Created Date" type="datetime-local" className="custom-datetime-input" onChange={(e) => setCreatedAt(e.target.value)} />
                </div>
                <button className='search-button'
                    onClick={handleSearch}
                >
                    Search
                </button>

            </div>
            <div className="blog-section">
                <div className="container">

                    <div className="row">

                        {blogs.length === 0 ? (
                            <p className='not-found'>Not found</p>
                        ) : (blogs.map((blog) => (
                            <div className="col-12 col-sm-6 col-md-4 mb-5" key={blog.id}>
                                <div className="post-entry">
                                    <a href={`/blogs/${blog.id}`} className="post-thumbnail"><img src={blog.imageLink} alt="Blog Banner Image" className="img-fluid" /></a>
                                    <div className="post-content-entry">
                                        <h3><a href="#">{blog.title}</a></h3>
                                        <div className="meta">
                                            <span className='author'>by MH Shop</span> <span className='createAt'>on {formattedDate(blog.createdAt)}</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        )))}

                    </div>
                    <Pagination currentPage={currentPage} onPageChange={handlePageChange} totalPages={totalPages}></Pagination>

                </div>

            </div>

            <TestimonialSlider></TestimonialSlider>
            <Footer></Footer>
        </div>
    )
}
