import React, { Component } from 'react';
import axios from 'axios';

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
            this.setState({
            customers: res.data
            });
        });
    }

    onSort(event, sortKey) {
        const { customers } = this.state;
        const direction = this.state.sort.direction === 'asc' ? 'desc' : 'asc';
        const sortedData = customers.sort((a, b) => {
            const nameA = a.name.toLowerCase();
            const nameB = b.name.toLowerCase();
            
            if (nameA < nameB) return -1;
            if (nameA > nameB) return 1;
            return 0;
        });

        if (direction === 'desc') sortedData.reverse();
        this.setState({
            customers,
            sort: { direction }
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
                            <th scopr="col" onClick={(e) => this.onSort(e, 'name')}>Name</th>
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