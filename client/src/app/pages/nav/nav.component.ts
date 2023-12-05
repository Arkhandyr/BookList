import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})

export class NavComponent implements OnInit {
  loggedUser: string | null = localStorage.getItem('loggedUser');
  searchQuery: string = "";

  constructor(
    private authService: AuthService,
    private userService: UserService,
    private router: Router
  ) { }

  title = 'BookList';

  ngOnInit() {
    this.loggedUser = localStorage.getItem('loggedUser');
  }

  onSearch() {
    const queryParams = { q: this.searchQuery };
    this.router.navigate(['/search'], { queryParams: queryParams })
  }

  logout(): void {
    this.authService.logout().subscribe(() => localStorage.removeItem('loggedUser'));
  }
}