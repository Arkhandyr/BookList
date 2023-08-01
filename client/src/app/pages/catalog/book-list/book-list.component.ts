import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Book } from '../../../interfaces/Book';
import { BookService } from '../../../services/books.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.scss']
})
export class BookListComponent implements OnInit {

  constructor(
    private router:Router,
    private service:BookService, 
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.service.getBooks().subscribe(x => this.books = x);
  }

  public books:Book[];
  filter: string;

  filterBooks() {
      this.service.filterBooks(this.filter).subscribe(x => this.books = x);
  }

  goToBookPage(value: string) {
    this.router.navigate(['/book', value]);
  }

  remove(_id: string) {
    this.service.deleteBook(_id)
      .subscribe(response => {
        this.toastr.success('Livro removido com sucesso', 'Sucesso');
        this.service.getBooks().subscribe(x => this.books = x);
        console.log(response)
      });
  }
}
