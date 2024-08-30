import React, { useState } from 'react'
import { useNavigate } from 'react-router-dom';
import Header from '../../components/Header/Header'
import Footer from '../../components/Footer/Footer'
import './Checkout.css'
import Hero from '../../components/Hero/Hero'
import { createOrder } from '../../apis/OrderAPIs'
import { useAuth } from '../../hooks';
import PrivacyPopup from '../../components/Privacy/Privacy';

export default function Checkout() {
  const checkoutCart = JSON.parse(localStorage.getItem('checkoutCart')) || [];
  if (checkoutCart == []) {
    navigate("/")
  }
  const { user, role } = useAuth();

  const navigate = useNavigate();

  const [selectedPaymentMethod, setSelectedPaymentMethod] = useState(0);
  const [isChecked, setIsChecked] = useState(false); // for privacy and terms

  const customerId = user?.userId; // Lấy customerId từ hệ thống authentication

  const calculateTotalPrice = () => {
    const total = checkoutCart.reduce((sum, item) => sum + item.price * item.quantity, 0)
    return total;
  };

  const handleChange = (event) => {
    setSelectedPaymentMethod(Number(event.target.value));
  };

  const handleChangeCheckBox = () => {
    // Toggle trạng thái của checkbox
    setIsChecked(!isChecked);
  };


  const handleSubmit = async (event) => {
    event.preventDefault() // ngan form tu redirect, very important!!
    if (!isChecked) {
      alert("You must agree to the Privacy and Terms before placing the order.");
      return;
    }
    try {
      if (orderDTO.paymentMethod > 0) {
        localStorage.setItem('orderDTO', JSON.stringify(orderDTO));
        // goi api check payment ben trang payment status xong roi xoa
      }
      const response = await createOrder(orderDTO)
      console.log(response)

      const data = response.data;

      if (response.status == 200) {
        console.log('Payment succeeded! Order placed successfully:', data);
      }

      localStorage.removeItem('checkoutCart'); // xóa checkout Cart

      if (orderDTO.paymentMethod > 0) {
        window.location.href = data;
      } else {
        navigate("/payment-status?paymentMethod=COD")
      }
      // Lưu orderDTO vào cookie chỉ khi paymentMethod > 0

      // Điều hướng người dùng đến trang cảm ơn sau khi đặt hàng thành công
      // window.location.href = 'thankyou.html';
    } catch (error) {
      console.error('Error placing order:', error);
    }
  };

  const orderDTO = {
    customerId: customerId,
    lines: checkoutCart.map(item => ({
      productId: item.id,
      quantity: item.quantity,
      linePrice: item.price * item.quantity
    })),
    totalPrice: calculateTotalPrice(),
    paymentMethod: selectedPaymentMethod,
    isPaid: false // Cập nhật giá trị này tùy thuộc vào trạng thái thanh toán
  };

  console.log('Order DTO:', orderDTO);

  const formattedPrice = (price) => {
    return new Intl.NumberFormat().format(price);
  };

  return (
    <div>
      {/* <!-- Start Header/Navigation --> */}
      <Header></Header>
      {/* <!-- End Header/Navigation --> */}

      {/* <!-- Start Hero Section --> */}
      <Hero title="Checkout"></Hero>
      {/* <!-- End Hero Section --> */}

      <div className="untree_co-section">
        <div className="container">
          {/* customer info */}
          <div className="row mb-5">
            <div className="col-md-12">
              <div className="border p-4 rounded" role="alert">
                Returning customer? <a href="#">Click here</a> to login
              </div>
            </div>
          </div>
          <div className="row">
            <div className="col-md-6 mb-5 mb-md-0">
              <h2 className="h3 mb-3 text-black">Billing Details</h2>
              <div className="p-3 p-lg-5 border bg-white">
                <div className="form-group">
                  <label htmlFor="c_country" className="text-black">Country <span className="text-danger">*</span></label>
                  <select id="c_country" className="form-control">
                    <option value="1">Select a country</option>
                    <option value="2">bangladesh</option>
                    <option value="3">Algeria</option>
                    <option value="4">Afghanistan</option>
                    <option value="5">Ghana</option>
                    <option value="6">Albania</option>
                    <option value="7">Bahrain</option>
                    <option value="8">Colombia</option>
                    <option value="9">Dominican Republic</option>
                  </select>
                </div>
                <div className="form-group row">
                  <div className="col-md-6">
                    <label htmlFor="c_fname" className="text-black">First Name <span className="text-danger">*</span></label>
                    <input type="text" className="form-control" id="c_fname" name="c_fname" />
                  </div>
                  <div className="col-md-6">
                    <label htmlFor="c_lname" className="text-black">Last Name <span className="text-danger">*</span></label>
                    <input type="text" className="form-control" id="c_lname" name="c_lname" />
                  </div>
                </div>

                <div className="form-group row">
                  <div className="col-md-12">
                    <label htmlFor="c_companyname" className="text-black">Company Name </label>
                    <input type="text" className="form-control" id="c_companyname" name="c_companyname" />
                  </div>
                </div>

                <div className="form-group row">
                  <div className="col-md-12">
                    <label htmlFor="c_address" className="text-black">Address <span className="text-danger">*</span></label>
                    <input type="text" className="form-control" id="c_address" name="c_address" placeholder="Street address" />
                  </div>
                </div>

                <div className="form-group mt-3">
                  <input type="text" className="form-control" placeholder="Apartment, suite, unit etc. (optional)" />
                </div>

                <div className="form-group row">
                  <div className="col-md-6">
                    <label htmlFor="c_state_country" className="text-black">State / Country <span className="text-danger">*</span></label>
                    <input type="text" className="form-control" id="c_state_country" name="c_state_country" />
                  </div>
                  <div className="col-md-6">
                    <label htmlFor="c_postal_zip" className="text-black">Posta / Zip <span className="text-danger">*</span></label>
                    <input type="text" className="form-control" id="c_postal_zip" name="c_postal_zip" />
                  </div>
                </div>

                <div className="form-group row mb-5">
                  <div className="col-md-6">
                    <label htmlFor="c_email_address" className="text-black">Email Address <span className="text-danger">*</span></label>
                    <input type="text" className="form-control" id="c_email_address" name="c_email_address" />
                  </div>
                  <div className="col-md-6">
                    <label htmlFor="c_phone" className="text-black">Phone <span className="text-danger">*</span></label>
                    <input type="text" className="form-control" id="c_phone" name="c_phone" placeholder="Phone Number" />
                  </div>
                </div>

                <div className="form-group">
                  <label htmlFor="c_create_account" className="text-black" data-bs-toggle="collapse" href="#create_an_account" role="button" aria-expanded="false" aria-controls="create_an_account">
                    <input type="checkbox" value="1" id="c_create_account" /> Create an account?
                  </label>
                  <div className="collapse" id="create_an_account">
                    <div className="py-2 mb-4">
                      <p className="mb-3">Create an account by entering the information below. If you are a returning customer please login at the top of the page.</p>
                      <div className="form-group">
                        <label htmlFor="c_account_password" className="text-black">Account Password</label>
                        <input type="email" className="form-control" id="c_account_password" name="c_account_password" placeholder="" />
                      </div>
                    </div>
                  </div>
                </div>


                <div className="form-group">
                  <label htmlFor="c_ship_different_address" className="text-black" data-bs-toggle="collapse" href="#ship_different_address" role="button" aria-expanded="false" aria-controls="ship_different_address"><input type="checkbox" value="1" id="c_ship_different_address" /> Ship To A Different Address?</label>
                  <div className="collapse" id="ship_different_address">
                    <div className="py-2">

                      <div className="form-group">
                        <label htmlFor="c_diff_country" className="text-black">Country <span className="text-danger">*</span></label>
                        <select id="c_diff_country" className="form-control">
                          <option value="1">Select a country</option>
                          <option value="2">bangladesh</option>
                          <option value="3">Algeria</option>
                          <option value="4">Afghanistan</option>
                          <option value="5">Ghana</option>
                          <option value="6">Albania</option>
                          <option value="7">Bahrain</option>
                          <option value="8">Colombia</option>
                          <option value="9">Dominican Republic</option>
                        </select>
                      </div>


                      <div className="form-group row">
                        <div className="col-md-6">
                          <label htmlFor="c_diff_fname" className="text-black">First Name <span className="text-danger">*</span></label>
                          <input type="text" className="form-control" id="c_diff_fname" name="c_diff_fname" />
                        </div>
                        <div className="col-md-6">
                          <label htmlFor="c_diff_lname" className="text-black">Last Name <span className="text-danger">*</span></label>
                          <input type="text" className="form-control" id="c_diff_lname" name="c_diff_lname" />
                        </div>
                      </div>

                      <div className="form-group row">
                        <div className="col-md-12">
                          <label htmlFor="c_diff_companyname" className="text-black">Company Name </label>
                          <input type="text" className="form-control" id="c_diff_companyname" name="c_diff_companyname" />
                        </div>
                      </div>

                      <div className="form-group row  mb-3">
                        <div className="col-md-12">
                          <label htmlFor="c_diff_address" className="text-black">Address <span className="text-danger">*</span></label>
                          <input type="text" className="form-control" id="c_diff_address" name="c_diff_address" placeholder="Street address" />
                        </div>
                      </div>

                      <div className="form-group">
                        <input type="text" className="form-control" placeholder="Apartment, suite, unit etc. (optional)" />
                      </div>

                      <div className="form-group row">
                        <div className="col-md-6">
                          <label htmlFor="c_diff_state_country" className="text-black">State / Country <span className="text-danger">*</span></label>
                          <input type="text" className="form-control" id="c_diff_state_country" name="c_diff_state_country" />
                        </div>
                        <div className="col-md-6">
                          <label htmlFor="c_diff_postal_zip" className="text-black">Posta / Zip <span className="text-danger">*</span></label>
                          <input type="text" className="form-control" id="c_diff_postal_zip" name="c_diff_postal_zip" />
                        </div>
                      </div>

                      <div className="form-group row mb-5">
                        <div className="col-md-6">
                          <label htmlFor="c_diff_email_address" className="text-black">Email Address <span className="text-danger">*</span></label>
                          <input type="text" className="form-control" id="c_diff_email_address" name="c_diff_email_address" />
                        </div>
                        <div className="col-md-6">
                          <label htmlFor="c_diff_phone" className="text-black">Phone <span className="text-danger">*</span></label>
                          <input type="text" className="form-control" id="c_diff_phone" name="c_diff_phone" placeholder="Phone Number" />
                        </div>
                      </div>

                    </div>

                  </div>
                </div>

                <div className="form-group">
                  <label htmlFor="c_order_notes" className="text-black">Order Notes</label>
                  <textarea name="c_order_notes" id="c_order_notes" cols="30" rows="5" className="form-control" placeholder="Write your notes here..."></textarea>
                </div>

              </div>
            </div>
            <div className="col-md-6">
              {/* end customer info */}

              {/* coupon */}
              <div className="row mb-5">
                <div className="col-md-12">
                  <h2 className="h3 mb-3 text-black">Coupon Code</h2>
                  <div className="p-3 p-lg-5 border bg-white">

                    <label htmlFor="c_code" className="text-black mb-3">Enter your coupon code if you have one</label>
                    <div className="input-group w-75 couponcode-wrap">
                      <input type="text" className="form-control me-2" id="c_code" placeholder="Coupon Code" aria-label="Coupon Code" aria-describedby="button-addon2" />
                      <div className="input-group-append">
                        <button className="btn btn-black btn-sm" type="button" id="button-addon2">Apply</button>
                      </div>
                    </div>

                  </div>
                </div>
              </div>
              {/* end coupon */}
              <div className="row mb-5">
                <div className="col-md-12">
                  <h2 className="h3 mb-3 text-black">Your Order</h2>
                  <div className="p-3 p-lg-5 border bg-white">
                    {/* order data */}
                    <table className="table site-block-order-table mb-5">
                      <thead>
                        <th>Product</th>
                        <th>Total</th>
                      </thead>
                      <tbody>
                        {checkoutCart.map((item) => (
                          <tr key={item.id}>
                            <td>{item.name} <strong className="mx-2">x</strong> {item.quantity}</td>
                            <td>{formattedPrice(item.price * item.quantity)} VND</td>
                          </tr>
                        ))}
                        <tr>
                          <td className="text-black font-weight-bold"><strong>Cart Subtotal</strong></td>
                          <td className="text-black">{formattedPrice(calculateTotalPrice())} VND</td>
                        </tr>
                        <tr>
                          <td className="text-black font-weight-bold"><strong>Order Total</strong></td>
                          <td className="text-black font-weight-bold"><strong>{formattedPrice(calculateTotalPrice())} VND</strong></td>
                        </tr>
                      </tbody>
                    </table>
                    {/* end order data */}

                    {/* payment methods description */}

                    {/* <div className="border p-3 mb-3">
                      <h3 className="h6 mb-0"><a className="d-block" data-bs-toggle="collapse" href="#collapsebank" role="button" aria-expanded="false" aria-controls="collapsebank">Direct Bank Transfer</a></h3>

                      <div className="collapse" id="collapsebank">
                        <div className="py-2">
                          <p className="mb-0">Make your payment directly into our bank account. Please use your Order ID as the payment reference. Your order won’t be shipped until the funds have cleared in our account.</p>
                        </div>
                      </div>
                    </div>

                    <div className="border p-3 mb-3">
                      <h3 className="h6 mb-0"><a className="d-block" data-bs-toggle="collapse" href="#collapsecheque" role="button" aria-expanded="false" aria-controls="collapsecheque">Cheque Payment</a></h3>

                      <div className="collapse" id="collapsecheque">
                        <div className="py-2">
                          <p className="mb-0">Make your payment directly into our bank account. Please use your Order ID as the payment reference. Your order won’t be shipped until the funds have cleared in our account.</p>
                        </div>
                      </div>
                    </div>

                    <div className="border p-3 mb-5">
                      <h3 className="h6 mb-0"><a className="d-block" data-bs-toggle="collapse" href="#collapsepaypal" role="button" aria-expanded="false" aria-controls="collapsepaypal">Paypal</a></h3>

                      <div className="collapse" id="collapsepaypal">
                        <div className="py-2">
                          <p className="mb-0">Make your payment directly into our bank account. Please use your Order ID as the payment reference. Your order won’t be shipped until the funds have cleared in our account.</p>
                        </div>
                      </div>
                    </div> */}
                    <form>
                      <div className="form-group mb-3">
                        <input
                          type="radio"
                          id="paymentBank"
                          name="paymentMethod"
                          value={0}
                          checked={selectedPaymentMethod === 0}
                          onChange={handleChange}
                          required
                        />
                        <label
                          htmlFor="paymentBank"
                          // className="d-block"
                          data-bs-toggle="collapse"
                          href="#collapsebank"
                          role="button"
                          aria-expanded={selectedPaymentMethod === 0}
                          aria-controls="collapsebank"
                        >
                          Cash On Delivery
                        </label>
                        <div className="collapse" id="collapsebank">
                          <div className="py-2">
                            <p className="mb-0">
                              Pay money when you receive product delivered.
                            </p>
                          </div>
                        </div>
                      </div>

                      <div className="form-group mb-5">
                        <input
                          type="radio"
                          id="paymentVNPay"
                          name="paymentMethod"
                          value={1}
                          checked={selectedPaymentMethod === 1}
                          onChange={handleChange}
                          required
                        />
                        <label
                          htmlFor="paymentVNPay"
                          // className="d-block"
                          data-bs-toggle="collapse"
                          href="#collapsevnpay"
                          role="button"
                          aria-expanded={selectedPaymentMethod === 1}
                          aria-controls="collapsevnpay"
                        >
                          VNPay
                        </label>
                        <div className="collapse" id="collapsevnpay">
                          <div className="py-2">
                            <p className="mb-0">
                              Make your payment using VN Pay (Only allows for bill has total price under 20.000.000 VND). Please follow the instructions on the VN Pay site to complete your payment.
                            </p>
                          </div>
                        </div>
                      </div>

                      {/* privacy and term */}

                      <input
                        type="checkbox"
                        id="privacy"
                        name="privacyAgree"
                        checked={isChecked}
                        onChange={handleChangeCheckBox}
                        required
                      />
                      <label
                        htmlFor="privacy"
                        // className="d-block"
                        data-bs-toggle="collapse"
                        href="#collapseprivacy"
                        role="button"
                        aria-expanded={isChecked}
                        aria-controls="collapseprivacy"
                      >
                        I agree and commit with your Privacy and Terms
                      </label>
                      <div className="collapse" id="collapseprivacy">
                        <div className="py-2">
                          <p className="mb-0">
                            By clicking to Place Order button, you will create an order, make sure you know and agree our {' '}<PrivacyPopup></PrivacyPopup>.
                          </p>
                        </div>
                      </div>

                      {/* end privacy and term */}
                      <br /><br />
                      <button className="btn btn-black btn-lg py-3 btn-block" onClick={handleSubmit}>Place Order</button>

                    </form>

                  </div>
                </div>
              </div>

            </div>
          </div>
          {/* <!-- </form> --> */}
        </div>
      </div>

      {/* <!-- Start Footer Section-- > */}
      <Footer></Footer>
      {/* <!--End Footer Section-- >	 */}
    </div>
  )
}
