import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr/toastr/toastr.service';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.scss']
})
export class LandingPageComponent implements OnInit {
  login: boolean = true

  constructor(private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  flipScreen(login: boolean) {
    this.login = login;
  }
}
