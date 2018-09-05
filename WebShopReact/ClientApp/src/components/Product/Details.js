import React from 'react';

export class Details extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            product: null,
            loading: true
        };
        this.updateInputValue = this.updateInputValue.bind(this);
        fetch('Products/' + this.props.id, { method: 'get' })
            .then(response => response.json())
            .then(data => {
                this.setState({ product: data, loading: false })
            })
    }
    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderDetails(this.state.product);
        return (<React.Fragment>
            <h1>Product Details</h1>
            {contents}
        </React.Fragment>);
    }
    addTocart(product) {
         if (this.state.inputValue > 0) {
            product.quantity = this.state.inputValue;
             fetch("CartProducts/",
                {
                    method: "post",
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(product)
                })
                .then(data => {
                    this.setState({ addedToCart: true });
                })
        }
    }
    updateInputValue(event) {
        this.setState({
            inputValue: event.target.value
        });
    }
    renderDetails(product) {
        console.log("renderDetails: " + product);
        return (
            <div className="details">
                <label>Id</label>
                <div>{product.ProductId}</div>
                <label>Name</label>
                <div>{product.Name}</div>
                <label>Quantity</label>
                <div>{product.Quantity}</div>
                <label>Price</label>
                <div>{product.Price}</div>
                <td>
                   
                    <label>Quantity</label>
                    <input id='CartQuantity' name='CartQuantity' type="number" defaultValue="0" onChange={this.updateInputValue} />
                    <button className="action" onClick={() => this.addTocart(product)}>Add To Cart</button>
                  
                </td>
            </div>);
    }

}