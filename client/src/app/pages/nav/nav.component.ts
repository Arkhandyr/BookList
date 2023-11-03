import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Emitters } from 'src/app/emitters/emitters';
import { AuthService } from 'src/app/services/auth.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})

export class NavComponent implements OnInit {
  authenticatedUser: string = "";
  searchQuery: string = "";

  constructor(
    private authService: AuthService,
    private router: Router
  ) { }

  title = 'BookList';

  ngOnInit(): void {
    Emitters.authEmitter.subscribe(
      (username: string) => {
        this.authenticatedUser = username;
      })

    if(!this.authenticatedUser) {
      this.router.navigate(['/']);
    }
  }

  onSearch() {
    const queryParams = { q: this.searchQuery };
    this.router.navigate(['/search'], { queryParams: queryParams });
  }

  goToHomePage() {
    if(this.authenticatedUser) {
      this.router.navigate(['/home']);
    }
  }

  logout(): void {
    this.authService.logout().subscribe(() => this.authenticatedUser = "");
  }
}