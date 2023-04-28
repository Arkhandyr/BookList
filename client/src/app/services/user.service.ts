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
    /*return this.httpClient.post<any>(userApi + "/login", user).pipe(
      tap((resposta) => {
        if(!resposta.success) return;
        localStorage.setItem('token', btoa(JSON.stringify(resposta['token'])));
        localStorage.setItem('user', btoa(JSON.stringify(resposta['user'])));
        this.router.navigate(['']);
      }));*/
      return this.mockLoggedUser(user).pipe(tap((response) => {
        if(!response.success) return;
        localStorage.setItem('token', btoa(JSON.stringify("TokenQueSeriaGeradoPelaAPI")));
        localStorage.setItem('user', btoa(JSON.stringify(user)));
        this.router.navigate(['']);
      }));
  }

  private mockLoggedUser(user: IUser): Observable<any> {
    var retornoMock: any = [];
    if(user.email === "arkhandyr@gmail.com" && user.senha == "123"){
      retornoMock.success = true;
      retornoMock.user = user;
      retornoMock.token = "TokenQueSeriaGeradoPelaAPI";
      return of(retornoMock);
    }
    retornoMock.success = false;
    retornoMock.user = user;
    return of(retornoMock);
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

  get getLoggedUserToken(): string {  
    return localStorage.getItem('token')
      ? JSON.parse(Buffer.from(localStorage.getItem('token')!, 'base64').toString('binary'))
      : null;
  }

  get logged(): boolean {
    return localStorage.getItem('token') ? true : false;
  }
}