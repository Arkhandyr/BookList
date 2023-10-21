import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BookService } from '../services/books.service';
import { Book } from '../interfaces/Book';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent implements OnInit {
  searchQuery: string;
  books: Book[];

  constructor(
    private route: ActivatedRoute, 
    private bookService: BookService) {
    this.route.queryParams.subscribe(params => {
      this.searchQuery = params['q'];
      // Use this.searchQuery to perform your search.
    });
  }

  ngOnInit(): void {
  }
  
  filterBooks() {
    this.bookService.filterBooks(this.searchQuery).subscribe(x => this.books = x);
}
}

