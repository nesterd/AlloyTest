import React, { Component } from 'react';
import CustomersEditModal from './CustomersEditModal';
import * as ApiConsts from '../../appConsts/ApiConsts';

export class Customers extends Component {
  static displayName = Customers.name;

  constructor(props) {
    super(props);
    this.state = 
    { 
        customers: [], 
        loading: true,
        editModalOpen: false, 
        intervals: []
    };
    this.openEditModal = this.openEditModal.bind(this);
    this.closeEditModal = this.closeEditModal.bind(this);
    this.onCreateSuccess = this.onCreateSuccess.bind(this);
    this.renderCustomersTable = this.renderCustomersTable.bind(this);
    this.startPopulateData = this.startPopulateData.bind(this);
    this.stopPopulateData = this.stopPopulateData.bind(this); 
  }

  componentDidMount() {
    window.onfocus = this.startPopulateData;
    window.onblur = this.stopPopulateData;
    

    this.startPopulateData();
  }

  renderCustomersTable(customers) {
    return (
         <table className='table table-hover table-sm' aria-labelledby="tabelLabel">
            <thead>
              <tr>
                  <th>Имя</th>
                  <th>Город</th>
                  <th>Сумма</th>
                  <th></th>
              </tr>
            </thead>
            <tbody>
              {customers.map(customer =>
                  <tr key={customer.name}>
                  <td>{customer.name}</td>
                  <td>{customer.city}</td>
                  <td>{customer.amount} р.</td>
                  <td><button className="btn btn-link float-right" onClick={this.deleteCustomer.bind(this, customer.name)}>удалить</button></td>
                  </tr>
              )}
            </tbody>
          </table>
    );
  }

  render() {
    const editModalOpen = this.state.editModalOpen;
    const contents = this.renderCustomersTable(this.state.customers);
    return (
      <div>
        <h2 id="tabelLabel" >Клиенты</h2>
        <button className="btn btn-success btn-sm float-right mb-1" type="button" onClick={this.openEditModal}>Добавить</button>
        {contents}
        {editModalOpen ?
        <CustomersEditModal onCreateSuccess={this.onCreateSuccess} onClose={this.closeEditModal}/>
        : null}
      </div>
    );
  }

  startPopulateData(){
    this.setState(prevState => {
      const interval = setInterval(() => this.populateData(), 1000);
      console.log(`интервал ${interval} - добавлен`)
      console.log("--   Данные начали приходить")
      return {intervals: prevState.intervals.concat([interval])}
    });
  }

  stopPopulateData(){
    this.setState(prevState => {
      for(const interval of prevState.intervals){      
        clearInterval(interval);
        console.log(`интервал ${interval} - удален`);
      }
      console.log("--   Данные ЗАКОНЧИЛИ приходить")
      return {intervals: []}
    });
  }

  openEditModal(){
      this.setState({editModalOpen: true});
  }

  closeEditModal(){
    this.setState({editModalOpen: false});
  }

  onCreateSuccess(){
      this.setState({editModalOpen: false}, () => this.populateData())
  }

  async deleteCustomer(customerName){
    const response = await fetch(`${ApiConsts.CUSTOMERS}/${customerName}`, {
      method: ApiConsts.DELETE,
      headers: ApiConsts.JSON_HEADER,
    })
    if(response.ok){
      alert("Клиент успешно удален!");
      this.setState(prevState => {
        return {customers: prevState.customers.filter(x => x.name != customerName)}
      })
    }
    else{
      alert("При удалении что то пошло не так....");
    }
  }

  async populateData() {
    this.setState({loading: true}, async () =>  {
      const response = await fetch(ApiConsts.CUSTOMERS);
      const data = await response.json();
      console.log("данные получены")
      this.setState({ customers: data, loading: false });
    });
  }
}
