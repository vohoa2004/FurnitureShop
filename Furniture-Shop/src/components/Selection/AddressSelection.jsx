import React from "react";
import { MDBBtn, MDBListGroup, MDBListGroupItem, MDBRadio } from "mdb-react-ui-kit";
import "./AddressSelection.css"
const AddressSelection = ({ addresses, selectedAddressId, onSelectAddress }) => {
    return (
        <div className="address-selection">
            <h4>Select an Address</h4>
            <MDBListGroup>
                {addresses.map((address) => (
                    <MDBListGroupItem key={address.id} className="d-flex align-items-center">
                        <MDBRadio
                            name="address"
                            value={address.id}
                            checked={selectedAddressId === address.id}
                            onChange={() => onSelectAddress(address.id)}
                            className="me-3"
                        />
                        <div>
                            <div><strong>House Number:</strong> {address.houseNumber}</div>
                            <div><strong>Street:</strong> {address.street}</div>
                            <div><strong>District:</strong> {address.district}</div>
                            <div><strong>City/Province:</strong> {address.provinceOrCity}</div>
                            <div><strong>Country:</strong> {address.country}</div>
                            <div><strong>Postal Code:</strong> {address.postalCode}</div>
                        </div>
                    </MDBListGroupItem>
                ))}
            </MDBListGroup>
        </div>
    );
};

export default AddressSelection;
