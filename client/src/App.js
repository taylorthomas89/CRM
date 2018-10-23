import React, { Component } from 'react';
import Customers from './components/Customers';
import Leads from './components/Leads'
import './App.css';

class App extends Component {

  render() {
    return (
      <div className="App container">
        <Customers />
        <Leads />
      </div>
    );
  }
}

export default App;
