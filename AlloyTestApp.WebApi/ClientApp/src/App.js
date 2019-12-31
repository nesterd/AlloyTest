import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/common/Layout';
import { Home } from './components/common/Home';
import { Customers } from './components/customers/Customers';
import { SummaryByCityReport } from './components/reports/SummaryByCityReport';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/customers' component={Customers} />
        <Route path='/summary-by-city-report' component={SummaryByCityReport} />
      </Layout>
    );
  }
}
