import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs/internal/Observable";
import { environment } from "src/environments/environment";
import { Book } from "../interfaces/Book";

@Injectable({
    providedIn: 'root'
  })

export class BookService { 
    constructor(private client:HttpClient) { }
    
    public getTrendingBooks(page: number): Observable<Book[]> {
        let url = `${environment.api}/catalog/trending/${page}`

        return this.client.get<Book[]>(url)
    }

    public getTopBooks(page: number): Observable<Book[]> {
        let url = `${environment.api}/catalog/top/${page}`

        return this.client.get<Book[]>(url)
    }

    public getBookById(id: string): Observable<Book> {
        let url = `${environment.api}/book/${id}`

        return this.client.get<Book>(url)
    }

    public addBook(book: Book): Observable<string> {
        let url = `${environment.api}/add`

        return this.client.post<string>(url, book)
    }

    public updateBook(book: Book): Observable<any> {
        let url = `${environment.api}/update`

        return this.client.put(url, book)
    }

    public deleteBook(id: string): Observable<any> {
        let url = `${environment.api}/books/${id}`

        return this.client.delete(url)
    }

    public getBookByAuthor(name: string): Observable<Book[]> {
        let url = `${environment.api}/booksByAuthor/${name}`

        return this.client.get<Book[]>(url)
    }
}