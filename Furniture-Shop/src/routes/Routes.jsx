import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Landing from '../pages/Landing/Landing';
import SearchProduct from '../pages/SearchProduct/SearchProduct';
import Checkout from '../pages/Order/Checkout';
import ProductDetails from '../pages/ProductDetail/ProductDetail';
import Cart from '../pages/Cart/Cart';
import PaymentStatus from '../pages/Payment/PaymentStatus';
import Login from '../pages/Login/Login';
import Register from '../pages/Login/Register';
import Profile from '../pages/Profile/Profile';
import UserOrderList from '../pages/Order/UserOrderList';
import Blog from '../pages/Blog/Blog';
import BlogDetails from '../pages/BlogDetails/BlogDetails';

export const AppRoutes = () => {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<Landing />} />
                <Route path="/login" element={<Login />} />
                <Route path="/register" element={<Register />} />
                <Route path="/profile" element={<Profile />} />
                <Route path="/history-orders" element={<UserOrderList />} />
                <Route path="/search-product" element={<SearchProduct />} />
                <Route path="/cart" element={<Cart />} />
                <Route path="/checkout" element={<Checkout />} />
                <Route path="/product-details/:productId" element={<ProductDetails />} />
                <Route path="/payment-status" element={<PaymentStatus />} />
                <Route path="/blogs" element={<Blog />} />
                <Route path="/blogs/:blogId" element={<BlogDetails />} />
            </Routes>
        </Router>
    )
}
