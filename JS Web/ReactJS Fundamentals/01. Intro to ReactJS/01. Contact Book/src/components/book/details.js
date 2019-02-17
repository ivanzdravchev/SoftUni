import React, { Component } from 'react';

class Details extends Component {
    render() {
        let person = this.props.person;
        return (
            <React.Fragment>
                <h1>Details</h1>
                <div className="content">
                    <div className="info">
                        <div className="col">
                            <span className="avatar">&#9787;</span>
                        </div>
                        <div className="col">
                            <span className="name">{person.firstName}</span>
                            <span className="name">{person.lastName}</span>
                        </div>
                    </div>
                    <div className="info">
                        <span className="info-line">&#9742; {person.phone}</span>
                        <span className="info-line">&#9993; {person.email}</span>
                    </div>
                </div>
            </React.Fragment>
        )
    }
}

export default Details;