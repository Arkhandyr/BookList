import { Component, EventEmitter, OnInit, Output } from '@angular/core';
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
  @Output() login = new EventEmitter<boolean>();
  form: FormGroup
  profilePic : string | ArrayBuffer | null;
  
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

  public selectedPicture(event: Event) {
    const inputElement = event.target as HTMLInputElement;

    if (inputElement.files) {
      const file = inputElement.files[0];
      if (file) {
        const reader = new FileReader();

        reader.addEventListener('load', (event: any) => {
  
          var element = event.target as FileReader;
    
          this.profilePic = element.result;
        });
        
        reader.readAsDataURL(inputElement.files[0]);
      }
      
    } else {
      this.profilePic = '';
    }
    
  }

  submit() {
    let user = this.form.value as User;
    user.picture = this.profilePic as string;

    this.service.register(user)
      .subscribe(() => {
        this.toastr.success('Usuário cadastrado!', 'Sucesso');
      })

    this.flipScreen();
  }

  flipScreen() {
    this.login.emit(true);
  }
}
