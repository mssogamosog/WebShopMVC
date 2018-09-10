import React, { Component } from 'react';
import Authenticate from './Authenticate'
export class Customers extends Component {
    constructor(props) {
        super(props);
        this.Auth = new Authenticate();
        this.state = {
            Customers: [],
            loading: true,
            token: this.Auth.getToken(),
            activeId: 0
        };
        fetch("Customers/Details",
            {

                method: "get",
                headers: new Headers({
                    'Authorization': this.state.token
                }),
                body: JSON.stringify(Customers)
            })
            .then(response => response.json())
            .then(data => {
                this.setState({
                    Customers: data,
                    loading: false,
                    activeId: 0
                });
            });
    }
    renderTable(Customers) {
        return (<table className='table'>
            <thead>
                <tr>
                    <th></th>
                    <th>First Name</th>
                    <th>Last Name</th>
                    <th>Email</th>
                </tr>
            </thead>
            <tbody>
                {Customers.map(item =>
                    <tr key={item.CustomerId}>
                        <td>{item.FirstName}</td>
                        <td>{item.LastName}</td>
                        <td>{item.Email}</td>
                    </tr>
                )}
            </tbody>
        </table>
        );
    }
    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderTable(this.state.Customers);
        return (<div>
            <h1>User</h1>
            {contents}

        </div>);
    }
}