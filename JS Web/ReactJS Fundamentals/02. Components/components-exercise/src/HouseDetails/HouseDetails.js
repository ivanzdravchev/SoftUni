import React from 'react';
import './HouseDetails.css';

const houseDetails = function(props) {
    return (
        <div className="HouseDetails">
            <h2>{props.type}</h2>
            <div>
                <img src={props.imageUrl} alt=""></img>
            </div>
            <p>Description: {props.description}</p>
            <p>Price: {props.price}$</p>
        </div>
    )
}

export default houseDetails;