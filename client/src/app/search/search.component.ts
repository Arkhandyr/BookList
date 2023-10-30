import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BookService } from '../services/books.service';
import { Book } from '../interfaces/Book';
import { FadeInOut } from 'src/animation';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss'],
  animations: [FadeInOut(300, 300, false)]
})
export class SearchComponent implements OnInit {
  searchQuery: string = "";
  books: Book[];
  currentBookPage: number = 1

  constructor(
    private route: ActivatedRoute, 
    private bookService: BookService) {
    this.route.queryParams.subscribe(params => {
      this.searchQuery = params['q'];
    });
  }

  ngOnInit(): void {
    this.filterBooks();
  }
  
  filterBooks() {
    this.bookService.filterBooks(this.searchQuery).subscribe(x => this.books = x);
  }

  loadBookPage(page: number) {
    this.currentBookPage += page;
    this.bookService.getBooks(this.currentBookPage).subscribe(x => this.books = x);
  }

}

