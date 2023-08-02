import { Component, OnInit } from '@angular/core';
import { Emitters } from 'src/app/emitters/emitters';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})

export class NavComponent implements OnInit {
  authenticated = false;

  constructor(
    private userService: UserService
  ) { }

  title = 'BookList';

  ngOnInit(): void {
    Emitters.authEmitter.subscribe(
      (auth: boolean) => {
        this.authenticated = auth;
      })
  }

  logout(): void {
    this.userService.logout().subscribe(() => this.authenticated = false);
  }
}