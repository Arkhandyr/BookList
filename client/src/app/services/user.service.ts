import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../interfaces/User';
import { Profile } from '../interfaces/Profile';

@Injectable({
  providedIn: 'root'
})

export class UserService { 
  constructor(private client:HttpClient) { }
  
  public register(user: User): Observable<string> {
      let url = `${environment.api}/register`

      return this.client.post<string>(url, user)
  }

  public login(user: any): Observable<string> {
    let url = `${environment.api}/login`

    return this.client.post<string>(url, user, {
      withCredentials: true
    })
  } 

  public getUser(): Observable<Profile> {
    let url = `${environment.api}/user`

    return this.client.get<Profile>(url, {
      withCredentials: true
    })
  }

  public getByUsername(username: string): Observable<Profile> {
    let url = `${environment.api}/profile/${username}`

    return this.client.get<Profile>(url)
  }

  public logout(): Observable<string> {
    let url = `${environment.api}/logout`

    return this.client.post<string>(url, {}, {
      withCredentials: true
    })
  } 
}