import React, { useState, useEffect } from 'react';
import { MDBInput, MDBBtn, MDBContainer, MDBRow, MDBCol, MDBCard, MDBCardBody, MDBCardImage, MDBCardTitle } from 'mdb-react-ui-kit';
import Header from '../../components/Header/Header';
import Footer from '../../components/Footer/Footer';
import "./Profile.css"
import { useAuth } from '../../hooks';
import { getAccountById } from '../../apis/AccountAPI';

const Profile = () => {
    const [currentUser, setCurrentUser] = useState(null);
    const { user, role } = useAuth();
    // console.log("User from useAuth: ", user)

    const customerId = user?.userId;
    // const customerId = "98826227-b58d-4405-b492-08dcc6a7ae14";
    console.log("User Id: ", customerId)

    useEffect(() => {
        const fetchUserDetails = async () => {
            try {
                const userResponse = await getAccountById(customerId);
                console.log("Response:", userResponse)
                setCurrentUser(userResponse.data);
            } catch (error) {
                console.error('Failed to fetch user details', error);
            }
        };
        fetchUserDetails();
    }, [customerId]);

    console.log("Current user: ", currentUser)

    // phai co phan nay de cho await fetch api, ko thi useEffect khong chay vi customerId khong doi
    if (currentUser == null) {
        return <div>Loading...</div>
    }

    const handleSubmit = (e) => {
        e.preventDefault();
        console.log('Updated user:', currentUser);
    };

    return (
        <>
            <Header></Header>
            <br /> <br />
            <MDBContainer className="user-profile">
                <MDBRow>
                    <MDBCol md="4">
                        <MDBCard>
                            <MDBCardImage src="https://via.placeholder.com/150" position="top" alt="Avatar" />
                            <MDBCardBody>
                                <MDBCardTitle>Username: {currentUser.username}</MDBCardTitle>
                                <p>Email: {currentUser.email}</p>
                                <p>Phone: {currentUser.phone}</p>
                                <p>Full Name: {currentUser.fullName}</p>
                            </MDBCardBody>
                        </MDBCard>
                    </MDBCol>
                    <MDBCol md="8">
                        <MDBCard>
                            <MDBCardBody>
                                <form onSubmit={handleSubmit}>
                                    <div className="mb-4">
                                        <MDBInput
                                            label="Full Name"
                                            id="fullName"
                                            type="text"
                                            name="fullName"
                                            value={currentUser.fullName}
                                        // onChange={handleChange}
                                        />
                                    </div>
                                    {/* <div className="mb-4">
                                        <MDBInput
                                            label="Email"
                                            id="email"
                                            type="email"
                                            name="email"
                                            value={currentUser.email}
                                        // onChange={handleChange}
                                        />
                                    </div> */}
                                    <div className="mb-4">
                                        <MDBInput
                                            label="Phone"
                                            id="phone"
                                            type="text"
                                            name="phone"
                                            value={currentUser.phone}
                                        // onChange={handleChange}
                                        />
                                    </div>

                                    {/* Shipping Addresses */}
                                    {/* <div className="mb-4">
                                        <MDBInput
                                            label="Address"
                                            id="address"
                                            type="text"
                                            name="address"
                                            value={currentUser.address}
                                            onChange={handleChange}
                                        />
                                    </div> */}
                                    <MDBBtn type="submit">Save Changes</MDBBtn>
                                </form>
                            </MDBCardBody>
                        </MDBCard>
                    </MDBCol>
                </MDBRow>
            </MDBContainer>
            <br /> <br />
            <Footer></Footer>
        </>

    );
};

export default Profile;
