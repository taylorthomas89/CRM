import React, { Component } from 'react';
import axios from 'axios';

class Leads extends Component {
    state = { 
        leads: []
    }


    componentDidMount() {
        this.getLeads();
    }

    getLeads() {
        axios.get("http://localhost:5000/api/leads")
            .then((res) => {
                console.log(res);
                this.setState({
                    leads: res.data
                });
            });
    } 

    render() { 
        return ( 
            <div>
                <h2>Leads</h2>
                <table className="table table-striped">
                    <thead>
                        <tr>
                            <th>Id</th>
                            <th>Customer</th>
                            <th>Status</th>
                            <th>Priority</th>
                            <th>Last Contact</th>
                            <th>Employee</th>
                        </tr>
                    </thead>
                    <tbody>
                    {
                        this.state.leads.map((lead, index) => (
                            <tr key={index}>
                                <td>{ lead.id }</td>
                                <td>{ lead.customer.name }</td>
                                <td>{ lead.status.status }</td>
                                <td>{ lead.priority.priority }</td>
                                <td>{ new Date(lead.lastContact).toLocaleDateString('en-US') }</td>
                                <td>{ lead.employee.name }</td>
                            </tr>
                        ))
                    }
                    </tbody>
                </table>
            </div>
         );
    }
}
 
export default Leads;