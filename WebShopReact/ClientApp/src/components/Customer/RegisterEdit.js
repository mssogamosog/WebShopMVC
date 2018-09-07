import React, { Component } from 'react'
export class CustomerCreateEdit extends Component {
    constructor(props) {
        super(props);
        if (this.props.dbaction == "edit") {
            this.state = {
                customer: null,
                loading: true,
            }
            fetch('/customers/' + this.props.CustomerId, {
                method: 'get',
            })
                .then(response => response.json())
                .then(data => {
                    this.setState({ customer: data, loading: false })
                })
        } else {
            this.state = { customer: null, loading: false }
        }
    }

    handleSave(e) {
        e.preventDefault()
        let form = Element = document.querySelector('#frmCreateEdit')
        fetch('/customers/',
            {
                method: 'post',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(this.formToJson(form))
            })
            .then(response => response.json())
            .then(data => {
                if (data.error != null) {
                    this.setState({
                        error: true
                    })
                }
                else {
                    this.setState({
                        token: data.token
                    })
                    
                }
            })
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderForm(this.state.customer);
        return (<div>
            <h1>{this.props.dbaction == "edit" ? "Edit User" : "Create User"}</h1>
            {contents}
        </div>)
    }


    renderPassword() {
        return (<div>
            <label>Password</label> <br />
            <input id='Password' name='Password' type="password" defaultValue='' />
            < br /> <br />
        </div>
        )
    }

    renderForm(item) {
        let content = null;
        if (this.props.dbaction != "edit") {
            item = { FirstName: '', LastName: '', Email: ''}
            content = this.renderPassword();
        }
        return <form id='frmCreateEdit'>
            {this.props.dbaction == 'edit' ? <input id='CustomerId' name='CustomerId' type='hidden' value={item.CustomerId} />
                : null}
            <label>First Name</label><br />
            <input id='FirstName' name='FirstName' type="text" defaultValue={item.FirstName != null ? (item.FirstName + '') : ''} />
            <br /> <br />
            <label>Last Name</label><br />
            <input id='LastName' name='LastName' type="text" defaultValue={item.FastName != null ? (item.LastName + '') : ''} />
            <br /> <br />
            <label>Email</label><br />
            <input id='Email' name='Email' type="text" defaultValue={item.Email != null ? (item.Email + '') : ''} />
            <br /> <br />
            {content}
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
        if (this.isValidElement(element) && this.isValidValue(element)) {
            data[element.name] = element.value;
        }
        return data;
    }, {});
}