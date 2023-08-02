import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})

export class LoginComponent implements OnInit {
  form: FormGroup;

  constructor(
    private service:UserService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private router: Router) { }

  ngOnInit(): void {
    this.form = this.formBuilder.group( {
      email: '',
      password: ''
    })
  }

  submit(): void {
    this.service.login(this.form.getRawValue())
      .subscribe(() => {
        this.router.navigate(['/home'])
      })
  }
}