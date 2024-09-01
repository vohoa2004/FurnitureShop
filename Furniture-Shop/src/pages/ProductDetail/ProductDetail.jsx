import React, { useState, useEffect } from 'react';
import Header from '../../components/Header/Header';
import Footer from '../../components/Footer/Footer';
import './ProductDetails.css';
import { getFurnitureById } from '../../apis/FurnitureAPIs';
import { useAuth } from '../../hooks';

const ProductDetails = () => {
    const [product, setProduct] = useState(null);

    const productId = parseInt(window.location.pathname.split('/')[2])
    const { user, role } = useAuth();

    useEffect(() => {
        const fetchProductDetails = async () => {
            try {
                const productResponse = await getFurnitureById(productId);
                console.log(productResponse)
                setProduct(productResponse.data);
            } catch (error) {
                console.error('Failed to fetch product details', error);
            }
        };
        fetchProductDetails();
    }, [productId]);

    console.log(product)

    if (product == null) {
        return <div>Loading...</div>
    }

    function addToCart(customerId, product) {
        if (customerId == undefined || customerId == null) {
            return;
        }
        // Lấy cart hiện tại từ localStorage
        let cart = JSON.parse(localStorage.getItem('cart-furniture')) || {};

        // Kiểm tra xem cart của customerId đã tồn tại hay chưa
        if (!cart[customerId]) {
            cart[customerId] = [];
        }

        // Nếu không có quantity, đặt mặc định là 1
        if (product.quantity == null) {
            product.quantity = 1;
        }


        // Tìm xem sản phẩm đã có trong giỏ hàng hay chưa
        const existingProductIndex = cart[customerId].findIndex(item => item.id === product.id);

        if (existingProductIndex > -1) {
            // Nếu sản phẩm đã tồn tại, tăng số lượng
            cart[customerId][existingProductIndex].quantity += 1; // Chỉ tăng 1
        } else {
            // Nếu sản phẩm chưa tồn tại, thêm sản phẩm mới vào giỏ hàng
            cart[customerId].push({
                productId: product.id,
                quantity: 1 // Chắc chắn là thêm mới với quantity là 1
            });
        }

        // Lưu lại cart mới vào localStorage
        localStorage.setItem('cart-furniture', JSON.stringify(cart));
    }


    const handleAddToCart = (product) => {

        const customerId = user?.userId; // lấy customerId từ hệ thống authentication

        if (product.availableQuantity == 0) {
            alert("This product is out of stock, you cannot order it at this time! Put it to your wishlist and comeback later!");
            return;
        }
        addToCart(customerId, product);
    };

    const formattedPrice = (price) => {
        return new Intl.NumberFormat().format(price);
    };

    return (
        <>
            <Header />
            <div className="product-details-container">
                <div className="hero product-hero">
                    <div className="hero-img-wrap2">
                        <img src={product.imageLink} alt={product.name} className="main-product-img" />
                    </div>
                    <div className="intro-excerpt2">
                        <h1>{product.name}</h1>
                        <p>{product.description}</p>
                        <p>Available quantity: {product.availableQuantity}</p>
                    </div>
                </div>

                <div className="thumbnail-wrapper">
                    {/* Replace the src with actual thumbnail images if available */}
                    <img src={product.imageLink} alt="Product Thumbnail 1" className="thumbnail-img" />
                    <img src={product.imageLink} alt="Product Thumbnail 2" className="thumbnail-img" />
                    <img src={product.imageLink} alt="Product Thumbnail 3" className="thumbnail-img" />
                </div>

                <div className="product-info-section">
                    <div className="product-info">
                        <h2 className="section-title">Product Details</h2>
                        <p>{product.description}</p>

                        <h3>Features</h3>
                        <ul className="custom-list">
                            {/* Example static features, you might want to replace or map these if they come from the API */}
                            <li>High-quality wood</li>
                            <li>Modern design</li>
                            <li>Durable and long-lasting</li>
                            <li>Available in multiple colors</li>
                        </ul>

                        {/* quantity = 0 thi ko hien nut nay */}
                        <div className="product-pricing">
                            <strong>{formattedPrice(product.price)} VND</strong>
                            <a href='/cart' className="btn btn-primary" onClick={() => handleAddToCart(product)}>Add to Cart</a>
                            {/* Later you can add the add-to-cart functionality */}
                        </div>
                    </div>
                </div>

                <div className="popular-product">
                    <h2 className="section-title">You Might Also Like</h2>
                    <div className="product-item-sm">
                        <div className="thumbnail">
                            <img src={product.imageLink} alt="Related Product" />
                        </div>
                        <div className="product-info-sm">
                            <h3><a href="src/assets/images/product-1.png">Another Product</a></h3>
                            <p>Short description of the related product.</p>
                            <strong>1.099.000 VND</strong>
                        </div>
                    </div>
                </div>
            </div>

            <Footer />
        </>
    );
};

export default ProductDetails;