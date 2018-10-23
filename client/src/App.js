import React, { Component } from 'react';
import { Switch, Route } from 'react-router-dom';
import Customers from './components/Customers';
import Leads from './components/Leads';
import Navbar from './components/Navbar';
import Home from './components/Home';
import './App.css';

class App extends Component {

  render() {
    return (
      <div className="App container">
      <Navbar />
        <Switch>
          <Route exact path="/" component={Home}/>
          <Route path="/leads" component={Leads} />
          <Route path="/customers" component={Customers} />
        </Switch>
      </div>
    );
  }
}

export default App;
