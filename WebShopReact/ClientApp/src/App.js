import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Products } from './components/Product/Index';
import { Cart } from './components/Cart/Index';

export default class App extends Component {
  displayName = App.name

  render() {
    return (
      <Layout>
            <Route exact path='/' component={Products} />
            <Route exact path='Cart/' component={Cart} />
      </Layout>
    );
  }
}
