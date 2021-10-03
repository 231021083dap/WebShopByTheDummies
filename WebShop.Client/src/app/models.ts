//import { CurrencyPipe } from "@angular/common";
//import { EmailValidator } from "@angular/forms";

export interface Category{
  id : number;
  name : string;
  picture : string;
  products? : Product[];
}

export interface Product{
  id : number;
  categoryId: number;
  name : string;
  price : number;          //CurrencyPipe
  description : string;
  images? : Image[];
}

export interface Image{
  id : number;
  path : string;
  productId: number;
}

export interface User {
  id : number;
  email : string;       //EmailValidator
  role? : Role;
  token? : string;
}

export enum Role {
  User = 'User',
  Admin = 'Admin'
}
export interface Customer {
  id : number;
  userId : number;
  // user : User[];
  firstName : string;
  middleName : string;
  lastName : string;
  address : Address[];
}
export interface Address{
  id : number;
  customerId : number;
  streetName : string;
  streetNumber : number;
  floor : string;
  zipcode : number;
  country : string;
}
export interface Order{
  id : number;
  shippingAddressId : number;
  billingAddressId : number;
  orderItems : OrderItem[];
}
export interface OrderItem{
  id : number;
  orderId : number;
  productId : number;
  amount : number;
  currentPrice : number;    //CurrencyPipe
}

export interface CartProduct{
  productId: number;
  amount: number;
  
}