import React, { Component } from 'react';
import * as ApiConsts from '../../appConsts/ApiConsts';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Modal } from 'react-bootstrap';
import NumericInput from 'react-numeric-input';
import Errors from '../common/Errors';
import isNullOrWhiteSpace from '../../utils/isNullOrWhiteSpace';

export default class CustomersEditModal extends Component {

    static displayName = CustomersEditModal.name;


    constructor(props) {
        super(props);
        this.state = 
        { 
            loading: false,
            name: null,
            city: null,
            amount: null,
            errors: []
        };
        this.onChangeName = this.onChangeName.bind(this);
        this.onChangeCity = this.onChangeCity.bind(this);
        this.onChangeAmount = this.onChangeAmount.bind(this);
        this.createCustomer = this.createCustomer.bind(this);
    }

    render() {
        const amount = this.state.amount;
        const errors = this.state.errors;
        const loading = this.state.loading;
        return (
            <Modal show={true} onHide={this.props.onClose} animation={false}>
                    <Modal.Header closeButton>
                    <Modal.Title>My Transactions</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <Errors errors={errors}/>
                        <form>
                            <div className="form-group row">
                                <label  className="col-sm-2 col-form-label">Имя клиента</label>
                                <div className="col-sm-10">
                                    <input type="text" className="form-control"  placeholder="имя" onChange={this.onChangeName} />
                                </div>
                            </div>
                            <div className="form-group row">
                                    <label  className="col-sm-2 col-form-label">Название города</label>
                                    <div className="col-sm-10">
                                        <input type="text" className="form-control"  placeholder="город" onChange={this.onChangeCity} />
                                    </div>
                                </div>
                            <div className="form-group row">
                                <label htmlFor="inputPassword" className="col-sm-2 col-form-label">Сумма</label>
                                <div className="col-md-4 col-sm-8">
                                    <NumericInput className="form-control" precision={2} value={amount} step={0.1} format={num => num + " p."} onChange={this.onChangeAmount}/>
                                </div>
                            </div>                
                        </form>
                    </Modal.Body>
                    <Modal.Footer>
                        <button type="button" className="btn btn-primary" disabled={loading} onClick={this.createCustomer}>{loading ? "Загрузка..." : "Добавить"}</button>
                        <button type="button" className="btn btn-secondary" onClick={this.props.onClose}>Отмена</button>
                    </Modal.Footer>
                </Modal>
        );
    }

    onChangeName(e){
        this.setState({name: e.target.value})
    }

    onChangeCity(e){
        this.setState({city: e.target.value})
    }

    onChangeAmount(amount){
        this.setState({amount})
    }  


    async createCustomer(){
        this.setState({loading: true});
        const errors = [];
        const name = this.state.name;
        const city = this.state.city;
        const amount = this.state.amount;
        if(isNullOrWhiteSpace(name)){
            errors.push("Необходимо указать имя клиента.")
        } 
        if(isNullOrWhiteSpace(city)){
            errors.push("Необходимо указать название города.")
        } 
        if(!amount || amount == 0){
            errors.push("Сумма не должна быть равна 0.")
        } 
        if(errors.length != 0){
            this.setState({errors, loading: false});
            return;
        }
        const customer = {name: this.state.name, city: this.state.city, amount: this.state.amount};
        const response = await fetch(ApiConsts.CUSTOMERS, {
            method: ApiConsts.POST,
            headers: ApiConsts.JSON_HEADER,
            body: JSON.stringify(customer)
        });
        if(response.ok){
            this.props.onCreateSuccess();
        }
        else{
            const errorsData = await response.json();
            const errors = [];
            Object.entries(errorsData).map(x => x[1]).forEach(x => errors.push(...x))
            this.setState({errors: errors, loading: false})
        }
    }
}
