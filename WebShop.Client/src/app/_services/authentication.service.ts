import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

// import { environment } from '../environments/environment';
import { Role, User } from '../models';
import { Router } from '@angular/router';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {

  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;
  private apiUrl = 'https://localhost:5001/API/User';

  constructor(private router: Router, private http: HttpClient) {
    // fake login durring testing
    // if (sessionStorage.getItem('currentUser') == null) {
    //   sessionStorage.setItem('currentUser', JSON.stringify({ id: 0, email: '', username: '', role: null }));
    // }
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(sessionStorage.getItem('currentUser') as string));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }

  login(email: string, password: string) {
    return this.http.post<any>(`${this.apiUrl}/authenticate`, { email, password })
      .pipe(map(user => {
        // store user details and jwt token in local storage to keep user logged in between page refreshes
        sessionStorage.setItem('currentUser', JSON.stringify(user));
        this.currentUserSubject.next(user);
        // console.log('login user',user);
        return user;
      }));
  }

  logout() {
    // remove user from local storage to log user out
    sessionStorage.removeItem('currentUser');
    // reset CurrentUserSubject, by fetching the value in sessionStorage, which is null at this point
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(sessionStorage.getItem('currentUser') as string));
    // reset CurrentUser to the resat UserSubject, as an obserable
    this.currentUser = this.currentUserSubject.asObservable();
  }
}
