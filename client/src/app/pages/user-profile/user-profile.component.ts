import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { forkJoin } from 'rxjs/internal/observable/forkJoin';
import { FadeInOut } from 'src/animation';
import { Badge } from 'src/app/interfaces/Badge';
import { Book } from 'src/app/interfaces/Book';
import { Profile } from 'src/app/interfaces/Profile';
import { User } from 'src/app/interfaces/User';
import { AuthService } from 'src/app/services/auth.service';
import { BadgeService } from 'src/app/services/badge.service';
import { BookService } from 'src/app/services/books.service';
import { ListService } from 'src/app/services/list.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss'],
  animations: [FadeInOut(300, 300, false)]
})
export class UserProfileComponent implements OnInit {
  loggedUsername: string = localStorage.getItem('loggedUser') ?? "";
  admin: boolean;
  user: Profile;
  username: string;
  followed: boolean;

  badges: Badge[];
  listCount: number[];

  reading: Book[];
  planning: Book[];
  read: Book[];

  currentReadingPage: number = 1;
  currentPlanningPage: number = 1;
  currentReadPage: number = 1;

  placeholderReadingCards: any[];
  placeholderPlanningCards: any[];
  placeholderReadCards: any[];

  constructor(
    private route: ActivatedRoute,
    private authService: AuthService,
    private userService: UserService,
    private listService: ListService,
    private badgeService: BadgeService, 
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.username = params['username'];
      this.userService.getByUsername(this.loggedUsername ?? "").subscribe(x => this.admin = x.admin),
      forkJoin({
        user: this.userService.getByUsername(this.username),
        followed: this.userService.getFollowStatus(this.loggedUsername, this.username),
        reading: this.listService.getUserReading(this.username, 1),
        planning: this.listService.getUserPlanning(this.username, 1),
        read: this.listService.getUserRead(this.username, 1),
        listCount: this.listService.getListCount(this.username),
        badges: this.badgeService.getUserBadges(this.username)
      }).subscribe({
        next: (res) => {
          this.user = res.user,
          this.followed = res.followed,
          
          this.reading = res.reading,
          this.placeholderReadingCards = new Array(5 - this.reading.length).fill(null);

          this.planning = res.planning,
          this.placeholderPlanningCards = new Array(5 - this.reading.length).fill(null);

          this.read = res.read
          this.placeholderReadCards = new Array(5 - this.read.length).fill(null);

          this.badges = res.badges;
          this.listCount = res.listCount;
        }
      });
    });
  };

  loadReadingPage(page: number) {
    this.currentReadingPage += page;
    this.listService.getUserReading(this.username, this.currentReadingPage).subscribe(x => {
      this.reading = x
      this.placeholderReadingCards = new Array(5 - this.reading.length).fill(null);
    });
  }

  loadPlanningPage(page: number) {
    this.currentPlanningPage += page;
    this.listService.getUserPlanning(this.username, this.currentPlanningPage).subscribe(x => {
      this.planning = x
      this.placeholderReadingCards = new Array(5 - this.planning.length).fill(null);
    });
  }

  loadReadPage(page: number) {
    this.currentReadPage += page;
    this.listService.getUserRead(this.username, this.currentReadPage).subscribe(x => {
      this.read = x
      this.placeholderReadCards = new Array(5 - this.read.length).fill(null);
    });
  }

  follow() {
    let followEntry : string = JSON.stringify({ User: this.userService.getByUsername(this.loggedUsername), User2: this.username })

    this.userService.follow(followEntry).subscribe({
      next: async () => {
        this.userService.getFollowStatus(this.loggedUsername, this.username).subscribe(x => this.followed = x)
        this.toastr.success('Agora você está seguindo ' + this.user.realName, 'Sucesso');
      }
    });
  }

  unfollow() {
    let followEntry : string = JSON.stringify({ User: this.userService.getByUsername(this.loggedUsername), User2: this.username })

    this.userService.unfollow(followEntry).subscribe({
      next: async () => {
        this.userService.getFollowStatus(this.loggedUsername, this.username).subscribe(x => this.followed = x)
        this.toastr.success('Você deixou de seguir ' + this.user.realName, 'Sucesso');
      }
    });
  }

  adjustTextToURL(text: string) {
    return text.replace(/\./g, '-').replace(/ /g, '_');
  }
}
