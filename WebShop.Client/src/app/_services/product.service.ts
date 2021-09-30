import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import {Observable } from 'rxjs';
import { Product } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private apiUrl = 'https://localhost:5001/api/product';

  constructor(
    private http:HttpClient
  ) { }

  getProducts(): Observable<Product[]>{
    return this.http.get<Product[]>(this.apiUrl);
  }
}
