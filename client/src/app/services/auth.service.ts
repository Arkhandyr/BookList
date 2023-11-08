import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../interfaces/User';
import { Profile } from '../interfaces/Profile';
import { Author } from '../interfaces/Author';

@Injectable({
  providedIn: 'root'
})

export class AuthService { 
  constructor(private client:HttpClient) { }
  
  public isAuthenticated(): boolean {
    let url = `${environment.api}/token`

    let token = this.client.post<string>(url, {}, {
      withCredentials: true
    })

    return token ? true : false;
  }

  public login(user: any): Observable<string> {
    let url = `${environment.api}/login`

    return this.client.post<string>(url, user, {
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