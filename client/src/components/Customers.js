import React, { Component } from 'react';
import axios from 'axios';

class Customers extends Component {
    state = {
        customers: []
    }

    componentDidMount() {
        axios.get("http://localhost:5000/api/customers")
        .then((res) => {
            console.log(res);
            this.setState({
            customers: res.data
            });
        });
    }

    render() { 
        return (
            <div>
                <h2>Customers</h2>
                <table className="table table-striped">
                    <thead>
                        <tr>
                            <th scope="col">Id</th>
                            <th scopr="col">Name</th>
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
            </div> 
            
         );
    }
}
 
export default Customers;