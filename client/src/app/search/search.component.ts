import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BookService } from '../services/books.service';
import { Book } from '../interfaces/Book';
import { FadeInOut } from 'src/animation';
import { Author } from '../interfaces/Author';
import { Profile } from '../interfaces/Profile';
import { SearchService } from '../services/search.service';
import { User } from '../interfaces/User';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss'],
  animations: [FadeInOut(300, 300, false)]
})
export class SearchComponent implements OnInit {
  searchQuery: string = "";

  books: Book[] = [];
  authors: Author[] = [];
  users: User[] = [];

  currentBookPage: number = 0;
  currentAuthorPage: number = 0;
  currentUserPage: number = 0;

  placeholderBookCards: any[];
  placeholderAuthorCards: any[];
  placeholderUserCards: any[];

  constructor(
    private route: ActivatedRoute, 
    private searchService: SearchService) {
    this.route.queryParams.subscribe(params => {
      this.searchQuery = params['q'];
    });
  }

  ngOnInit(): void {
    this.filter();
  }
  
  filter() {
    this.loadBookPage(1);
    this.loadAuthorPage(1);
    this.loadUserPage(1);

    console.log(this.books.length)
    console.log(this.authors.length)
    console.log(this.users.length)
  }

  loadBookPage(page: number) {
    this.currentBookPage += page;
    this.searchService.filterBooks(this.searchQuery, this.currentBookPage).subscribe(x => {
      this.books = x
      this.placeholderBookCards = new Array(5 - this.books.length).fill(null);
    });
  }

  loadAuthorPage(page: number) {
    this.currentAuthorPage += page;
    this.searchService.filterAuthors(this.searchQuery, this.currentAuthorPage).subscribe(x => {
      this.authors = x
      this.placeholderAuthorCards = new Array(5 - this.authors.length).fill(null);
    });
  }

  loadUserPage(page: number) {
    this.currentUserPage += page;
    this.searchService.filterUsers(this.searchQuery, this.currentUserPage).subscribe(x => {
      this.users = x
      this.placeholderUserCards = new Array(5 - this.users.length).fill(null);
      console.log(this.users)
    });
  }

  adjustTextToURL(text: string) {
    return text.replace(/\./g, '-').replace(/ /g, '_');
  }
}

