import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})

export class HomeComponent implements OnInit {
  constructor(private userService: UserService) { }

  title = 'BookList';

  ngOnInit(): void {
  }

  logout(){
    this.userService.logout();
  }
}