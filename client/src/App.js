import React, { Component } from 'react';
import Customers from './components/Customers';
import './App.css';

class App extends Component {

  render() {
    return (
      <div className="App container">
        <Customers />
      </div>
    );
  }
}

export default App;
