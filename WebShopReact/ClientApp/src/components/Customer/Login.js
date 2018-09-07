import React, { Component } from 'react'
import { CustomerCreateEdit } from './RegisterEdit'
import Modal from 'react-modal';
Modal.setAppElement('#root')
export class Login extends Component {
    constructor(props) {
        super(props);
        this.state = {
            email: '',
            password: '',
            error: false,
            showRegister: false,
            token: ''
        }
        this.emailChanged = this.emailChanged.bind(this);
        this.passChanged = this.passChanged.bind(this);
    }
    renderPopup() {
        if (!this.state.showRegister)
            return
        return (<Modal
            isOpen={true}
            contentLabel="Crawl"
            onRequestClose={this.closeModal}>
            <button onClick={this.closeModal} className="action" title="close">X</button>
            {this.renderPopupContent()}
        </Modal>);
    }
    renderPopupContent() {
        if (this.state.showRegister) {
            console.log(this.state.showRegister);
            return <CustomerCreateEdit id={null} dbaction="create"
                onSave={this.handlePopupSave.bind(this)} />
        }
    }
    handleSave(e) {
        e.preventDefault()
        if (this.state.password != '' && this.state.email != '') {
            let form = Element = document.querySelector('#frmLogin')
            fetch('/customers/authenticate',
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
                        this.handleToken();
                    }
                })
        }
    }
    handlePopupSave(success) {
        if (success)
            this.setState({ showRegister: false })
    }
    register() {
        this.setState({ showRegister: true})
    }
    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : (this.renderButton());
        let contents2 = this.state.loading
            ? <p><em>Loading...</em></p>
            : (this.renderForm());
        return (<div>
            <h1>{this.props.dbaction == "edit" ? "Edit User" : "Login"}</h1>
            
            {contents2}
            <br /> <br />
            {contents}{this.renderPopup()}
            <br />
            <div>
                {this.state.error == true ? <p> The Email or password are incorrect </p> : null}
            </div>
        </div>)
    }
    handleToken() {
        console.log("token " + this.state.token);
    }
    emailChanged(evt) {
        this.setState({
            email: evt.target.value
        })
    }
    passChanged(evt) {
        this.setState({
            password: evt.target.value
        })
    }
    renderForm() {
        return (<form id='frmLogin'>
            <label>Email</label><br />
            <input id='Email' name='Email' type="text" defaultValue='' onChange={this.emailChanged} />
            <br /> <br />
            <label>Password</label><br />
            <input id='Password' name='Password' type="password" defaultValue='' onChange={this.passChanged} />
            <br /> <br />
            <button onClick={this.handleSave.bind(this)}>submit</button>
            
        </form>
           
        )
    }
    renderButton() { return (<button className="action" onClick={() => this.register()}>Register</button>) }
    closeModal() {
        this.setState({ showRegister: false })
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