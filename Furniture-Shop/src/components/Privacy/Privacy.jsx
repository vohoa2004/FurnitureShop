import React, { useState } from 'react';
import { Modal, Button } from 'react-bootstrap';

const PrivacyPopup = () => {
    const [show, setShow] = useState(false);

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    return (
        <>
            <span style={{ fontWeight: 'bold' }}>
                <a href="#" onClick={handleShow}>Privacy and Terms</a>
            </span>

            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Privacy and Terms</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    {/* Nội dung của Privacy and Terms */}
                    <p>
                        - You must commit to receiving and fully paying for the order upon delivery.<br />
                        - Cancellations after delivery will only be handled on the same day if the issue is due to our store's mistake (poor product quality or late delivery.)<br />
                        - Order cancellations are only allowed before the order reaches the "delivering" status.<br />
                        - If you cancel orders before delivery more than 5 times, you will be permanently banned from our store.<br />
                    </p>
                    {/* Thêm nội dung theo yêu cầu */}
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Close
                    </Button>
                </Modal.Footer>
            </Modal>
        </>
    );
};

export default PrivacyPopup