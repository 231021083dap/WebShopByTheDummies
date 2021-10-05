import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Customer } from '../models';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  private apiUrl = 'https://localhost:5001/API/User/Customer';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };
  constructor(
    private http:HttpClient
  ) { }

  getCustomers(): Observable<Customer[]>{
    return this.http.get<Customer[]>(this.apiUrl);
  }

  getCustomerById(customerId : number): Observable<Customer>{
    return this.http.get<Customer>(`${this.apiUrl}/${customerId}`);
  }

  addCustomer(customer: Customer): Observable<Customer> {
    return this.http.post<Customer>(this.apiUrl, customer, this.httpOptions);
  }

  updateCustomer(customerId:number, customer:Customer) : Observable<Customer>{
    return this.http.put<Customer>(`${this.apiUrl}/${customerId}`, customer, this.httpOptions);
  }

  deleteCustomer(customerId:number) : Observable<boolean>{
    return this.http.delete<boolean>(`${this.apiUrl}/${customerId}`, this.httpOptions);
  }
}
