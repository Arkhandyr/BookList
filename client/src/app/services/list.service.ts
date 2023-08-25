import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../interfaces/User';
import { Profile } from '../interfaces/Profile';

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
}