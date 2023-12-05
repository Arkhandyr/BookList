import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Book } from '../../../interfaces/Book';
import { BookService } from '../../../services/books.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { animate, style, transition, trigger } from '@angular/animations';
import { FadeInOut } from 'src/animation';
import { Profile } from 'src/app/interfaces/Profile';
import { AuthService } from 'src/app/services/auth.service';
import { forkJoin } from 'rxjs/internal/observable/forkJoin';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.scss'],
  animations: [FadeInOut(300, 300, false)]
})
export class BookListComponent implements OnInit {
  loggedUsername: string = localStorage.getItem('loggedUser') ?? "";
  admin: boolean;
  public topBooks: Book[];
  currentTopPage: number = 1

  public trendingBooks: Book[];
  currentTrendingPage: number = 1

  constructor(
    private router:Router,
    private bookService:BookService,
    private userService:UserService,
    private toastr: ToastrService) { }
 
  ngOnInit(): void {
    
    this.userService.getByUsername(this.loggedUsername).subscribe(x => this.admin = x.admin),
    forkJoin({
      topBooks: this.bookService.getTopBooks(this.currentTopPage),
      trendingBooks: this.bookService.getTrendingBooks(this.currentTrendingPage),
    }).subscribe({
      next: (res) => {
        this.topBooks = res.topBooks,
        this.trendingBooks = res.trendingBooks
      }
    });
  }

  loadTopPage(page: number) {
    this.currentTopPage += page;
    this.bookService.getTopBooks(this.currentTopPage).subscribe(x => this.topBooks = x);
  }

  loadTrendingPage(page: number) {
    this.currentTrendingPage += page;
    this.bookService.getTrendingBooks(this.currentTrendingPage).subscribe(x => this.trendingBooks = x);
  }

  adjustTextToURL(text: string) {
    return text.replace(/\./g, '-').replace(/ /g, '_');
  }
}
