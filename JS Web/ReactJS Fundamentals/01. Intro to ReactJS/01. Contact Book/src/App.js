import React, { Component } from 'react';
import './App.css';
import Header from './components/header';
import Footer from './components/footer';
import Book from './components/book/book';

class App extends Component {
  render() {
    return (
      <div className="container">
        <Header />
        <Book />
        <Footer />
      </div>
    );
  }
}

export default App;
