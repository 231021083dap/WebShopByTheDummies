import { Component, OnInit } from '@angular/core';
import { Customer } from 'src/app/models';
import { User } from 'src/app/models';
import { CustomerService } from 'src/app/_services/customer.service';

@Component({
  selector: 'app-admincustomer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss']
})
export class AdminCustomerComponent implements OnInit {

  customers: Customer[] = [];
  customer: Customer = { id: 0, userId: 0, firstName: '', middleName: '', lastName: '', address: [], user: { id: 0, email: '' } }

  constructor(
    private customerService: CustomerService
  ) { }

  ngOnInit(): void {
    this.getCustomers();
  }
  getCustomers(): void {
    this.customerService.getCustomers()
    .subscribe(c => this.customers = c);
  }
  edit(customer: Customer): void {
    this.customer = customer;
  }
  delete(customer: Customer): void {
    if (confirm('Er du sikker på at du vil slette denne bruger?')){
      this.customerService.deleteCustomer(customer.id)
      .subscribe(() => {
        this.getCustomers();
      })
    }
  }
  cancel(): void {
    this.customer = { id: 0, userId: 0, firstName: '', middleName: '', lastName: '', address: [], user: { id: 0, email: '' } }
  }
  save(): void{
    if (this.customer.id == 0){
      this.customerService.addCustomer(this.customer)
      .subscribe(c => {
        this.customers.push(c)
        this.customer = { id: 0, userId: 0, firstName: '', middleName: '', lastName: '', address: [], user: { id: 0, email: '' } }
      });
    }else {
      console.log(this.customer);
      this.customerService.updateCustomer(this.customer.id, this.customer)
      .subscribe(() => {
        this.customer = { id: 0, userId: 0, firstName: '', middleName: '', lastName: '', address: [], user: { id: 0, email: '' } }
      })
    }
  }
}
