import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { UserService } from '../user.service';
@Injectable({
  providedIn: 'root'
})
export class UnauthenticatedUserGuard implements CanActivate{
    constructor(
      private userService: UserService,
      private router: Router) { }
    canActivate(){
      if (this.userService.logged) {
        this.router.navigate(['']);
        return false;
      }
      return true;
    }
}