import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

import { User } from '../models/user';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new ReplaySubject<User>(1);
  // eslint-disable-next-line @typescript-eslint/member-ordering
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) {}

  login(model: any) {
    return this.http.post(`${this.baseUrl}/account/authenticate`, model).pipe(
      map((response: User) => {
        if (response) {
          this.setCurrentUser(response);
        }
      })
    );
  }

  register(model: any) {
    return this.http.post(`${this.baseUrl}/account/register`, model).pipe(
      map((response: User) => {
        if (response) {
          this.setCurrentUser(response);
        }
      })
    );
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }

  setCurrentUser(user: User) {
    const token = this.getDecodedToken(user.token);
    const roles = token.role;
    user.username = token.unique_name;
    user.roles = [];
    // eslint-disable-next-line @typescript-eslint/no-unused-expressions
    Array.isArray(roles) ? (user.roles = roles) : user.roles.push(roles);
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  getDecodedToken(token: string) {
    return JSON.parse(atob(token.split('.')[1]));
  }
}
