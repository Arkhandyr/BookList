import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { FormsBookResolver } from './pages/shared/forms-book.resolver';

import { BookAddComponent } from './pages/home/book-add/book-add.component';
import { BookEditComponent } from './pages/home/book-edit/book-edit.component';
import { BookListComponent } from './pages/home/book-list/book-list.component';
import { LoginComponent } from './pages/login/login.component';
import { HomeComponent } from './pages/home/home.component';
import { UnauthenticatedUserGuard } from './services/guards/unauthenticatedUser.guard';
import { AuthenticatedUserGuard } from './services/guards/authenticatedUser.guard';

const routes: Routes = [
  {path:'login', component:LoginComponent, canActivate: [UnauthenticatedUserGuard]},
  {path:'', canActivate: [AuthenticatedUserGuard],
    children: [
      {path:'', component:BookListComponent},
      {path:'add', component:BookAddComponent},
      {path:'update/:id', component:BookEditComponent, resolve: { book: FormsBookResolver } }
    ],
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
