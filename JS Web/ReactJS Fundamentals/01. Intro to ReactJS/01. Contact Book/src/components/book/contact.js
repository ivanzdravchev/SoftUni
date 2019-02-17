import React from 'react';

const Contact = (props) => {
    return (
        <div className="contact" data-id={props.id} onMouseDown={props.MouseAction}>
            <span className="avatar small">&#9787;</span>
            <span className="title">{props.contact.firstName} {props.contact.lastName}</span>
        </div>
    )
};

export default Contact;