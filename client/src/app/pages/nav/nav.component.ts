import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Emitters } from 'src/app/emitters/emitters';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})

export class NavComponent implements OnInit {
  authenticatedUser: string = "";
  searchQuery: string;

  constructor(
    private userService: UserService,
    private router: Router
  ) { }

  title = 'BookList';

  ngOnInit(): void {
    Emitters.authEmitter.subscribe(
      (username: string) => {
        this.authenticatedUser = username;
      })
  }

  onSearch() {
    this.router.navigate(['/search'], { queryParams: { q: this.searchQuery } });
  }

  logout(): void {
    this.userService.logout().subscribe(() => this.authenticatedUser = "");
  }
}