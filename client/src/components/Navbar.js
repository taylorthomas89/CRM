import React, { Component } from 'react';
import { Link } from 'react-router-dom';

class Navbar extends Component {
    constructor(props) {
        super(props);
        this.state = {  }
    }
    render() { 
        return ( 
            <div>
                <nav className="navbar navbar-expand-lg navbar-light bg-light">
                    <a className="navbar-brand">Navbar</a>
                    <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNavAltMarkup" aria-controls="navbarNavAltMarkup" aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="collapse navbar-collapse" id="navbarNavAltMarkup">
                        <div className="navbar-nav">
                        <Link to='/'><a className="nav-item nav-link active">Home <span className="sr-only">(current)</span></a></Link>
                        <Link to='/customers'><a className="nav-item nav-link">Customers</a></Link>
                        <Link to='/leads'><a className="nav-item nav-link">Leads</a></Link>
                        </div>
                    </div>
                </nav>
            </div>
         );
    }
}
 
export default Navbar;