import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { forkJoin } from 'rxjs/internal/observable/forkJoin';
import { Emitters } from 'src/app/emitters/emitters';
import { Badge } from 'src/app/interfaces/Badge';
import { Book } from 'src/app/interfaces/Book';
import { Profile } from 'src/app/interfaces/Profile';
import { User } from 'src/app/interfaces/User';
import { BadgeService } from 'src/app/services/badge.service';
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
  books: Book[][] = [[], [], []];
  badges: Badge[];
  private sub: any;

  constructor(
    private route: ActivatedRoute,
    private userService: UserService,
    private listService: ListService,
    private badgeService: BadgeService, 
    private router: Router,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.sub = this.route.params.subscribe(params => {
      this.username = params['username'];

      forkJoin({
        user: this.userService.getByUsername(this.username),
        lists: this.listService.getUserLists(this.username),
        badges: this.badgeService.getUserBadges(this.username)
      }).subscribe({
        next: (res) => {
          this.user = res.user;
          [this.books[0], this.books[1], this.books[2]] = res.lists.slice(0, 3);
          this.badges = res.badges;
        },
        error: (err) => {
          console.log(err);
        }
      });
    });
  };
}
