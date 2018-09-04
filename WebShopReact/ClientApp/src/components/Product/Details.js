import React from 'react';

export class Details extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            product: null,
            loading: true
        };

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
            </div>);
    }
}