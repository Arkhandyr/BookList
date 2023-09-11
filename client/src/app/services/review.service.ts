import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Review } from '../interfaces/Review';

@Injectable({
  providedIn: 'root'
})

export class ReviewService { 
  constructor(private client:HttpClient) { }

  public addReview(reviewEntry: string): Observable<string> {
    let url = `${environment.api}/books/addReview`
    
    const headers = new HttpHeaders({
        'Content-Type': 'application/json'
    }); 
    return this.client.post<string>(url, reviewEntry, {
        headers: headers,
        withCredentials: true
    })
  } 

  public removeFromList(reviewEntry: string): Observable<string> {
    let url = `${environment.api}/books/deleteReview`
    
    const headers = new HttpHeaders({
        'Content-Type': 'application/json'
    }); 
    return this.client.post<string>(url, reviewEntry, {
        headers: headers,
        withCredentials: true
    })
  } 

  public getBookReviews(bookId: string): Observable<Review[]> {
    let url = `${environment.api}/reviews/${bookId}`
    
    return this.client.get<Review[]>(url, {
        withCredentials: true
    })
  }

  public likeReview(likeEntry: string): Observable<string> {
    let url = `${environment.api}/reviews/likeReview`
    
    const headers = new HttpHeaders({
        'Content-Type': 'application/json'
    }); 
    return this.client.post<string>(url, likeEntry, {
        headers: headers,
        withCredentials: true
    })
  } 

  public dislikeReview(likeEntry: string): Observable<string> {
    let url = `${environment.api}/reviews/dislikeReview`
    
    const headers = new HttpHeaders({
        'Content-Type': 'application/json'
    }); 
    return this.client.post<string>(url, likeEntry, {
        headers: headers,
        withCredentials: true
    })
  } 
}