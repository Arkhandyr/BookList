import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Author } from '../interfaces/Author';
import { Book } from '../interfaces/Book';
import { User } from '../interfaces/User';

@Injectable({
  providedIn: 'root'
})

export class SearchService { 
    constructor(private client:HttpClient) { }

    public filterBooks(filter: string, page: number): Observable<Book[]> {
    let url = `${environment.api}/searchBooks/${filter}/${page}`

    return this.client.get<Book[]>(url)
    }


    public filterAuthors(filter: string, page: number): Observable<Author[]> {
        let url = `${environment.api}/searchAuthors/${filter}/${page}`

        return this.client.get<Author[]>(url)
    }


    public filterUsers(filter: string, page: number): Observable<User[]> {
        let url = `${environment.api}/searchUsers/${filter}/${page}`

        return this.client.get<User[]>(url)
    }
}