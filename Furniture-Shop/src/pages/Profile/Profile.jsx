import React, { useState } from 'react';
import { MDBInput, MDBBtn, MDBContainer, MDBRow, MDBCol, MDBCard, MDBCardBody, MDBCardImage, MDBCardTitle } from 'mdb-react-ui-kit';
import Header from '../../components/Header/Header';
import Footer from '../../components/Footer/Footer';
import "./Profile.css"
const Profile = () => {
    const [user, setUser] = useState({
        fullName: 'Amelia Nguyen',
        email: 'amelia@example.com',
        phone: '123-456-7890',
        address: '123 Example Street, City, Country',
    });

    const handleChange = (e) => {
        const { name, value } = e.target;
        setUser((prevUser) => ({
            ...prevUser,
            [name]: value,
        }));
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        console.log('Updated user:', user);
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
                                <MDBCardTitle>{user.fullName}</MDBCardTitle>
                                <p>Email: {user.email}</p>
                                <p>Phone: {user.phone}</p>
                                <p>Address: {user.address}</p>
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
                                            value={user.fullName}
                                            onChange={handleChange}
                                        />
                                    </div>
                                    <div className="mb-4">
                                        <MDBInput
                                            label="Email"
                                            id="email"
                                            type="email"
                                            name="email"
                                            value={user.email}
                                            onChange={handleChange}
                                        />
                                    </div>
                                    <div className="mb-4">
                                        <MDBInput
                                            label="Phone"
                                            id="phone"
                                            type="text"
                                            name="phone"
                                            value={user.phone}
                                            onChange={handleChange}
                                        />
                                    </div>
                                    <div className="mb-4">
                                        <MDBInput
                                            label="Address"
                                            id="address"
                                            type="text"
                                            name="address"
                                            value={user.address}
                                            onChange={handleChange}
                                        />
                                    </div>
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
