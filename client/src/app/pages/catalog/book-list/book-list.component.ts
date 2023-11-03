import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Book } from '../../../interfaces/Book';
import { BookService } from '../../../services/books.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { Emitters } from 'src/app/emitters/emitters';
import { animate, style, transition, trigger } from '@angular/animations';
import { FadeInOut } from 'src/animation';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.scss'],
  animations: [FadeInOut(300, 300, false)]
})
export class BookListComponent implements OnInit {
  public topBooks:Book[];
  currentTopPage: number = 1
  public trendingBooks:Book[];
  currentTrendingPage: number = 1

  constructor(
    private router:Router,
    private bookService:BookService,
    private userService:UserService, 
    private toastr: ToastrService) { }
 
  ngOnInit(): void {
    this.userService.getUser().subscribe({
      next: (res) => {
        Emitters.authEmitter.emit(res.username);
      },
      error: (err) => {
        console.log(err);
        this.router.navigate(['/login']);
      }
    });

    this.bookService.getBooks(this.currentTopPage).subscribe(x => this.topBooks = x);
    this.bookService.getBooks(this.currentTrendingPage).subscribe(x => this.trendingBooks = x);
  }

  loadTopPage(page: number) {
    this.currentTopPage += page;
    this.bookService.getBooks(this.currentTopPage).subscribe(x => this.topBooks = x);
  }

  loadTrendingPage(page: number) {
    this.currentTrendingPage += page;
    this.bookService.getBooks(this.currentTrendingPage).subscribe(x => this.trendingBooks = x);
  }

  goToBookPage(value: string) {
    this.router.navigate(['/book', value]);
  }

  adjustTextToURL(text: string) {
    return text.replace(/\./g, '-').replace(/ /g, '_');
  }
}
