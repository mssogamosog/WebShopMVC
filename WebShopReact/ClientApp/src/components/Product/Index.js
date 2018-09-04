import React, { Component } from 'react';
import Modal from 'react-modal';

Modal.setAppElement('#root')
export class Products extends Component
{
    constructor(props) {
        super(props);
        this.state = {
            products: [],
            loading: true,
            showCreate: false,
            showDetails: false,
            showUpdate: false,
            showModal: true,
            activeId: 0
        };
        fetch('/Products')
            .then(response => response.json())
            .then(data => {
                this.setState({
                    products: data,
                    loading: false,
                    showCreate: false,
                    showDetails: false,
                    showUpdate: false,
                    showModal: true,
                    activeId: 0
                });
            });
        this.closeModal = this.closeModal.bind(this);

        renderTable(products) {
            return (<table className='table'>
                <thead>
                    <tr>
                        <th></th>
                        <th>Id</th>
                        <th>Name</th>
                        <th>Quantity</th>
                        <th>Price</th>    
                    </tr>
                </thead>
                <tbody>
                    {products.map(item =>
                        <tr key={item.productid}>
                            <td>{item.productid}</td>
                            <td>{item.name}</td>
                            <td>{item.quantity}</td>
                            <td>{item.price}</td>                           
                        </tr>
                    )}
                </tbody>
            </table>);
        }

        renderPopup() {
     
            return (<Modal
                isOpen={true}
                contentLabel="Crawl"
                onRequestClose={this.closeModal}>
                <button onClick={this.closeModal} className="action" title="close">X</button>
                {this.renderPopupContent()}
            </Modal>);
        }
        render() {
            let contents = this.state.loading
                ? <p><em>Loading...</em></p>
                : this.renderTable(this.state.products);

            return (<div>
                <h1>Product</h1>
                {contents}
                {this.renderPopup()}
            </div>);

        }
    }
}