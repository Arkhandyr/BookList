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

export class AuthorService { 
  constructor(private client:HttpClient) { }
  
  public getById(id: string): Observable<Author> {
    let url = `${environment.api}/author/${id}`

    return this.client.get<Author>(url)
  }
}