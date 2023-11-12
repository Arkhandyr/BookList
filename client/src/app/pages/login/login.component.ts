import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})

export class LoginComponent implements OnInit {
  @Output() login = new EventEmitter<boolean>();
  form: FormGroup;

  constructor(
    private service:AuthService,
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

  flipScreen() {
    this.login.emit(false);
  }
}