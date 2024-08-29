import React, { useState } from 'react';
import {
    MDBBtn,
    MDBContainer,
    MDBRow,
    MDBCol,
    MDBCard,
    MDBCardBody,
    MDBInput,
    MDBCheckbox,
    MDBIcon
}
    from 'mdb-react-ui-kit';
import { useNavigate } from 'react-router-dom';
import { register } from '../../apis/AuthAPi';
import cookieUtils from '../../utils/cookieUtils';
import config from '../../config';

function Register() {
    const navigate = useNavigate();
    const [username, setUsername] = useState('')
    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')
    const onFinish = async () => {
        try {
            const response = await register({ email, username, password });
            const data = response.data
            if (!data) {
                throw new Error('Network response was not ok');
            } else if (response.status == 200) {
                const { accessToken } = data;
                cookieUtils.setItem(config.cookies.token, accessToken);
                setTimeout(() => {
                    navigate("/");
                }, 2000);
                console.log(data)
            } else {
                console.log(response.message)
            }
        } catch (error) {
            console.log(error)
        }
    }
    return (
        <MDBContainer fluid className='p-4'>

            <MDBRow>

                <MDBCol md='6' className='text-center text-md-start d-flex flex-column justify-content-center'>

                    <h1 className="my-5 display-3 fw-bold ls-tight px-3">
                        MH Shop <br />
                        <span className="text-primary">Furniture Heaven</span>
                    </h1>

                    <p className='px-3' style={{ color: 'hsl(217, 10%, 50.8%)' }}>
                        MH Shop is a well-known furniture brand! We offer high-quality furniture at reasonable prices and stand behind our products with a quality guarantee.
                    </p>

                </MDBCol>

                <MDBCol md='6'>

                    <MDBCard className='my-5'>
                        <MDBCardBody className='p-5'>
                            <MDBInput wrapperClass='mb-4' label='Email' id='form1' type='email' onChange={(e) => setEmail(e.target.value)} />
                            {/* <MDBInput wrapperClass='mb-4' label='Full Name' id='form1' type='text' /> */}
                            <MDBRow>
                                <MDBCol col='6'>
                                    <MDBInput wrapperClass='mb-4' label='Username' id='form1' type='text' onChange={(e) => setUsername(e.target.value)} />

                                </MDBCol>

                                <MDBCol col='6'>
                                    <MDBInput wrapperClass='mb-4' label='Password' id='form1' type='password' onChange={(e) => setPassword(e.target.value)} />
                                </MDBCol>
                            </MDBRow>



                            <div className='d-flex justify-content-center mb-4'>
                                <MDBCheckbox name='flexCheck' value='' id='flexCheckDefault' label='Subscribe to our newsletter' />
                            </div>

                            <MDBBtn className='w-100 mb-4' size='md' onClick={onFinish}>sign up</MDBBtn>

                            <div className="text-center">

                                <p>or sign up with:</p>

                                <MDBBtn tag='a' color='none' className='mx-3' style={{ color: '#1266f1' }}>
                                    <MDBIcon fab icon='facebook-f' size="sm" />
                                </MDBBtn>

                                <MDBBtn tag='a' color='none' className='mx-3' style={{ color: '#1266f1' }}>
                                    <MDBIcon fab icon='twitter' size="sm" />
                                </MDBBtn>

                                <MDBBtn tag='a' color='none' className='mx-3' style={{ color: '#1266f1' }}>
                                    <MDBIcon fab icon='google' size="sm" />
                                </MDBBtn>

                                <MDBBtn tag='a' color='none' className='mx-3' style={{ color: '#1266f1' }}>
                                    <MDBIcon fab icon='github' size="sm" />
                                </MDBBtn>
                                <div className="d-flex justify-content-between mx-4 mb-4">
                                    <a href="/">Back to Homepage</a>
                                </div>
                            </div>

                        </MDBCardBody>
                    </MDBCard>

                </MDBCol>

            </MDBRow>

        </MDBContainer>
    );
}

export default Register;