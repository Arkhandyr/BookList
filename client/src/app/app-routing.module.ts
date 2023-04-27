import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { FormsBookResolver } from './pages/shared/forms-book.resolver';

import { BookAddComponent } from './pages/home/book-add/book-add.component';
import { BookEditComponent } from './pages/home/book-edit/book-edit.component';
import { BookListComponent } from './pages/home/book-list/book-list.component';

const routes: Routes = [
  {path:'', component:BookListComponent},
  {path:'add', component:BookAddComponent},
  {path:'update/:id', component:BookEditComponent, resolve: { book: FormsBookResolver } },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
