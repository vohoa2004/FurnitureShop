import React, { useState, useEffect } from 'react'
import Header from '../../components/Header/Header'
import Footer from '../../components/Footer/Footer'
import Hero from '../../components/Hero/Hero'
import './SearchProduct.css'
import './SearchForm.css'
import { searchProducts } from '../../apis/FurnitureAPIs'
import Pagination from '../../components/Pagination/Pagination'
import { categoryMap } from '../../utils/enum'
const SearchProduct = () => {
    const [productName, setProductName] = useState('');
    const [minPrice, setMinPrice] = useState('');
    const [maxPrice, setMaxPrice] = useState('');
    const [selectedCategories, setSelectedCategories] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(0);
    const [products, setProducts] = useState([]); // Lưu danh sách sản phẩm trả về từ API
    const [loading, setLoading] = useState(false); // Trạng thái loading khi fetch API
    const [error, setError] = useState(null); // Trạng thái lỗi nếu có lỗi khi fetch API

    const handleCategoryChange = (event) => {
        const categoryName = event.target.value;
        const categoryId = categoryMap[categoryName];

        setSelectedCategories((prev) =>
            prev.includes(categoryId)
                ? prev.filter((id) => id !== categoryId)
                : [...prev, categoryId]
        );
    };


    const fetchProducts = async () => {
        setLoading(true);
        setError(null);
        try {
            // Create an object with all the search parameters
            const params = {
                name: productName || undefined,
                minPrice: minPrice || undefined,
                maxPrice: maxPrice || undefined,
                categoryIds: selectedCategories.length > 0 ? selectedCategories : undefined,
                pageNo: currentPage,
                pageSize: 8,
            };

            // Filter out any undefined values
            const queryParams = Object.keys(params)
                .filter((key) => params[key] !== undefined)
                .reduce((acc, key) => {
                    acc[key] = params[key];
                    return acc;
                }, {});

            const response = await searchProducts(queryParams);
            const list = response.data.items
            setProducts(list);
            setTotalPages(response.data.totalPages);
        } catch (error) {
            setError('Failed to fetch products');
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchProducts();
    }, [currentPage]);

    const handleSearch = () => {
        setCurrentPage(1);
        fetchProducts();
    };


    console.log(products)

    const formattedPrice = (price) => {
        return new Intl.NumberFormat().format(price);
    };

    const handlePageChange = (page) => {
        setCurrentPage(page);
    };

    return (
        <>
            <Header></Header>
            {/* search product form: */}
            <div className="search-product-container">

                <div className="search-field-row">
                    <input
                        type="text"
                        placeholder="Product Name"
                        value={productName}
                        onChange={(e) => setProductName(e.target.value)}
                        className="search-field"
                    />
                </div>
                <div className="search-field-row">
                    <input
                        type="number"
                        placeholder="Min Price"
                        value={minPrice}
                        onChange={(e) => setMinPrice(e.target.value)}
                        className="search-field"
                    />
                    <input
                        type="number"
                        placeholder="Max Price"
                        value={maxPrice}
                        onChange={(e) => setMaxPrice(e.target.value)}
                        className="search-field"
                    />
                </div>
                <div className="category-row">
                    {Object.keys(categoryMap).slice(0, 4).map((categoryName, index) => (
                        <label key={index} className="category-item">
                            <input
                                type="checkbox"
                                value={categoryName}
                                onChange={handleCategoryChange}
                            />
                            {categoryName}
                        </label>
                    ))}
                </div>
                <div className="category-row">
                    {Object.keys(categoryMap).slice(4).map((categoryName, index) => (
                        <label key={index} className="category-item">
                            <input
                                type="checkbox"
                                value={categoryName}
                                onChange={handleCategoryChange}
                            />
                            {categoryName}
                        </label>
                    ))}
                </div>
                <div>

                    <button className='search-button'
                        onClick={handleSearch}
                    >
                        Search
                    </button>
                </div>

            </div>

            {/* search result: */}
            <div className="untree_co-section product-section before-footer-section">
                <div className="container">
                    <div className="row">
                        {
                            products.length === 0 ? (
                                <p className='not-found'>Not found</p>
                            ) : (products.map((product) => (
                                <div key={product.id} className="col-12 col-md-4 col-lg-3 mb-5">
                                    <a className="product-item" href={`/product-details/${product.id}`}>
                                        <img src={product.imageLink} className="img-fluid product-thumbnail" alt={product.name} />
                                        <h3 className="product-title">{product.name}</h3>
                                        <strong className="product-price">{formattedPrice(product.price)} VND</strong>
                                        <span className="icon-cross">
                                            <img src="src/assets/images/cross.svg" className="img-fluid" alt="cross" />
                                        </span>
                                    </a>
                                </div>
                            )))}
                    </div>
                    <Pagination currentPage={currentPage} onPageChange={handlePageChange} totalPages={totalPages}></Pagination>
                </div>
            </div>
            <Footer></Footer>
        </>
    );
};

export default SearchProduct;

