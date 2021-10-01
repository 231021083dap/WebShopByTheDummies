import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private apiUrl = 'https://localhost:5001/API/User';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  constructor(
    private http:HttpClient
  ) { }

  getUsers(): Observable<User[]>{
    return this.http.get<User[]>(this.apiUrl);
  }

  getUserById(userId : number): Observable<User>{
    return this.http.get<User>(`${this.apiUrl}/${userId}`);
  }

  addUser(user: User): Observable<User> {
    return this.http.post<User>(this.apiUrl, user, this.httpOptions);
  }

  updateUser(userId:number, product:User) : Observable<User>{
    return this.http.put<User>(`${this.apiUrl}/${userId}`, product, this.httpOptions);
  }

  deleteUser(userId:number) : Observable<boolean>{
    return this.http.delete<boolean>(`${this.apiUrl}/${userId}`, this.httpOptions);
  }
}
