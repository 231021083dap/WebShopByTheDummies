import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Category } from '../models';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  private apiUrl = 'https://localhost:5001/API/Category';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };
  constructor(
    private http:HttpClient
  ) { }

  getCategories(): Observable<Category[]>{
    return this.http.get<Category[]>(`${this.apiUrl}/`);
  }
  getCategory(categoryId: number): Observable<Category> {
    return this.http.get<Category>(`${this.apiUrl}/${categoryId}`);
  }
  addCategory(category: Category): Observable<Category> {
    return this.http.post<Category>(this.apiUrl, category, this.httpOptions);
  }
  updateCategory(categoryId:number, category:Category) : Observable<Category>{
    return this.http.put<Category>(`${this.apiUrl}/${categoryId}`, category, this.httpOptions);
  }
  deleteCategory(categoryId:number) : Observable<boolean>{
    return this.http.delete<boolean>(`${this.apiUrl}/${categoryId}`, this.httpOptions);
  }
}
