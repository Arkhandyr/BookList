import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Badge } from '../interfaces/Badge';

@Injectable({
  providedIn: 'root'
})

export class BadgeService { 
  constructor(private client:HttpClient) { }

  public getUserBadges(username: string): Observable<Badge[]> {
    let url = `${environment.api}/badges/${username}`
    
    return this.client.get<Badge[]>(url, {
        withCredentials: true
    })
  }
}