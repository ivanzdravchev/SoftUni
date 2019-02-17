import React from 'react';
import ContactList from './contactList';
import Details from './details';
import data from '../../contacts';

const Book = () => {
    return (
        <div id="book">
            <div id="list">
                <ContactList data={data} />
            </div>
            <div id="details">
                <Details person={data[0]} />
            </div>
        </div>
    );
};

export default Book;