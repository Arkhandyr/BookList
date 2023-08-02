import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Book } from '../../../interfaces/Book';
import { BookService } from '../../../services/books.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { Emitters } from 'src/app/emitters/emitters';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.scss']
})
export class BookListComponent implements OnInit {

  constructor(
    private router:Router,
    private bookService:BookService,
    private userService:UserService, 
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.userService.getUser().subscribe({
      next: () => {
        Emitters.authEmitter.emit(true);
      },
      error: (err) => {
        console.log(err);
        Emitters.authEmitter.emit(false);
        this.router.navigate(['/login']);
      }
    });

    this.bookService.getBooks().subscribe(x => this.books = x);
  }

  public books:Book[];
  filter: string;

  filterBooks() {
      this.bookService.filterBooks(this.filter).subscribe(x => this.books = x);
  }

  goToBookPage(value: string) {
    this.router.navigate(['/book', value]);
  }
}
