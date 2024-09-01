import { AccountStatus, Role } from '../utils/enum';
import { useCallback, useEffect, useState } from 'react';

import cookieUtils from '../utils/cookieUtils';
import { getInfoCurrentUser } from '../apis/AccountAPI';

const getRole = () => {
    const decoded = cookieUtils.decodeJwt();

    if (!decoded || !decoded.role) return null;

    // Check if role is an array
    if (Array.isArray(decoded.role)) {
        // Map over the array to return corresponding Role enums
        return decoded.role.map(role => Role[role]);
    }

    // If it's not an array, return the single role
    return Role[decoded.role];
};

const getStatus = () => {
    const decoded = cookieUtils.decodeJwt();

    if (!decoded || !decoded.status) return null;

    return AccountStatus[decoded.status];
};

const useAuth = () => {
    const [role, setRole] = useState(getRole());
    const [status, setStatus] = useState(getStatus());
    const [loading, setLoading] = useState(false);
    const [user, setUser] = useState();

    const token = cookieUtils.getToken();
    // console.log(token)

    // Function to check token expiration
    const checkTokenExpiration = useCallback(() => {
        if (token) {
            const decoded = cookieUtils.decodeJwt();
            console.log("Decoded data: ", decoded)
            // Check if the token is expired
            if (!decoded || decoded.exp < Date.now() / 1000) {
                console.log("Token expired!")
                setRole(null);
                setStatus(null);
                cookieUtils.deleteUser();
                return;
            } else {
                console.log("Token valid")
            }
        } else {
            console.log("Nothing!")
        }
    }, [token]);

    useEffect(() => {
        const token = cookieUtils.getToken();
        console.log("token in use effect", token)
        // If there is no token, set the role to null
        if (!token) {
            console.log("No Token!")
            setRole(null);
            setStatus(null);
            return;
        }

        try {
            setLoading(true);

            // Set role
            setRole(getRole());
            setStatus(getStatus());

            // Fetch API to get info user
            const getInfo = async () => {
                const { data } = await getInfoCurrentUser();
                setUser(data);
                console.log(user)
            };

            getInfo();

        } finally {
            setLoading(false);
        }

        // Set up an interval to check token expiration every 5 seconds
        const intervalId = setInterval(checkTokenExpiration, 5000);

        // Clean up the interval on component unmount
        return () => clearInterval(intervalId);
    }, [checkTokenExpiration]);

    return { loading, role, user, status };
};

export default useAuth;
