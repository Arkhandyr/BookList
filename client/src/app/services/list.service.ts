import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../interfaces/User';
import { Profile } from '../interfaces/Profile';
import { Book } from '../interfaces/Book';

@Injectable({
  providedIn: 'root'
})

export class ListService { 
  constructor(private client:HttpClient) { }

  public addToList(listEntry: string): Observable<string> {
    let url = `${environment.api}/addToList`
    
    const headers = new HttpHeaders({
        'Content-Type': 'application/json'
    }); 
    return this.client.post<string>(url, listEntry, {
        headers: headers,
        withCredentials: true
    })
  } 

  public removeFromList(listEntry: string): Observable<string> {
    let url = `${environment.api}/removeFromList`
    
    const headers = new HttpHeaders({
        'Content-Type': 'application/json'
    }); 
    return this.client.post<string>(url, listEntry, {
        headers: headers,
        withCredentials: true
    })
  } 

  public getUserReading(username: string, page: number): Observable<Book[]> {
    let url = `${environment.api}/reading/${username}/${page}`
    
    return this.client.get<Book[]>(url, {
        withCredentials: true
    })
  }

  public getUserPlanning(username: string , page: number): Observable<Book[]> {
    let url = `${environment.api}/planning/${username}/${page}`
    
    return this.client.get<Book[]>(url, {
        withCredentials: true
    })
  }

  public getUserRead(username: string, page: number): Observable<Book[]> {
    let url = `${environment.api}/done/${username}/${page}`
    
    return this.client.get<Book[]>(url, {
        withCredentials: true
    })
  }

  public getBookStatus(bookId: string, username: string): Observable<string> {
    let url = `${environment.api}/books/${bookId}/${username}`
    
    return this.client.get<string>(url, {
        withCredentials: true
    })
  }

  public getListCount(username: string): Observable<number[]> {
    let url = `${environment.api}/bookCount/${username}`
    
    return this.client.get<number[]>(url, {
        withCredentials: true
    })
  }
}