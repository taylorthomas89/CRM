import React, { Component } from 'react';
import axios from 'axios';

import { onSort } from './../helpers/helper-functions';

class Customers extends Component {
    state = {
        customers: [],
        sort: {
            direction: 'desc'
        }
    }

    componentDidMount() {
        this.getCustomers();
    }

    getCustomers() {
        axios.get("http://localhost:5000/api/customers")
            .then((res) => {
                console.log(res);
                this.setState({ customers: res.data });
            });
    }

    render() { 
        return (
            <div>
                <h2>Customers</h2> 
                <table className="table table-striped">
                    <thead>
                        <tr>
                            <th scope="col" onClick={(e) => onSort(e, 'id')}>Id</th>
                            <th scopr="col" onClick={(e) => onSort(e, 'name')}>Name</th>
                            <th scopr="col">Age</th>
                            <th scopr="col">Phone</th>
                            <th scopr="col">Email</th>
                            <th scopr="col">Preferred Contact</th>
                        </tr>
                    </thead>
                    <tbody>
                    {
                        this.state.customers.map((customer, index) => (
                            <tr key={index}>
                                <td>{ customer.id }</td>
                                <td>{ customer.name }</td>
                                <td>{ customer.age }</td>
                                <td>{ customer.phone }</td>
                                <td>{ customer.email }</td>
                                <td>{ customer.details.preferred_contact }</td>
                            </tr>
                        ))
                    }
                    </tbody>
                </table>
                <div className="">
                    <button className="btn btn-primary float-right">Add Customer</button>
                </div>
            </div> 
            
         );
    }
}
 
export default Customers;