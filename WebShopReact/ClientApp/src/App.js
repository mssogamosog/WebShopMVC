import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Products } from './components/Product/Index';
import { CartProducts } from './components/CartProducts/Index';
//import { Customer } from './components/User/Details';
import { Login } from './components/Customer/Login';

export default class App extends Component {
  displayName = App.name

  render() {
    return (
      <Layout>
            <Route exact path='/' component={Products} />
            <Route exact path='/CartProducts' component={CartProducts} />
            
            <Route exact path='/Login' component={Login} />
      </Layout>
    );
  }
}
