import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.scss']
})
export class LandingPageComponent implements OnInit {
  login: boolean = true

  constructor() { }

  ngOnInit(): void {
  }

  flipScreen(login: boolean) {
    this.login = login;
  }
}
