import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { FormsBookResolver } from './pages/shared/forms-book.resolver';

import { BookAddComponent } from './pages/catalog/book-add/book-add.component';
import { BookEditComponent } from './pages/catalog/book-edit/book-edit.component';
import { BookListComponent } from './pages/catalog/book-list/book-list.component';
import { LoginComponent } from './pages/login/login.component';
import { UserProfileComponent } from './pages/user-profile/user-profile.component';
import { UnauthenticatedUserGuard } from './services/guards/unauthenticatedUser.guard';
import { AuthenticatedUserGuard } from './services/guards/authenticatedUser.guard';

import { BookComponent } from './pages/book/book.component';

const routes: Routes = [
  {path:'login', component:LoginComponent, canActivate: [UnauthenticatedUserGuard]},
  {path:'', canActivate: [AuthenticatedUserGuard],
    children: [
      {path:'', component:BookListComponent},
      {path:'add', component:BookAddComponent},
      {path:'update/:id', component:BookEditComponent, resolve: { book: FormsBookResolver } },
      {path:'book/:id', component:BookComponent},
      {path:'profile/:id', component:UserProfileComponent}
    ],
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
