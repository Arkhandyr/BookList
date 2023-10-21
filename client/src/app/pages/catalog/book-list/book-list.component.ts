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
  public books:Book[];
  searchQuery: string;
  page: number = 1
  maxPages: number = 0;

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

    this.bookService.getBooks(this.page).subscribe(x => this.books = x);
  }

  loadPage(page: number) {
    this.page += page;
    this.bookService.getBooks(this.page).subscribe(x => this.books = x);
  }

  goToBookPage(value: string) {
    this.router.navigate(['/book', value]);
  }
}
