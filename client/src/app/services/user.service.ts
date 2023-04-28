import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IUser } from '../interfaces/IUser';

@Injectable({
  providedIn: 'root'
})

export class UserService {
constructor(private httpClient: HttpClient,
            private router: Router) { }
  login(user: IUser) : Observable<any> {
    return this.httpClient.post<any>(environment.api + "/login", user).pipe(
      tap((response) => {
        if(!response.success) return;

        localStorage.setItem('token', Buffer.from(JSON.stringify(response['token'])).toString('base64'));
        localStorage.setItem('user', Buffer.from(JSON.stringify(response['user'])).toString('base64'));
        this.router.navigate(['']);
      }));
  }

  logout() {
      localStorage.clear();
      this.router.navigate(['login']);
  }

  get GetLoggedUser(): IUser {
    return localStorage.getItem('user')
      ? JSON.parse(Buffer.from(localStorage.getItem('user')!, 'base64').toString('binary'))
      : null;
  }

  get getLoggedUserId(): string {
    return localStorage.getItem('user')
      ? (JSON.parse(Buffer.from(localStorage.getItem('user')!, 'base64').toString('binary')) as IUser).id
      : "";
  }

  get getUserToken(): string {  
    return localStorage.getItem('token')
      ? JSON.parse(Buffer.from(localStorage.getItem('token')!, 'base64').toString('binary'))
      : null;
  }

  get logged(): boolean {
    return localStorage.getItem('token') ? true : false;
  }
}