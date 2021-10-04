import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private apiUrl = 'https://localhost:5001/API/Product';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };
  constructor(
    private http:HttpClient
  ) { }

  getProducts(): Observable<Product[]>{
    return this.http.get<Product[]>(this.apiUrl);
  }
  
  getItemById(itemId : number): Observable<Product>{
    return this.http.get<Product>(`${this.apiUrl}/${itemId}`);
  }

  addProduct(product: Product): Observable<Product> {
    return this.http.post<Product>(this.apiUrl, product, this.httpOptions);
  }

  updateProduct(productId:number, product:Product) : Observable<Product>{
    return this.http.put<Product>(`${this.apiUrl}/${productId}`, product, this.httpOptions);
  }

  deleteProduct(productId:number) : Observable<boolean>{
    return this.http.delete<boolean>(`${this.apiUrl}/${productId}`, this.httpOptions);
  }
}
