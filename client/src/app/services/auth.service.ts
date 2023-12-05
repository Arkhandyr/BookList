import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Profile } from '../interfaces/Profile';

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  constructor(private client:HttpClient) { }
  
  loggedUser = localStorage.getItem('loggedUser');

  setLoggedUser(loggedUser: string) {
    localStorage.setItem('loggedUser', loggedUser);
  }

  getLoggedUser() {
    return this.loggedUser;
  }

  public isAuthenticated(): boolean {
    let url = `${environment.api}/token`

    let token = this.client.post<string>(url, {}, {
      withCredentials: true
    })

    return token ? true : false;
  }

  public login(user: any): Observable<Profile> {
    let url = `${environment.api}/login`

    return this.client.post<Profile>(url, user, {
      withCredentials: true
    })
  } 

  public logout(): Observable<string> {
    let url = `${environment.api}/logout`

    return this.client.post<string>(url, {}, {
      withCredentials: true
    })
  } 
}