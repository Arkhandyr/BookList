import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Book } from 'src/app/interfaces/IBook';
import { BookService } from 'src/app/services/books.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent implements OnInit {

  constructor(
    private userService:UserService,
    private bookService:BookService, 
    private toastr: ToastrService) { }

  ngOnInit(): void {
    //this.userService.getBooksByUser().subscribe(x => this.books = x);
  }

  public books:Book[];
  filter: string;

  filterBooks() {
      this.bookService.filterBooks(this.filter).subscribe(x => this.books = x);
  }
}
