import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { Emitters } from 'src/app/emitters/emitters';
import { Book } from 'src/app/interfaces/Book';
import { BookService } from 'src/app/services/books.service';
import { ListService } from 'src/app/services/list.service';
import { NavComponent } from '../nav/nav.component';
import { HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.scss']
})
export class BookComponent implements OnInit {
  username: string = this.navComponent.authenticatedUser;
  bookId: string
  public book: Book;
  private sub: any;

  constructor(
    private route: ActivatedRoute,
    private navComponent: NavComponent,
    private bookService:BookService,
    private listService:ListService) { }

  ngOnInit() {
    this.sub = this.route.params.subscribe(params => {
       this.bookId = params['id'];

       this.bookService.getBookById(this.bookId).subscribe(x => this.book = x);
    });
  }

  addToList(): void {
    let listEntry : string = JSON.stringify({ user: this.username, book: this.bookId })

    this.listService.addToList(listEntry).subscribe({
      next: (res) => {
        //alterar botão para "remover da lista"
      },
      error: (err) => {
        console.log(err);
      }
    });
  }
}
