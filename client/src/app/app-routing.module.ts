import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { BookAddComponent } from './pages/catalog/book-add/book-add.component';
import { BookListComponent } from './pages/catalog/book-list/book-list.component';
import { UserProfileComponent } from './pages/user-profile/user-profile.component';

import { BookComponent } from './pages/book/book.component';
import { SearchComponent } from './search/search.component';
import { LandingPageComponent } from './pages/landing-page/landing-page.component';
import { AuthorComponent } from './pages/author/author.component';

const routes: Routes = [
  {path:'',
    children: [
      {path:'', component:LandingPageComponent},
      {path:'home', component:BookListComponent},
      {path:'add', component:BookAddComponent},
      {path:'book/:id', component:BookComponent},
      {path:'profile/:username', component:UserProfileComponent},
      {path:'author/:id', component:AuthorComponent},
      {path:'search', component: SearchComponent },
    ],
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
