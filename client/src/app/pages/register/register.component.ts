import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Toast, ToastrService } from 'ngx-toastr';
import { User } from '../../interfaces/User';
import { UserService } from 'src/app/services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  form: FormGroup
  
  constructor(
    private service:UserService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private router: Router) { }

  ngOnInit(): void {
    this.form = this.formBuilder.group( {
      username: '',
      realName: '',
      email: '',
      password: '',
      picture: ''
    });
  }

  profilePic : string | ArrayBuffer | null;

  public selectedPicture(imageInput: HTMLInputElement) {

    const reader = new FileReader();
    const files = imageInput.files as FileList;

    reader.addEventListener('load', (event: any) => {

      var element = event.target as FileReader;

      this.profilePic = element.result;
    });

    reader.readAsDataURL(files[0]);
  }

  submit() {
    let user = this.form.value as User;
    user.picture = this.profilePic as string;

    this.service.register(user)
      .subscribe(() => {
        this.toastr.success('Usuário cadastrado!', 'Sucesso');
        this.router.navigate(['/login'])
      })
  }
}
