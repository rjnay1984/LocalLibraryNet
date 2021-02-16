import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http.post(`${this.baseUrl}/account/authenticate`, model).pipe(
      map((response: User) => {
        const user = response;
        const token = JSON.parse(atob(user.token.split('.')[1]));
        console.log(token);
      })
    )
  }
}
