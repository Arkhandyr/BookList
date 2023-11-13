import { HttpClient, HttpHeaders } from '@angular/common/http';
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

  public follow(followEntry: string): Observable<any> {
    let url = `${environment.api}/follow`

    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    }); 
    return this.client.post<string>(url, followEntry, {
      headers: headers,
      withCredentials: true
    })
  }

  public unfollow(followEntry: string): Observable<any> {
    let url = `${environment.api}/unfollow`

    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    }); 
    return this.client.post<string>(url, followEntry, {
      headers: headers,
      withCredentials: true
    })
  }

  getFollowStatus(user: string, user2: string): Observable<boolean> {
    let url = `${environment.api}/followStatus/${user}/${user2}`

    return this.client.get<boolean>(url, {
      withCredentials: true
    })
  } 
}