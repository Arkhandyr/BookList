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
    const token = this.getCookie("jwt");

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

  private getCookie(cookieName: string): string | undefined {
    const cookies = document.cookie.split(';'); // Split the cookie string into an array of individual cookies

    console.log(cookies);

    for (const cookie of cookies) {
      const [name, value] = cookie.trim().split('=');
      if (cookieName === name) {
        return decodeURIComponent(value); // Decode the cookie value
      }
    }
    return '';
  }
}