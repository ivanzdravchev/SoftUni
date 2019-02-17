import React from 'react';
import ReactDOM from 'react-dom';
import Contact from './contact';
import Details from './details';

const Contacts = (props) => {
    let contacts = [];
    let data = props.data;

    let updateDetails = function (index) {
        return () => {
            ReactDOM.render(<Details person={data[index]} />, document.getElementById('details'));
        }
    }

    for(let i = 0; i < data.length; i++) {
        let contact = data[i];
        contacts.push(<Contact contact={contact} key={i} id={i} MouseAction={updateDetails(i)} />);
    }

    return contacts;
};

const ContactList = (props) => {
    return (
        <React.Fragment>
            <h1>Contacts</h1>
            <div className="content">
                <Contacts data={props.data} />
            </div>
        </React.Fragment>
    );
};

export default ContactList;