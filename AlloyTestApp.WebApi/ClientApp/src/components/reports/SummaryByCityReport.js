import React, { Component } from 'react';
import * as ApiConsts from '../../appConsts/ApiConsts';

export class SummaryByCityReport extends Component {
  static displayName = SummaryByCityReport.name;

  constructor(props) {
    super(props);
    this.state = { report: [], loading: true };
  }

  componentDidMount() {
    this.populateData();
  }

  static renderCustomersTable(report) {
    return (
      <table className='table table-hover table-sm' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Город</th>
            <th>Сумма</th>
          </tr>
        </thead>
        <tbody>
          {report.map((item, i) =>
            <tr key={i}>
              <td>{item.cityName}</td>
              <td>{item.summaryAmount} р.</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : SummaryByCityReport.renderCustomersTable(this.state.report);

    return (
      <div>
        <h2 id="tabelLabel" >Города</h2>
        {contents}
      </div>
    );
  }

  async populateData() {
    const response = await fetch(ApiConsts.SUMMARY_BY_CITY_REPORT);
    const data = await response.json();
    this.setState({ report: data, loading: false });
  }
}
