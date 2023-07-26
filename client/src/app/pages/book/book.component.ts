import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Book } from 'src/app/interfaces/IBook';
import { BookService } from 'src/app/services/books.service';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.scss']
})
export class BookComponent implements OnInit {
  bookId: string
  private sub: any;

  constructor(
    private route: ActivatedRoute,
    private bookService:BookService) { }

  ngOnInit() {
    this.sub = this.route.params.subscribe(params => {
       this.bookId = params['id'];

       this.bookService.getBookById(this.bookId).subscribe(x => this.book = x);
    });
  }

  public book:Book;
}
