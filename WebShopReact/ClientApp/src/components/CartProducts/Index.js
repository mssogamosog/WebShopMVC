import React, { Component } from 'react';
export class CartProducts extends Component {
    constructor(props) {
        super(props);
        this.state = {
            CartProducts: [],
            loading: true,
            activeId: 0
        };
        fetch("CartProducts/",
            {

                method: "get",
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJDb25uZWN0aW9uIjoiV2ViU2hvcERCQ29udGV4dEluTWVtb3J5IiwibmJmIjoxNTM2NTI4NjQzLCJleHAiOjE1MzcxMzM0NDMsImlhdCI6MTUzNjUyODY0M30.i2ARXehLJr01HalW7J5lNOnvZLQK98chZEghmMFWq3g'
                },
                body: JSON.stringify(CartProducts)
            })
            .then(response => response.json())
            .then(data => {
                this.setState({
                    CartProducts: data,
                    loading: false,
                    activeId: 0
                });
            });
    }

    handleRemoveFromCart(ProductId, CartId) {
        if (!window.confirm("Are you sure to delete this product?"))
            return
        fetch('CartProducts/' + ProductId + '/' + CartId, {
            method: 'delete',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJDb25uZWN0aW9uIjoiV2ViU2hvcERCQ29udGV4dEluTWVtb3J5IiwibmJmIjoxNTM2NTI4NjQzLCJleHAiOjE1MzcxMzM0NDMsImlhdCI6MTUzNjUyODY0M30.i2ARXehLJr01HalW7J5lNOnvZLQK98chZEghmMFWq3g'
            } })
            .then(data => {
                
            })
    }
    renderTable(CartProducts) {
        console.log(CartProducts);
        return (<table className='table'>
            <thead>
                <tr>
                    <th></th>
                    <th>Name</th>
                    <th>Price</th>
                    <th>Quantity</th>
                </tr>
            </thead>
            <tbody>
                {CartProducts.map(item =>
                    <tr key={item.ProductId}>
                        <td>
                            <button className="action" onClick={() => this.handleRemoveFromCart(item.ProductId, item.CartId)}>X</button>
                        </td>
                        <td>{item.Product.Name}</td>
                        <td>{item.Product.Price}</td>
                        <td>{item.Quantity}</td>
                    </tr>
                )}
            </tbody>
        </table>
        );
    } 
    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderTable(this.state.CartProducts);
        return (<div>
            <h1>Cart Products</h1>
            {contents}
      
        </div>);
    }
}