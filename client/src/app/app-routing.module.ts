import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { BookAddComponent } from './pages/catalog/book-add/book-add.component';
import { BookListComponent } from './pages/catalog/book-list/book-list.component';
import { UserProfileComponent } from './pages/user-profile/user-profile.component';

import { BookComponent } from './pages/book/book.component';
import { SearchComponent } from './search/search.component';
import { LandingPageComponent } from './pages/landing-page/landing-page.component';
import { AuthorComponent } from './pages/author/author.component';
import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [
  {path:'',
    children: [
      
      {path:'', component:LandingPageComponent},
      {path:'home', component:BookListComponent, canActivate: [AuthGuard]},
      {path:'add', component:BookAddComponent, canActivate: [AuthGuard]},
      {path:'book/:id', component:BookComponent, canActivate: [AuthGuard]},
      {path:'profile/:username', component:UserProfileComponent, canActivate: [AuthGuard]},
      {path:'author/:name', component:AuthorComponent, canActivate: [AuthGuard]},
      {path:'search', component: SearchComponent, canActivate: [AuthGuard]},
    ],
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
