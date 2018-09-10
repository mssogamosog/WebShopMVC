import React, { Component } from 'react';
import Authenticate from '../../components/Customer/Authenticate'
export class CartProducts extends Component {
    constructor(props) {
        super(props);
        this.Auth = new Authenticate();
        this.state = {
            CartProducts: [],
            loading: true,
            token: this.Auth.getToken(),
            activeId: 0
        };
        fetch("CartProducts/",
            {

                method: "get",
                headers: new Headers({
                    'Authorization': this.state.token
                }),
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
        fetch('CartProducts/' + ProductId + '/' + CartId, 
            {
                headers: new Headers({
                    'Authorization': this.state.token
                })
            }
        )
            .then(response => response.json())
            .then(data => {
                console.log(data);
                this.setState({
                    shoppingCart: data,
                    loading: false
                });
            });
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