import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { FormsBookResolver } from './book/shared/forms-book.resolver';

import { BookAddComponent } from './book/book-add/book-add.component';
import { BookEditComponent } from './book/book-edit/book-edit.component';
import { BookListComponent } from './book/book-list/book-list.component';

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
