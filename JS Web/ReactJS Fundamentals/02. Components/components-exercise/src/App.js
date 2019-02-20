import React, { Component } from 'react';
import './App.css';
import Street from './Street/Street';
import House from './House/House';
import HouseDetails from './HouseDetails/HouseDetails';

class App extends Component {
    constructor(props) {
        super(props);
        this.state = {
            streets: [],
            selectedStreetIndex: 0,
            selectedHouseIndex: 0,
            hasFetched: false
        };
    }

    getSelectedStreet() {
        return this.state.streets[this.state.selectedStreetIndex].homes;
    }

    getSelectedHouse() {
        return this.state.streets[this.state.selectedStreetIndex].homes[this.state.selectedHouseIndex];
    }

    streetHoverEvent(index) {
        this.setState({
            selectedStreetIndex: index
        });
    }

    houseHoverEvent(index) {
        this.setState({
            selectedHouseIndex: index
        });
    }

    render() {
        if (!this.state.hasFetched) {
            return null;
        }
        return (
            <div className="App">
                <div className="streets">
                    <h2>Streets</h2>
                    {this.state.streets.length > 0 ? this.state.streets.map((street, index) => {
                        return (<Street location={street.location} key={index} id={index} 
                            streetHoverEvent={this.streetHoverEvent.bind(this)} />);
                    }) : null}
                </div>

                <div className="houses">
                    <h2>Houses</h2>
                    {this.getSelectedStreet().map((home, index) => {
                        return (<House type={home.type} description={home.description} price={home.price} 
                            imageUrl={home.imageUrl} id={index} key={index}
                            houseHoverEvent={this.houseHoverEvent.bind(this)} />);
                    })}
                </div>

                {this.state.streets.length > 0 ? <HouseDetails type={this.getSelectedHouse().type} 
                description={this.getSelectedHouse().description} imageUrl={this.getSelectedHouse().imageUrl}
                price={this.getSelectedHouse().price} key={this.state.selectedHouseIndex} /> : null}
            </div>
        );
    }

    componentWillMount() {
        fetch('http://localhost:9999/feed/street/all')
            .then(response => response.json())
            .then(data => {
                this.setState({
                    streets: data.streets,
                    hasFetched: true
                });
            });
    }
}

export default App;
