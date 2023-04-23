import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Book } from '../shared/book';
import { BookService } from '../shared/books.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-book-edit',
  templateUrl: './book-edit.component.html',
  styleUrls: ['./book-edit.component.scss']
})
export class BookEditComponent implements OnInit {

  constructor(
    private service:BookService, 
    private formBuilder:FormBuilder,
    private route:ActivatedRoute,
    private toastr: ToastrService) { }

  public book: Book;
  form:FormGroup;

  ngOnInit(): void {
    this.book = this.route.snapshot.data['book'];

    this.form = this.formBuilder.group({
      "_id": new FormControl(null, [Validators.required]),
      "title": new FormControl(null, [Validators.required]),
      "author": new FormControl(null, [Validators.required]),
      "pages": new FormControl(null, [Validators.required]),
    });

    this.form.patchValue({
      _id: this.book._id,
      title: this.book.title,
      author: this.book.author,
      pages: this.book.pages,
    })
  }

  public selectedCover(imageInput: HTMLInputElement) {

    const reader = new FileReader();
    const files = imageInput.files as FileList;

    reader.addEventListener('load', (event: any) => {

      var element = event.target as FileReader;

      this.book.cover = element.result as string ;
    });

    reader.readAsDataURL(files[0]);
  }

  submitData() {
    let book = this.form.value as Book;
    book.cover = this.book.cover; 
    console.log(book.cover);
    this.service.updateBook(book)
      .subscribe(response => {
        this.toastr.success('Livro editado com sucesso', 'Sucesso');
        console.log(response)
      })
  }
}
