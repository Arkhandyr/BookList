import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { UserService } from '../user.service';
@Injectable({
  providedIn: 'root'
})
export class AuthenticatedUserGuard implements CanActivate{
    constructor(
      private userService: UserService,
      private router: Router) { }
    canActivate(){
      if (this.userService.logged) {
        return true;
      }
      this.router.navigate(['login']);
      return false;
    }
}