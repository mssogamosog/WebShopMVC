import React, { Component } from 'react'

export class CreateEdit extends Component {
    constructor(props) {
        super(props);
        if (this.props.dbaction == "edit") {
            this.state = { product: null, loading: true, save: false }
            fetch('Products/' + this.props.id, { method: 'get' })
                .then(response => response.json())
                .then(data => {
                    this.setState({ product: data, loading: false })
                })
        } else {
            this.state = { product: null, loading: false, save: false }
        }
    }

    handleSave(e) {
        e.preventDefault()
        let meth = (this.props.dbaction == "edit" ? "put" : "post")
        let form = Element = document.querySelector('#frmCreateEdit')
        let url = (this.props.dbaction == "edit" ? 'Products/' + document.getElementById('Id').value : 'Products/')

        fetch(url,
            {
                method: meth,
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(this.formToJson(form))
            })
            .then(data => {
                this.setState({ save: false });
                this.props.onSave(true);
            })
    }
    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderForm(this.state.product);
        return <div>
            <h1>{this.props.dbaction == "edit" ? "Editor Product" : "Create Product"}</h1>
            {contents}
        </div>
    }

    renderForm(item) {
        console.log("this.props.dbaction " + this.props.dbaction)
        if (this.props.dbaction != "edit")
            item = { Name: '', Quantity: '', Price: 0 }
        return <form id='frmCreateEdit'>
            {this.props.dbaction == 'edit' ? <input id='productId' name='productId' type='hidden' value={item.productId} />
                : null}
            <label>Name</label>
            <input id='Name' name='Name' type="text" defaultValue={item.Name != null ? (item.Name + '') : ''} />
            <label>Quantity</label>
            <input id='Quantity' name='Quantity' type="text" defaultValue={item.Quantity != null ? (item.Quantity + '') : ''} />
            <label>Price</label>
            <input id='Price' name='Price' type="text" defaultValue={item.Price != null ? (item.Price + '') : ''} />
            <button onClick={this.handleSave.bind(this)}>submit</button>
        </form>
    }

    isValidElement = element => {
        return element.name && element.value;
    };

    isValidValue = element => {
        return (['checkbox', 'radio'].indexOf(element.type) == -1 || element.checked);
    };

    formToJson = elements => [].reduce.call(elements, (data, element) => {
        console.log('formToJson()', element)
        if (this.isValidElement(element) && this.isValidValue(element)) {
            data[element.name] = element.value;
        }
        return data;
    }, {});
}