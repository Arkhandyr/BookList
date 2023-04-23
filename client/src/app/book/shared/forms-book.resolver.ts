import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve } from "@angular/router";
import { Observable } from "rxjs";
import { Book } from "./book";
import { BookService } from "./books.service";

@Injectable()
export class FormsBookResolver implements Resolve<Book> {

  constructor(private bookService: BookService) { }

  resolve(route: ActivatedRouteSnapshot): Observable<Book> {
    return this.bookService.getBookById(route.params['id']);
  }
}