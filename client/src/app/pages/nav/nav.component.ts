import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Emitters } from 'src/app/emitters/emitters';
import { Profile } from 'src/app/interfaces/Profile';
import { AuthService } from 'src/app/services/auth.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})

export class NavComponent implements OnInit {
  user: Profile | null = null;
  searchQuery: string = "";

  constructor(
    private authService: AuthService,
    private userService: UserService,
    private router: Router
  ) { }

  title = 'BookList';

  ngOnInit(): void {
    Emitters.authEmitter.subscribe(
      (user: Profile) => {
        this.userService.getUser().subscribe(() => this.user = user);
      })

    if(!this.authService.isAuthenticated()) {
      this.router.navigate(['/']);
    }
  }

  onSearch() {
    const queryParams = { q: this.searchQuery };
    this.router.navigate(['/search'], { queryParams: queryParams });
  }

  goToHomePage() {
    this.router.navigate(['/home']);
  }

  logout(): void {
    this.authService.logout().subscribe(() => this.user = null);
  }
}