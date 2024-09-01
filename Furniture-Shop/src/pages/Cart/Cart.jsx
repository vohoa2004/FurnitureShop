import React, { useState, useEffect, useMemo } from 'react'
import { useNavigate } from 'react-router-dom';
import Header from '../../components/Header/Header'
import Footer from '../../components/Footer/Footer'
import './Cart.css'
import Hero from '../../components/Hero/Hero'
import { useAuth } from '../../hooks';
import { getFurnitureById } from '../../apis/FurnitureAPIs';

export default function Cart() {
    const title = "Cart"
    const [cartItems, setCartItems] = useState([]);

    const navigate = useNavigate();
    const { user, role } = useAuth();

    const customerId = user?.userId

    const cart = useMemo(() => {
        return JSON.parse(localStorage.getItem('cart-furniture')) || {};
    }, []);
    console.log("All Cart items: ", cart)

    useEffect(() => {
        if (customerId === undefined) {
            return; // Nếu customerId là undefined, không thực hiện gì và không load trang cart
        }
        // Lấy cart từ localStorage
        console.log("ID to get cart:", customerId)

        // Lấy giỏ hàng của customer hiện tại
        const fetchCartItems = async () => {
            if (customerId) {
                const userCart = cart[customerId] || [];
                const fetchedItems = await Promise.all(
                    userCart.map(async (item) => {
                        const response = await getFurnitureById(item.productId);
                        const product = await response.data;
                        return {
                            ...product,
                            quantity: item.quantity
                        };
                    })
                );
                setCartItems(fetchedItems);
            }
        };
        // Cập nhật state với các sản phẩm trong giỏ hàng
        fetchCartItems();
        console.log("Cart items: ", cartItems)
    }, [customerId, cart]);

    const updateQuantity = (productId, newQuantity) => {
        // Cập nhật state với số lượng mới
        const updatedCartItems = cartItems.map(item => {
            if (item.id === productId) {
                if (newQuantity > item.availableQuantity) {
                    alert("New quantity is higher than available quantity!");
                    return { ...item, quantity: item.availableQuantity }; // Đặt quantity thành availableQuantity nếu vượt quá
                }
                return { ...item, quantity: newQuantity }; // Chỉ cập nhật số lượng nếu điều kiện trên không xảy ra
            }
            return item; // Các sản phẩm khác không thay đổi
        });

        // Cập nhật state với cart đã cập nhật
        setCartItems(updatedCartItems);

        // Sử dụng updatedCartItems để lưu vào localStorage
        const updatedCartItemsInLocalStorage = updatedCartItems.map(item => {
            return { productId: item.id, quantity: item.quantity }; // Cập nhật tất cả các sản phẩm
        });

        updateCartInLocalStorage(updatedCartItemsInLocalStorage);
    };


    // Hàm để xóa sản phẩm khỏi giỏ hàng
    const removeItem = (productId) => {
        const updatedCartItems = cartItems.filter(item => item.id !== productId);

        // Cập nhật state
        setCartItems(updatedCartItems);

        // Cập nhật localStorage với updatedCartItems
        const updatedCartItemsInLocalStorage = updatedCartItems.map(item => {
            return { productId: item.id, quantity: item.quantity }; // Các sản phẩm khác không thay đổi
        });

        updateCartInLocalStorage(updatedCartItemsInLocalStorage);
    };

    // Hàm để lưu cart vào localStorage
    const updateCartInLocalStorage = (updatedCartItems) => {
        const cart = JSON.parse(localStorage.getItem('cart-furniture')) || {};
        cart[customerId] = updatedCartItems;
        localStorage.setItem('cart-furniture', JSON.stringify(cart));
    };

    // checkout cart to store to go to checkout site only
    function proceedToCheckout() {
        // Giả sử cartItems là mảng chứa các sản phẩm trong giỏ hàng
        localStorage.setItem('checkoutCart', JSON.stringify(cartItems));

        // Chuyển hướng người dùng đến trang checkout
        navigate('/checkout');  // Sử dụng hook navigate của react-router-dom hoặc phương pháp tương tự
    }

    const subtotal = cartItems.reduce((sum, item) => sum + item.price * item.quantity, 0);

    const formattedPrice = (price) => {
        return new Intl.NumberFormat().format(price);
    };

    return (
        <div>

            {/* <!-- Start Header/Navigation --> */}
            <Header></Header>
            {/* <!-- End Header/Navigation --> */}

            {/* <!-- Start Hero Section --> */}
            <Hero title={title}></Hero>
            {/* <!-- End Hero Section --> */}

            <div className="untree_co-section before-footer-section">
                <div className="container">
                    <div className="row mb-5">
                        <form className="col-md-12" method="post">
                            <div className="site-blocks-table">
                                <table className="table">
                                    <thead>
                                        <tr>
                                            <th className="product-thumbnail">Image</th>
                                            <th className="product-name">Product</th>
                                            <th className="product-price">Price</th>
                                            <th className="product-quantity">Quantity</th>
                                            <th className="product-total">Total</th>
                                            <th className="product-remove">Remove</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {cartItems.map((item) => (
                                            <tr key={item.id}>
                                                <td className="product-thumbnail">
                                                    <img src={item.imageLink} alt={item.name} className="img-fluid" />
                                                </td>
                                                <td className="product-name">
                                                    <h2 className="h5 text-black">{item.name}</h2>
                                                </td>
                                                <td>{formattedPrice(item.price)} VND</td>
                                                <td>
                                                    <div className="input-group mb-3 d-flex align-items-center quantity-container" style={{ maxWidth: "120px" }}>
                                                        <div className="input-group-prepend">
                                                            <button
                                                                className="btn btn-outline-black decrease"
                                                                type="button"
                                                                onClick={() => updateQuantity(item.id, item.quantity > 1 ? item.quantity - 1 : 1)}
                                                            >
                                                                &minus;
                                                            </button>
                                                        </div>
                                                        <input
                                                            type="text"
                                                            className="form-control text-center quantity-amount"
                                                            value={item.quantity}
                                                            readOnly
                                                        />
                                                        <div className="input-group-append">
                                                            <button
                                                                className="btn btn-outline-black increase"
                                                                type="button"
                                                                onClick={() => updateQuantity(item.id, item.quantity + 1)}
                                                            >
                                                                +
                                                            </button>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td>{formattedPrice(item.price * item.quantity)} VND</td>
                                                <td>
                                                    <button
                                                        className="btn btn-black btn-sm"
                                                        onClick={() => removeItem(item.id)}
                                                    >
                                                        X
                                                    </button>
                                                </td>
                                            </tr>
                                        ))}
                                    </tbody>
                                </table>
                            </div>
                        </form>
                    </div>

                    <div className="row">
                        <div className="col-md-6">
                            <div className="row mb-5">
                                {/* <div className="col-md-6 mb-3 mb-md-0">
                                    <button className="btn btn-black btn-sm btn-block">Update Cart</button>
                                </div> */}
                                <div className="col-md-6">
                                    <a className="btn btn-outline-black btn-sm btn-block" href='/search-product'>Continue Shopping</a>
                                </div>
                            </div>
                            <div className="row">
                                <div className="col-md-12">
                                    <label className="text-black h4" for="coupon">Coupon</label>
                                    <p>Enter your coupon code if you have one.</p>
                                </div>
                                <div className="col-md-8 mb-3 mb-md-0">
                                    <input type="text" className="form-control py-3" id="coupon" placeholder="Coupon Code" />
                                </div>
                                <div className="col-md-4">
                                    <button className="btn btn-black">Apply Coupon</button>
                                </div>
                            </div>
                        </div>
                        <div className="col-md-6 pl-5">
                            <div className="row justify-content-end">
                                <div className="col-md-7">
                                    <div className="row">
                                        <div className="col-md-12 text-right border-bottom mb-5">
                                            <h3 className="text-black h4 text-uppercase">Cart Totals</h3>
                                        </div>
                                    </div>
                                    <div className="row mb-3">
                                        <div className="col-md-6">
                                            <span className="text-black">Subtotal</span>
                                        </div>
                                        <div className="col-md-6 text-right">
                                            <strong className="text-black">{formattedPrice(subtotal)} VND</strong>
                                        </div>
                                    </div>
                                    <div className="row mb-5">
                                        <div className="col-md-6">
                                            <span className="text-black">Total</span>
                                        </div>
                                        <div className="col-md-6 text-right">
                                            <strong className="text-black">{formattedPrice(subtotal)} VND</strong>
                                        </div>
                                    </div>

                                    <div className="row">
                                        <div className="col-md-12">
                                            <button
                                                className="btn btn-black btn-lg py-3 btn-block"
                                                onClick={proceedToCheckout}
                                            >
                                                Proceed To Checkout
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            {/* <!-- Start Footer Section --> */}
            <Footer></Footer>
            {/* <!-- End Footer Section -->	 */}


            {/* <script src="js/bootstrap.bundle.min.js"></script>
            <script src="js/tiny-slider.js"></script>
            <script src="js/custom.js"></script> */}
        </div>
    )
}
