import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom'; // Nếu bạn dùng React Router
import cookieUtils from '../../utils/cookieUtils';
import config from '../../config';
import './UserMenu.css'

const UserMenu = () => {
    const [menuOpen, setMenuOpen] = useState(false);
    const navigate = useNavigate();

    const handleLogout = () => {
        cookieUtils.removeItem(config.cookies.token);
        navigate("/login")
    };

    const existedToken = cookieUtils.getToken()
    console.log("userToken: ", existedToken)
    if (existedToken == null || existedToken == undefined || existedToken == '') {
        return (
            <li><a className="nav-link" href='/login'>Login</a></li>
        )
    }
    else {
        return (
            <li>
                <a className="nav-link" onClick={() => {
                    console.log('Icon clicked');
                    setMenuOpen(!menuOpen)
                }}>
                    <img src="src/assets/images/user.svg" alt="User" />
                </a>
                {menuOpen && (
                    <div className="dropdown-menu" style={{ position: 'absolute', top: '100%', right: 0 }}>
                        <a className="dropdown-item" href="/profile">Profile</a>
                        <a className="dropdown-item" href="/history-orders">Orders</a>
                        <div className="dropdown-divider"></div>
                        <a className="dropdown-item" onClick={handleLogout}>Logout</a>
                    </div>
                )}
            </li>
        );
    }
};

export default UserMenu;
