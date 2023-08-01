import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../interfaces/User';

@Injectable({
  providedIn: 'root'
})

export class UserService { 
  constructor(private client:HttpClient) { }
  
  public register(user: User): Observable<string> {
      let url = `${environment.api}/user/register`

      return this.client.post<string>(url, user)
  }

  public login(user: any): Observable<string> {
    let url = `${environment.api}/user/login`

    return this.client.post<string>(url, user, {
      withCredentials: true
    })
  } 
}