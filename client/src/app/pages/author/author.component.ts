import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Author } from 'src/app/interfaces/Author';
import { Book } from 'src/app/interfaces/Book';
import { Profile } from 'src/app/interfaces/Profile';
import { AuthorService } from 'src/app/services/author.service';
import { BookService } from 'src/app/services/books.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-author',
  templateUrl: './author.component.html',
  styleUrls: ['./author.component.scss']
})
export class AuthorComponent implements OnInit {
  author: Author;
  name: string;
  books: Book[];
  private sub: any;

  constructor(
    private route: ActivatedRoute,
    private authorService: AuthorService,
    private bookService: BookService,
    private router: Router,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.sub = this.route.params.subscribe(params => {
      this.name = params['name'];

      this.authorService.getByName(this.name).subscribe({
        next: (res) => {
          this.author = res;
        }
    });

    this.bookService.getBookByAuthor(this.name).subscribe({
      next: (res) => {
        this.books = res;
      }
    });
  });
  }
}