import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Emitters } from 'src/app/emitters/emitters';
import { Book } from 'src/app/interfaces/Book';
import { Profile } from 'src/app/interfaces/Profile';
import { User } from 'src/app/interfaces/User';
import { BookService } from 'src/app/services/books.service';
import { ListService } from 'src/app/services/list.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent implements OnInit {
  user: Profile;
  username: string;
  books: Book[];
  private sub: any;

  constructor(
    private route: ActivatedRoute,
    private userService: UserService,
    private listService: ListService, 
    private router: Router,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.sub = this.route.params.subscribe(params => {
      this.username = params['username'];

      this.userService.getByUsername(this.username).subscribe({
        next: (res) => {
          this.user = res;
        },
        error: (err) => {
          console.log(err);
        }
      });
    });

    this.listService.getUserLists(this.username).subscribe({
      next: (res) => {
        this.books = res;
      },
      error: (err) => {
        console.log(err);
      }
    });
  }
}
