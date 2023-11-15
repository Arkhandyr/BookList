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
    private authService: AuthService,
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
      .subscribe({
        next: (res) => {
          this.router.navigate(['/home']);
          this.authService.setLoggedUser(res);
        },
        error: () => {
          this.toastr.error('Credenciais inválidas', 'Erro');
        }
      })
  }

  flipScreen() {
    this.login.emit(false);
  }
}