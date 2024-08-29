import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import {
    MDBBtn,
    MDBContainer,
    MDBCard,
    MDBCardBody,
    MDBCardImage,
    MDBRow,
    MDBCol,
    MDBInput,
    MDBCheckbox
}
    from 'mdb-react-ui-kit';
import { login } from '../../apis/AuthAPi';
import cookieUtils from '../../utils/cookieUtils';
import config from '../../config';



function Login() {
    const navigate = useNavigate();
    const [username, setUsername] = useState('')
    const [password, setPassword] = useState('')
    const onFinish = async () => {
        try {
            const response = await login({ username, password });
            const data = response.data
            if (!data) {
                throw new Error('Network response was not ok');
            } else if (response.status == 200) {
                const { accessToken } = data;
                cookieUtils.setItem(config.cookies.token, accessToken);
                setTimeout(() => {
                    navigate("/");
                }, 2000);
            }
        } catch (error) {
            console.log(error)
        }
    }
    return (
        <MDBContainer className='my-5'>
            <MDBCard>

                <MDBRow className='g-0 d-flex align-items-center'>
                    <MDBCol md='6'>
                        <MDBCardImage src='https://sc04.alicdn.com/kf/H3c32aa02cf324140ab21b7d01f1c2451q.jpg' alt='furniture' className='rounded-t-5 rounded-tr-lg-0' fluid />
                    </MDBCol>

                    <MDBCol md='6'>

                        <MDBCardBody style={{
                            flex: '1 1 auto',
                            padding: '5rem 5rem'
                        }}>

                            <h1 style={{
                                textAlign: 'center'
                            }}>Login</h1>
                            <br />

                            <h6 style={{
                                textAlign: 'center'
                            }}>Just a few clicks bring furniture to your door with <span className='text-primary'><a href='/'>MH Shop!</a></span>
                            </h6>
                            <br />
                            <MDBInput wrapperClass='mb-4' label='Username' id='form1' type='email' onChange={(e) => setUsername(e.target.value)} />
                            <MDBInput wrapperClass='mb-4' label='Password' id='form2' type='password' onChange={(e) => setPassword(e.target.value)} />

                            <div className="d-flex justify-content-between mx-4 mb-4">
                                <MDBCheckbox name='flexCheck' value='' id='flexCheckDefault' label='Remember me' />
                                <a href="!#">Forgot password?</a>
                            </div>

                            <MDBBtn className="mb-4 w-100" onClick={onFinish}>Sign in</MDBBtn>
                            <div className="d-flex justify-content-between mx-4 mb-4">
                                <a href="/register">Not have an account yet? Signup here!</a>
                            </div>
                        </MDBCardBody>

                    </MDBCol>

                </MDBRow>

            </MDBCard>
        </MDBContainer>
    );
}

export default Login;