import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Category } from '../models';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  private apiUrl = 'https://localhost:5001/api/Category/';

  constructor(
    private http:HttpClient
  ) { }

  getCategories(): Observable<Category[]>{
    return this.http.get<Category[]>(this.apiUrl);
  }
}
